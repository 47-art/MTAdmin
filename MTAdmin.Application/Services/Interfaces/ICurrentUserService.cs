namespace MTAdmin.Application.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string GetFullName();
        string GetRoles();
        string GetUserEmail();
        string GetUserId();
        bool IsUserAuthenticated();
    }
}
