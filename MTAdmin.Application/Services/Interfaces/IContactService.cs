using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Interfaces
{
    public interface IContactService
    {
        Task<Response<ContactMessageVM>> GetContactMessage(int Id);
        Task<Response<PagedList<ContactVM>>> GetList(ContactParameters @params);
    }
}
