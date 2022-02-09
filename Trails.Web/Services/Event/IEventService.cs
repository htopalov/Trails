using Trails.Web.Models.Event;

namespace Trails.Web.Services.Event
{
    public interface IEventService
    {
        Task<string> CreateEventAsync(EventFormModel eventFormModel, string currentUserId, IFormFile imgFile);

        Task<EventDetailsModel> GetEventAsync(string eventId);
    }
}
