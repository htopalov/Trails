using System.Globalization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Web.Data;
using Trails.Web.Data.DomainModels;
using Trails.Web.Models.Event;

namespace Trails.Web.Services.Event
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

            await using var memoryStream = new MemoryStream();
            await imgFile.CopyToAsync(memoryStream);

            var img = new Data.DomainModels.Image()
            {
                Title = $"{Guid.NewGuid().ToString()}-{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}",
                CreatedOn = DateTime.UtcNow,
                CreatorId = currentUserId,
                DataBytes = memoryStream.ToArray()
            };

            @event.Image = img;

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

            var eventDetailsModel = this.mapper
                .Map<EventDetailsModel>(@event);

            eventDetailsModel.Image = ProcessImageFromDb(@event);

            return eventDetailsModel;
        }

        private static string ProcessImageFromDb(Data.DomainModels.Event @event)
        {
            var imageBaseData = Convert.ToBase64String(@event.Image.DataBytes);
            return $"data:image/jpg;base64,{imageBaseData}";
        }
    }
}
