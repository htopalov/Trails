namespace Trails.Api.Hub
{
    public interface IBroadcastHub
    {
        Task ReceiveData(string data);
    }
}
