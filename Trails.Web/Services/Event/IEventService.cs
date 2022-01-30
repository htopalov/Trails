using Trails.Web.Models.Event;

namespace Trails.Web.Services.Event
{
    public interface IEventService
    {
        Task<bool> CreateEventAsync(EventFormModel eventFormModel, string currentUserId, IFormFile imgFile);
    }
}
