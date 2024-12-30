namespace MTClient.Application.Services.Interfaces
{
    public interface IHttpContextService
    {
        string GetConnectionId();
        (string connectionId, string ip) GetConnectionIdAndIP();
        string GetIPAddress();
    }
}
