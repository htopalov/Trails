using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Data.DomainModels;
using Trails.Infrastructure;
using Trails.Models.Event;

namespace Trails.Services.Event
{
    public class EventService : IEventService
    {
        private readonly TrailsDbContext dbContext;
        private readonly IMapper mapper;

        public EventService(TrailsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<string> CreateEventAsync(EventFormModel eventFormModel,string currentUserId, IFormFile imgFile)
        {
            var isExisting = await this.dbContext
                .Events
                .AnyAsync(e => e.Name == eventFormModel.Name);

            if (isExisting)
            {
                return string.Empty;
            }

            var @event = this.mapper
                .Map<Data.DomainModels.Event>(eventFormModel);

            @event.CreatorId = currentUserId;

            @event.Image = await ImageProcessor
                .ProcessImageToDb(imgFile, currentUserId);

            var result = await this.dbContext
                .Events
                .AddAsync(@event);

            var created = await this.dbContext
                .SaveChangesAsync();

            if (created > 0)
            {
                return result.Entity.Id;
            }

            return string.Empty;
        }

        public async Task<EventDetailsModel> GetEventAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events
                .Include(e=>e.Creator)
                .Include(e=>e.Image)
                .Include(e=>e.Participants)
                .Include(e=>e.Route)
                .FirstOrDefaultAsync(e=> e.Id == eventId);

            if (@event == null)
            {
                return null;
            }

            return this.mapper
                .Map<EventDetailsModel>(@event); ;
        }

        public async Task<EventEditFormModel> GetEventToEditAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events.FindAsync(eventId);

            if (@event == null)
            {
                return null;
            }

            var eventToEdit = this.mapper.
                Map<EventEditFormModel>(@event);

            return eventToEdit;
        }

        public async Task<bool> EditEventAsync(string eventId, EventEditFormModel eventEditFormModel)
        {
            var @event = await this.dbContext
                .Events
                .FindAsync(eventId);

            if (@event == null)
            {
                return false;
            }

            this.mapper.Map(eventEditFormModel, @event);

            @event.IsModifiedByCreator = true;
            //set is approved by admin again????

            this.dbContext
                .Events
                .Update(@event);

            var updated = await this.dbContext
                .SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteEventAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (@event == null)
            {
                return false;
            }

            @event.IsDeleted = true;

            this.dbContext
                .Events
                .Update(@event);

            var deleted = await this.dbContext
                .SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> ApplyForEventAsync(string userId,string eventId)
        {
            var eventToParticipate = await this.dbContext
                .Events
                .FindAsync(eventId);

            if (eventToParticipate == null)
            {
                return false;
            }

            var hasParticipantApplied = eventToParticipate
                .Participants
                .Any(p => p.UserId == userId);
            
            if (hasParticipantApplied)
            {
                return false;
            }

            var user = await this.dbContext
                .Users
                .FindAsync(userId);

            if (user == null)
            {
                return false;
            }

            var newParticipant = new Participant
            {
                User = user,
                Event = eventToParticipate
            };

            await this.dbContext
                .Participants
                .AddAsync(newParticipant);

            var created = await this.dbContext
                .SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> ApproveParticipantAsync(string participantId, string eventId)
        {
            var eventOfParticipant = await this.dbContext
                .Events
                .Include(e=>e.Participants)
                .FirstOrDefaultAsync(e=>e.Id == eventId);

            if (eventOfParticipant == null)
            {
                return false;
            }

            var participantToApprove = eventOfParticipant
                .Participants
                .FirstOrDefault(p => p.Id == participantId);

            if (participantToApprove == null)
            {
                return false;
            }

            participantToApprove.IsApproved = true;

            this.dbContext
                .Participants
                .Update(participantToApprove);

            var hasParticipantStateChanged = await this.dbContext
                .SaveChangesAsync();

            return hasParticipantStateChanged > 0;
        }

        public async Task<bool> EditImageAsync(string currentUserId, string eventId, IFormFile imgFile)
        {
            var @event = await this.dbContext
                .Events
                .FindAsync(eventId);

            if (@event == null)
            {
                return false;
            }

            var img = await ImageProcessor
                .ProcessImageToDb(imgFile, currentUserId);

            @event.Image = img;

            this.dbContext
                .Events
                .Update(@event);

            var updated = await this.dbContext
                .SaveChangesAsync();

            return updated > 0;
        }

        public async Task<List<FirstToStartEventCardModel>> GetClosestStartingEventsAsync()
        {
            var events = await this.dbContext
                           .Events
                           .Include(e=>e.Image)
                           .Where(e => e.IsApproved && e.IsDeleted == false)
                           .OrderBy(e => e.StartDate)
                           .Take(3)
                           .ToListAsync();

            return this.mapper
                .Map<List<FirstToStartEventCardModel>>(events);
        }
    }
}
