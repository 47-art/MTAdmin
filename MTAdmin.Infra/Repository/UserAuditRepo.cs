using Dapper;
using MTAdmin.Core.Entities.UserAudit;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using System.Data;

namespace MTAdmin.Infra.Repository
{
    public class UserAuditRepo : BaseRepository,IUserAuditRepo
    {
        public UserAuditRepo(DapperContext context, string userId):base(context)
        {
            CurrentUserId = userId;     
        }
        public async Task<Response<PagedList<UserAuditVM>>> GetUserAudits(UserAuditParameters @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    parameters.Add("@EventId", @params.EventId, DbType.Int32);
                    var response = await connection.QueryAsync<UserAuditVM>("[dbo].[LIST_USER_AUDITS]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    return Response<UserAuditVM>.ToPagedResponse(response.ToArray(), res, @params.PageNumber, @params.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Response<PagedList<UserAuditVM>>.Error(ex, null, "Error");
            }
        }
    }
}
