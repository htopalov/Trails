namespace Trails.Services.Administration
{
    public interface IAdministrationService
    {
        Task<int> GetUnapprovedEventsCount();
    }
}
