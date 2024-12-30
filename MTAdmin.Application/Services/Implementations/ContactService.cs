using MTAdmin.Application.Services.Interfaces;
using MTAdmin.Core.Interfaces;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;

namespace MTAdmin.Application.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _uof;
        public ContactService(IUnitOfWork uof) => _uof = uof;

        public async Task<Response<PagedList<ContactVM>>> GetList(ContactParameters @params) => await _uof.ContactRepo.GetList(@params);
        public async Task<Response<ContactMessageVM>> GetContactMessage(int Id) => await _uof.ContactRepo.GetContactMessage(Id);
    }
}
