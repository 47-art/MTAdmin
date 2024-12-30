using MTAdmin.Shared.Models;

namespace MTClient.Application.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<Response<MetaDataModel>> GetAboutUsMetaData();
    }
}
