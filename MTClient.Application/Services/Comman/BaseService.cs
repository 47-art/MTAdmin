using Dapper;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using System.Data;

namespace MTClient.Application.Services.Comman
{
    public abstract class BaseService
    {
        internal readonly DapperContext _context;
        public BaseService(DapperContext context)
        {
            _context = context;
        }
        internal string GetFileUrl(string fileName) => $"/siteimg/{fileName}";
        internal DynamicParameters AddQueryParameters(DynamicParameters parameters, QueryStringParameters @params)
        {
            parameters.Add("@Query", @params.Query, DbType.String);
            parameters.Add("@PageNumber", @params.PageNumber, DbType.Int32);
            parameters.Add("@PageSize", @params.PageSize, DbType.Int32);
            parameters.Add("@SortColumn", @params.SortColumn, DbType.Int32);
            parameters.Add("@SortOrder", @params.SortOrder, DbType.Boolean);
            parameters.Add("@TotalCount", 0, DbType.Int32, ParameterDirection.Output);
            return parameters;
        }
        internal async Task<Response<MetaDataModel>> GetPageMetaData(ClientModule pageId)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PageId",pageId, DbType.Int32);
                    var response = await connection.QueryAsync<MetaDataModel>("[dbo].[GET_PAGEMETADATA_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<MetaDataModel>.Success(response.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Response<MetaDataModel>.Error(ex, default, "Error");
            }
        }
        internal async Task<Response<int>> SavePostStatestic(PostTypeEnum postType,PostEventTypeEnum postEvent,string ip,string conId, int? postId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    return Response<int>.Success(-1, "IP is excluded or not valid");
                }

                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PostId",postId, DbType.Int32);
                    parameters.Add("@PostType", postType, DbType.Int32);
                    parameters.Add("@PostEventType", postEvent, DbType.Int32);
                    parameters.Add("@IPAddress", ip, DbType.String);
                    parameters.Add("@Identity", conId, DbType.String);
                    parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
                    parameters.Add("@RowsAffected", 0, DbType.Int32, ParameterDirection.Output);
                    await connection.QueryAsync("[dbo].[SAVE_POSTSTATISTIC]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("@Id"));
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex,0, "Error");
            }
        }
    }
}
