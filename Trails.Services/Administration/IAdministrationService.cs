using Trails.Models.Beacon;
using Trails.Models.Event;
using Trails.Models.Participant;

namespace Trails.Services.Administration
{
    public interface IAdministrationService
    {
        Task<int> GetUnapprovedEventsCountAsync();

        Task<AllUnapprovedEventsModel> GetAllUnapprovedEventsAsync(int currentPage = 1, int eventsPerPage = int.MaxValue);

        Task<bool> ApproveEventAsync(string eventId);

        Task<bool> DeclineEventAsync(string eventId);

        Task DetachBeaconsFromParticipantsInPassedEventsAsync();

        Task<List<EventPreparationModel>> GetEventsToPrepareAsync();

        Task<List<ParticipantPreparationModel>> GetParticipantsToPrepareAsync(string eventId);

        Task<List<BeaconPreparationModel>> GetBeaconsToConnectAsync();

        Task<bool> ConnectBeaconToParticipantAsync(ParticipantBeaconModel model);
    }
}
