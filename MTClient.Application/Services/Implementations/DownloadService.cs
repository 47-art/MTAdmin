using Dapper;
using MTAdmin.Shared.Models;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Interfaces;
using System.Data;

namespace MTClient.Application.Services.Implementations
{
    public class DownloadService : BaseService, IDownloadService
    {
        private readonly IHttpContextService _contextService; 
        public DownloadService(DapperContext context,IHttpContextService contextService) : base(context)
        {
            _contextService = contextService;
        }
        public async Task<Response<string>> GetTemplateFilePath(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    string ip = _contextService.GetIPAddress();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int32);
                    parameters.Add("@Identity", _contextService.GetConnectionId(), DbType.String);
                    parameters.Add("@IPAddress", ip, DbType.String);
                    parameters.Add("@AddState",!string.IsNullOrEmpty(ip), DbType.Boolean);
                    var fileName = await connection.QueryAsync<string>("[dbo].[GET_TEMPLATE_FILENAME_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<string>.Success(Path.Combine("siteimg", fileName.FirstOrDefault()));
                }
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex, default, "Error");
            }
        }
    }
}
