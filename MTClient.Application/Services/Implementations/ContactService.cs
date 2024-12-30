using Dapper;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels.Client;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Interfaces;
using System.Data;

namespace MTClient.Application.Services.Implementations
{
    public class ContactService : BaseService, IContactService
    {
        public ContactService(DapperContext context) : base(context)
        {
            
        }
        public async Task<Response<int>> AddContact(ContactVM vm)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", vm.Name, DbType.String);
                    parameters.Add("@Email", vm.Email, DbType.String);
                    parameters.Add("@ContactMessage", vm.Message, DbType.String);
                    parameters.Add("@IPAddress", vm.IPAddress, DbType.String);
                    parameters.Add("@RowsAffected", 0, DbType.Int32, ParameterDirection.Output);
                    parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
                    await connection.QueryAsync("[dbo].[ADD_CONTACT_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("Id"), "Contact added");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }

        public async Task<Response<MetaDataModel>> GetContactPageMetaData()
        {
            return await GetPageMetaData(MTAdmin.Shared.Enums.ClientModule.Contact);
        }
    }
}
