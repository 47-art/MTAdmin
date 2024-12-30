using Dapper;
using MTAdmin.Infra.Dapper;
using MTAdmin.Shared.Models;
using System.Data;

namespace MTAdmin.Infra.Repository.Comman
{
    public abstract class BaseRepository
    {
        internal readonly DapperContext _context;
        public string CurrentUserId { get; set; }
        public BaseRepository(DapperContext context)
        {
            _context = context;
        }
        internal DynamicParameters AddId(DynamicParameters parameters, int Id)
        {
            parameters.Add("@Id", Id, DbType.Int32);
            return parameters;
        }
        internal DynamicParameters AddCreatedBy(DynamicParameters parameters)
        {
            parameters.Add("@CreatedBy", CurrentUserId, DbType.String);
            parameters.Add("@RowsAffected", 0, DbType.Int32, ParameterDirection.Output);
            parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
            return parameters;
        }
        internal DynamicParameters AddModifiedBy(DynamicParameters parameters)
        {
            parameters.Add("@ModifiedBy", CurrentUserId, DbType.String);
            parameters.Add("@RowsAffected", 0, DbType.Int32, ParameterDirection.Output);
            return parameters;
        }
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
    }
}
