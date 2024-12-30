using Dapper;
using MTAdmin.Core.Entities.Language;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using System.Data;

namespace MTAdmin.Infra.Repository
{
    public class LanguageRepository : BaseRepository, ILanguageRepo
    {
        public LanguageRepository(DapperContext context,string userId) : base(context) { CurrentUserId = userId; }
        public async Task<Response<PagedList<LanguagePageVM>>> GetLanguages(LanguageParameters @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    var response = await connection.QueryAsync<LanguagePageVM>("[dbo].[LIST_LANGUAGES]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    return Response<LanguagePageVM>.ToPagedResponse(response.ToArray(), res, @params.PageNumber, @params.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Response<PagedList<LanguagePageVM>>.Error(ex, null, "Error");
            }
        }
        public async Task<Response<int>> CreateLanguage(Language language)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", language.Name, DbType.String);
                    parameters.Add("@NativeName", language.NativeName, DbType.String);
                    parameters.Add("@Slug", language.Slug, DbType.String);
                    parameters.Add("@IsActive", language.IsActive, DbType.Boolean);
                    AddCreatedBy(parameters);
                    await connection.QueryAsync("[dbo].[ADD_LANGUAGE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("Id"),"Language created");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<Language>> GetLanguageById(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    var result = await connection.QueryFirstOrDefaultAsync<Language>("[dbo].[GET_LANGUAGE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<Language>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return Response<Language>.Error(ex);
            }
        }
        public async Task<Response<int>> UpdateLanguage(Language language)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, language.Id);
                    parameters.Add("@Name", language.Name, DbType.String);
                    parameters.Add("@NativeName", language.NativeName, DbType.String);
                    parameters.Add("@Slug", language.Slug, DbType.String);
                    parameters.Add("@IsActive", language.IsActive, DbType.Boolean);
                    AddModifiedBy(parameters);
                    await connection.QueryAsync("[dbo].[Update_LANGUAGE]", parameters, commandType: CommandType.StoredProcedure);
                    int rowsAffected = parameters.Get<int>("RowsAffected");
                    return Response<int>.Success(rowsAffected,"Language updated");
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
                    var result = await connection.QueryAsync<bool>("[dbo].[ACTIVE_INACTIVE_LANGUAGE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<bool>.Success(parameters.Get<bool>("@IsActive"));
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Error(ex);
            }
        }
        public async Task<Response<int>> DeleteLanguage(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    AddModifiedBy(parameters);
                    var result = await connection.QueryAsync<bool>("[dbo].[Delete_LANGUAGE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),"Language deleted");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetLanguagesDropdownValues()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<IdNameVM<int>>("[dbo].[Get_Languages_Dropdown]", commandType: CommandType.StoredProcedure);
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
