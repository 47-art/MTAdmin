using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Core.Entities.Contact
{
    public interface IContactRepo
    {
        Task<Response<ContactMessageVM>> GetContactMessage(int Id);
        Task<Response<PagedList<ContactVM>>> GetList(ContactParameters @params);
    }
}
