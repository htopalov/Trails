using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Models.Event;

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

        public async Task<int> GetUnapprovedEventsCountAsync() =>
            await this.dbContext
                .Events
                .Where(e => e.IsApproved == false && e.IsDeleted == false)
                .CountAsync();

        public async Task<AllUnapprovedEventsModel> GetAllUnapprovedEventsAsync(int currentPage = 1, int eventsPerPage = int.MaxValue)
        {
            var unapproved = await this.dbContext
                .Events
                .Include(e=>e.Creator)
                .Where(e => e.IsApproved == false && e.IsDeleted == false)
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
    }
}
