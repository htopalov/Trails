using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Trails.Api.Models;
using Trails.Data;
using Trails.Security;

namespace Trails.Api.Filters
{
    public class AuthKeyFilter : IAsyncActionFilter
    {
        private const string AUTHKEYNAME = "AuthKey";
        private readonly TrailsDbContext dbContext;

        public AuthKeyFilter(TrailsDbContext dbContext) 
            => this.dbContext = dbContext;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isKeyProvided = context.
                HttpContext
                .Request
                .Headers
                .TryGetValue(AUTHKEYNAME, out var authKeyHeader);

            if (!isKeyProvided)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Missing Header"
                };
                return;
            }

            //model binding and model state validation happens automatic
            //when controller is marked as api controller so no need to check for param

            var requestParameter = context.ActionArguments["beaconDataDto"] as BeaconDataDtoPost;

            var beacon = await this.dbContext
                .Beacons
                .FirstOrDefaultAsync(b=>b.Imei == requestParameter.BeaconImei);

            if (beacon == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 404,
                    Content = "Beacon not found"
                };
                return;
            }

            var hashedKey = SecurityProvider
                .KeyHasher(authKeyHeader.ToString());
            
            if (hashedKey != beacon.KeyHash)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Invalid Key"
                };
                return;
            }

            await next();
        }
    }
}
