using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels.Client;

namespace MTClient.Application.Services.Interfaces
{
    public interface IContactService
    {
        Task<Response<int>> AddContact(ContactVM vm);
        Task<Response<MetaDataModel>> GetContactPageMetaData();
    }
}
