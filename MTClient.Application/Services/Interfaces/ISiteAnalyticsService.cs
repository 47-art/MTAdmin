using MTAdmin.Shared.Models;

namespace MTClient.Application.Services.Interfaces
{
    public interface ISiteAnalyticsService
    {
        Task<Response<int>> SaveSiteRequest(SiteRequest request);
    }
}
