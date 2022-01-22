namespace Trails.Web.Areas.Administration.Services.Administration
{
    public interface IAdministrationService
    {
        Task<int> GetUnapprovedEventsCount();
    }
}
