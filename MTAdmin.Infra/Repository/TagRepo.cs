using Dapper;
using MTAdmin.Core.Entities.Tag;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using System.Data;

namespace MTAdmin.Infra.Repository
{
    public class TagRepo : BaseRepository, ITagRepo
    {
        public TagRepo(DapperContext context,string userId) : base(context) { CurrentUserId = userId; }

        public async Task<Response<PagedList<TagPageVM>>> GetTags(TagParameters @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    var response = await connection.QueryAsync<TagPageVM>("[dbo].[LIST_TAGS]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    return Response<TagPageVM>.ToPagedResponse(response.ToArray(), res, @params.PageNumber, @params.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Response<PagedList<TagPageVM>>.Error(ex, null, "Error");
            }
        }
        public async Task<Response<int>> CreateTag(Tag tag)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", tag.Name, DbType.String);
                    parameters.Add("@Slug", tag.Slug, DbType.String);
                    parameters.Add("@IsActive", tag.IsActive, DbType.Boolean);
                    AddCreatedBy(parameters);
                    await connection.QueryAsync("[dbo].[ADD_TAG]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("Id"),"Tag created");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<Tag>> GetTagById(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    var result = await connection.QueryFirstOrDefaultAsync<Tag>("[dbo].[GET_TAG]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<Tag>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return Response<Tag>.Error(ex);
            }
        }
        public async Task<Response<int>> UpdateTag(Tag tag)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, tag.Id);
                    parameters.Add("@Name", tag.Name, DbType.String);
                    parameters.Add("@Slug", tag.Slug, DbType.String);
                    parameters.Add("@IsActive", tag.IsActive, DbType.Boolean);
                    AddModifiedBy(parameters);
                    await connection.QueryAsync("[dbo].[Update_TAG]", parameters, commandType: CommandType.StoredProcedure);
                    int rowsAffected = parameters.Get<int>("RowsAffected");
                    return Response<int>.Success(rowsAffected,"Tag updated");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<bool>> ActiveInActive(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    AddModifiedBy(parameters);
                    parameters.Add("@IsActive", null, DbType.Boolean, ParameterDirection.Output);
                    var result = await connection.QueryAsync<bool>("[dbo].[ACTIVE_INACTIVE_TAG]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<bool>.Success(parameters.Get<bool>("@IsActive"));
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Error(ex);
            }
        }
        public async Task<Response<int>> DeleteTag(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    AddModifiedBy(parameters);
                    var result = await connection.QueryAsync<bool>("[dbo].[DELETE_TAG]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),"Tag deleted");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetTagsDropdownValues()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<IdNameVM<int>>("[dbo].[GET_TAGS_DROPDOWN]", commandType: CommandType.StoredProcedure);
                    return Response<IReadOnlyList<IdNameVM<int>>>.Success(result.ToList());
                }
            }
            catch (Exception ex)
            {
                return Response<IReadOnlyList<IdNameVM<int>>>.Error(ex);
            }
        }
    }
}
