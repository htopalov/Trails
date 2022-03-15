using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Data.DomainModels;
using Trails.GPXProcessor;
using Trails.GPXProcessor.Models.Export;
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

        public async Task<string> CreateEventAsync(EventFormModel eventFormModel)
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

            @event.Image = await ImageProcessor
                .ProcessImageToDb(eventFormModel.Image,eventFormModel.CreatorId);

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

        public async Task<T> GetEventAsync<T>(string eventId)
            where T : IEventModel
        {
            var queryableEvent = this.dbContext
                .Events
                .AsQueryable();

            if (typeof(T) == typeof(EventDetailsModel))
            {
                queryableEvent = queryableEvent
                    .Include(e => e.Creator)
                    .Include(e => e.Image)
                    .Include(e => e.Participants)
                    .ThenInclude(p => p.User)
                    .Include(e => e.Route);
            }

            var @event = await queryableEvent
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (@event == null)
            {
                return default;
            }

            var mappedEvent = this.mapper
                .Map<T>(@event);

            return mappedEvent;
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

            if (IsEventLocked(@event.StartDate))
            {
                return false;
            }

            this.mapper
                .Map(eventEditFormModel, @event);

            @event.IsModifiedByCreator = true;

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

            if (DateTime.UtcNow >= @event.StartDate && DateTime.UtcNow <= @event.EndDate)
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

            if (IsEventLocked(eventToParticipate.StartDate))
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

            var participant = new Participant
            {
                User = user,
                Event = eventToParticipate
            };

            await this.dbContext
                .Participants
                .AddAsync(participant);

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

            if (IsEventLocked(eventOfParticipant.StartDate))
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

            var updated = await this.dbContext
                .SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> EditImageAsync(string eventId, EventImageEditModel imageModel)
        {
            var @event = await this.dbContext
                .Events
                .FindAsync(eventId);

            if (@event == null)
            {
                return false;
            }

            if (IsEventLocked(@event.StartDate))
            {
                return false;
            }

            var img = await ImageProcessor
                .ProcessImageToDb(imageModel.Image, @event.CreatorId);

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
                           .Where(e => e.IsApproved && e.IsDeleted == false && DateTime.UtcNow < e.StartDate)
                           .OrderBy(e => e.StartDate)
                           .Take(3)
                           .ToListAsync();

            return this.mapper
                .Map<List<FirstToStartEventCardModel>>(events);
        }

        public async Task<ListEventsModel> GetEventsAsync(
            string userId = null,
            string searchEvent = null,
            int currentPage = 1,
            int eventsPerPage = int.MaxValue)
        {
            var queryableEvents = this.dbContext
                .Events
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                queryableEvents = queryableEvents
                    .Where(e => e.CreatorId == userId);
            }

            if (!string.IsNullOrEmpty(searchEvent))
            {
                queryableEvents = queryableEvents
                    .Where(e => e.Name.ToLower().Contains(searchEvent.ToLower()));
            }

            var events = await queryableEvents
                .Where(e => e.IsApproved && e.IsDeleted == false)
                .OrderByDescending(e=>e.StartDate)
                .ToListAsync(); 

            var totalEvents = events.Count;

            var pagedEvents = events
                .Skip((currentPage - 1) * eventsPerPage)
                .Take(eventsPerPage)
                .ToList();

            var mappedEvents = this.mapper
                .Map<List<BaseEventModel>>(pagedEvents);

            return new ListEventsModel
            {
                TotalEvents = totalEvents,
                CurrentPage = currentPage,
                Events = mappedEvents,
                SearchEvent = searchEvent,
                UserId = userId
            };
        }

        public async Task<List<LiveEventCardModel>> GetLiveEventsAsync()
        {
            var events = await this.dbContext
                .Events
                .Include(e => e.Image)
                .Where(e => e.IsDeleted == false && e.IsApproved)
                .Where(e=>e.Participants.Count > 0)
                .Where(e => e.StartDate <= DateTime.UtcNow && e.EndDate >= DateTime.UtcNow)
                .ToListAsync();

            var mappedEvents = this.mapper
                .Map<List<LiveEventCardModel>>(events);

            return mappedEvents;
        }

        public async Task<LiveEventDetailsModel> GetLiveEventAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events
                .Include(e => e.Participants.Where(p => p.IsApproved))
                .ThenInclude(p=>p.BeaconData)
                .Include(e=>e.Participants.Where(p=>p.IsApproved))
                .ThenInclude(p => p.User)
                .Include(e => e.Route)
                .ThenInclude(r => r.RoutePoints)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (@event == null)
            {
                return null;
            }

            var liveEvent = this.mapper
                .Map<LiveEventDetailsModel>(@event);

            return liveEvent;
        }

        public async Task<byte[]> GenerateParticipantPathAsync(string participantId)
        {
            var participantPositionsList = await this.dbContext
                .BeaconData
                .Where(bd => bd.ParticipantId == participantId)
                .OrderBy(bd=>bd.Timestamp)
                .ToListAsync();

            if (participantPositionsList.Count == 0)
            {
                return null;
            }

            var mappedPoints = this.mapper
                .Map<List<ExportPointModel>>(participantPositionsList);

            var gpxXml = RouteProcessor.Serialize(mappedPoints);

            await using var memoryStream = new MemoryStream();

            var fileBytes = Encoding.Default.GetBytes(gpxXml);

            await memoryStream.WriteAsync(fileBytes, 0, fileBytes.Length);

            return fileBytes;


        }

        private bool IsEventLocked(DateTime startDate) 
            => DateTime.UtcNow > startDate.AddDays(-3);
    }
}
