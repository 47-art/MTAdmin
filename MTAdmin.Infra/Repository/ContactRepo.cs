using Dapper;
using MTAdmin.Core.Entities.Contact;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using System.Data;

namespace MTAdmin.Infra.Repository
{
    public class ContactRepo : BaseRepository, IContactRepo
    {
        public ContactRepo(DapperContext context) : base(context) { }
        public async Task<Response<PagedList<ContactVM>>> GetList(ContactParameters @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    var response = await connection.QueryAsync<ContactVM>("[dbo].[LIST_ENQUIRIES]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    return Response<ContactVM>.ToPagedResponse(response.ToArray(), res, @params.PageNumber, @params.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Response<PagedList<ContactVM>>.Error(ex, null, "Error");
            }
        }
        public async Task<Response<ContactMessageVM>> GetContactMessage(int Id)
        {
            try
            {
                using(IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters,Id);
                    var response = await connection.QueryAsync<ContactMessageVM>("[dbo].[GET_CONTACT_MESSAGE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<ContactMessageVM>.Success(response.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Response<ContactMessageVM>.Error(ex, null, "Error");
            }
        }
    }    
}
