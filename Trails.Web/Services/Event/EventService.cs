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

        public async Task<bool> CreateEventAsync(EventFormModel eventFormModel,string currentUserId, IFormFile imgFile)
        {
            var isExisting = await this.dbContext
                .Events
                .AnyAsync(e => e.Name == eventFormModel.Name);

            if (isExisting)
            {
                return false;
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

            await this.dbContext
                .Events
                .AddAsync(@event);

            var created = await this.dbContext
                .SaveChangesAsync();

            return created > 0;
        }
    }
}
