using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Models.Beacon;
using Trails.Models.Event;
using Trails.Models.Participant;

namespace Trails.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly TrailsDbContext dbContext;
        private readonly IMapper mapper;

        public AdministrationService(TrailsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<int> GetUnapprovedEventsCountAsync() 
            => await this.dbContext
                .Events
                .Where(e => e.IsApproved == false && e.IsDeleted == false && !string.IsNullOrEmpty(e.RouteId))
                .CountAsync();

        public async Task<AllUnapprovedEventsModel> GetAllUnapprovedEventsAsync(
            int currentPage = 1,
            int eventsPerPage = int.MaxValue)
        {
            var unapproved = await this.dbContext
                .Events
                .Include(e=>e.Creator)
                .Where(e => e.IsApproved == false && e.IsDeleted == false && !string.IsNullOrEmpty(e.RouteId))
                .ToListAsync();

            var totalEvents = unapproved.Count;

            var pagedEvents = unapproved
                .Skip((currentPage - 1) * eventsPerPage)
                .Take(eventsPerPage)
                .ToList();

            var mappedEvents = this.mapper
                .Map<List<UnapprovedEventModel>>(pagedEvents);

            return new AllUnapprovedEventsModel
            {
                TotalEvents = totalEvents,
                CurrentPage = currentPage,
                Events = mappedEvents,
            };
        }

        public async Task<bool> ApproveEventAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events
                .Where(e=>e.IsDeleted == false)
                .FirstOrDefaultAsync(e=>e.Id == eventId);

            if (@event == null)
            {
                return false;
            }

            @event.IsApproved = true;

            this.dbContext
                .Events
                .Update(@event);

            var result =await this.dbContext
                .SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeclineEventAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events
                .FindAsync(eventId);

            if (@event == null)
            {
                return false;
            }

            @event.IsDeleted = true;

            this.dbContext
                .Events
                .Update(@event);

            var result = await this.dbContext
                .SaveChangesAsync();

            return result > 0;
        }

        public async Task DetachBeaconsFromParticipantsInPassedEventsAsync()
        {
            var participants = await this.dbContext
                .Participants
                .Include(p => p.Beacon)
                .Include(p => p.Event)
                .Where(p => p.Event.EndDate < DateTime.UtcNow && p.Beacon != null)
                .ToListAsync();

            if (participants.Count == 0)
            {
                return;
            }

            participants
                .AsParallel()
                .ForAll(p => p.Beacon = null);

            this.dbContext
                .Participants
                .UpdateRange(participants);

            await this.dbContext
                .SaveChangesAsync();
        }

        public async Task<List<EventPreparationModel>> GetEventsToPrepareAsync()
        {
            var events = await this.dbContext
                .Events
                .Where(e => e.IsApproved && e.IsDeleted == false)
                .Where(e => e.StartDate.AddDays(-3) <= DateTime.UtcNow && e.EndDate > DateTime.UtcNow)
                .OrderBy(e=>e.StartDate)
                .ToListAsync();

            var mappedEvents = this.mapper
                .Map<List<EventPreparationModel>>(events);

            return mappedEvents;
        }

        public async Task<List<ParticipantPreparationModel>> GetParticipantsToPrepareAsync(string eventId)
        {
            var @event = await this.dbContext
                .Events
                .Include(e=>e.Participants.Where(p=> string.IsNullOrEmpty(p.BeaconId) && p.IsApproved))
                .ThenInclude(p=>p.User)
                .FirstOrDefaultAsync(e=> e.Id == eventId);

            if (@event == null)
            {
                return null;
            }

            var mappedParticipants = this.mapper
                .Map<List<ParticipantPreparationModel>>(@event.Participants);

            return mappedParticipants;
        }

        public async Task<List<BeaconPreparationModel>> GetBeaconsToConnectAsync()
        {
            var listOfConnectedBeaconIds = await this.dbContext
                .Participants
                .Where(p => !string.IsNullOrEmpty(p.BeaconId))
                .Select(p=> new string(p.BeaconId))
                .ToListAsync();

            var beaconsToConnect = await this.dbContext
                .Beacons
                .Where(b => !listOfConnectedBeaconIds.Contains(b.Id))
                .ToListAsync();

            var mappedBeacons = this.mapper
                .Map<List<BeaconPreparationModel>>(beaconsToConnect);

            return mappedBeacons;
        }

        public async Task<bool> ConnectBeaconToParticipantAsync(ParticipantBeaconModel model)
        {
            var participant = await this.dbContext
                .Participants
                .FindAsync(model.ParticipantId);

            if (participant == null || !string.IsNullOrEmpty(participant.BeaconId))
            {
                return false;
            }

            var beacon = await this.dbContext
                .Beacons
                .FindAsync(model.BeaconId);

            if (beacon == null)
            {
                return false;
            }

            participant.BeaconId = model.BeaconId;

            this.dbContext
                .Participants
                .Update(participant);

            var updated = await this.dbContext
                .SaveChangesAsync();

            return updated > 0;
        }
    }
}
