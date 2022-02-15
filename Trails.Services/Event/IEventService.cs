using Microsoft.AspNetCore.Http;
using Trails.Models.Event;

namespace Trails.Services.Event
{
    public interface IEventService
    {
        Task<string> CreateEventAsync(EventFormModel eventFormModel, string currentUserId, IFormFile imgFile);

        Task<EventDetailsModel> GetEventAsync(string eventId);

        Task<bool> DeleteEventAsync(string eventId);

        Task<bool> ApplyForEventAsync(string userId, string eventId);

        Task<bool> ApproveParticipantAsync(string participantId,string eventId);

        Task<bool> EditImageAsync(string currentUserId, string eventId, IFormFile imgFile);

        Task<EventEditFormModel> GetEventToEditAsync(string eventId);

        Task<bool> EditEventAsync(string eventId, EventEditFormModel eventEditFormModel);
    }
}
