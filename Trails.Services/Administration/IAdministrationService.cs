using Trails.Models.Event;

namespace Trails.Services.Administration
{
    public interface IAdministrationService
    {
        Task<int> GetUnapprovedEventsCountAsync();

        Task<AllUnapprovedEventsModel> GetAllUnapprovedEventsAsync(int currentPage = 1, int eventsPerPage = int.MaxValue);

        Task<bool> ApproveEventAsync(string eventId);

        Task<bool> DeclineEventAsync(string eventId);
    }
}
