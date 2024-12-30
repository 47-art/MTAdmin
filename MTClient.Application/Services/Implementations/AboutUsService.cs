using MTAdmin.Shared.Models;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Interfaces;

namespace MTClient.Application.Services.Implementations
{
    public class AboutUsService : BaseService, IAboutUsService
    {
        public AboutUsService(DapperContext context) : base(context)
        {

        }
        public async Task<Response<MetaDataModel>> GetAboutUsMetaData()
        {
            return await GetPageMetaData(MTAdmin.Shared.Enums.ClientModule.AboutUs);
        }
    }
}
