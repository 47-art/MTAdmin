using Dapper;
using MTAdmin.Core.Entities.Template;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using System.Data;

namespace MTAdmin.Infra.Repository
{
    public class TemplateRepo : BaseRepository, ITemplateRepo
    {
        public TemplateRepo(DapperContext context, string userId) : base(context) { CurrentUserId = userId; }
        public async Task<Response<PagedList<TemplatePageVM>>> GetTemplates(TemplateParameters @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    var response = await connection.QueryAsync<TemplatePageVM>("[dbo].[LIST_Templates]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    return Response<TemplatePageVM>.ToPagedResponse(response.ToArray(), res, @params.PageNumber, @params.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Response<PagedList<TemplatePageVM>>.Error(ex, null, "Error");
            }
        }
        public async Task<Response<int>> AddTemplate(CreateTemplateVM vm)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", vm.Name, DbType.String);
                    parameters.Add("@Slug", vm.Slug, DbType.String);
                    parameters.Add("@Desc", vm.Desc, DbType.String);
                    parameters.Add("@LanguageId", vm.LanguageId, DbType.Int32);
                    parameters.Add("@CategoryId", string.Join(",", vm.CategoryIds), DbType.String);
                    parameters.Add("@Tags", string.Join(",", vm.TagIds), DbType.String);
                    parameters.Add("@MetaDesc", vm.MetaDesc, DbType.String);
                    parameters.Add("@MetaKeywords", vm.MetaKeywords, DbType.String);
                    parameters.Add("@Thumbnail", vm.Thumbnail, DbType.String);
                    parameters.Add("@ThumbnailAlt", vm.ThumbnailAlt, DbType.String);
                    parameters.Add("@FileName", vm.FileName, DbType.String);                                                                                
                    parameters.Add("@IsActive", vm.IsActive, DbType.Boolean);
                    AddCreatedBy(parameters);
                    await connection.QueryAsync("[dbo].[ADD_Template]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("Id"),"Template added");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex, default, "Error");
            }
        }
        public async Task<Response<int>> EditTemplate(EditTemplateVM vm)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", vm.Id, DbType.Int32);
                    parameters.Add("@Name", vm.Name, DbType.String);
                    parameters.Add("@Slug", vm.Slug, DbType.String);
                    parameters.Add("@Desc", vm.Desc, DbType.String);
                    parameters.Add("@LanguageId", vm.LanguageId, DbType.Int32);
                    parameters.Add("@CategoryId", string.Join(",", vm.CategoryIds), DbType.String);
                    parameters.Add("@Tags", string.Join(",", vm.TagIds), DbType.String);
                    parameters.Add("@MetaDesc", vm.MetaDesc, DbType.String);
                    parameters.Add("@MetaKeywords", vm.MetaKeywords, DbType.String);                    
                    parameters.Add("@IsActive", vm.IsActive, DbType.Boolean);
                    AddModifiedBy(parameters);
                    await connection.QueryAsync("[dbo].[UPDATE_TEMPLATE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),"Template updated");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex, default, "Error");
            }
        }
        public async Task<Response<EditTemplateVM>> GetTemplateById(int Id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", Id, DbType.Int32);
                    var response = await connection.QueryAsync<EditTemplateVM>("[dbo].[GET_TEMPLATE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<EditTemplateVM>.Success(response.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Response<EditTemplateVM>.Error(ex, default, "Error");
            }
        }
        public async Task<Response<int>> DeleteTemplate(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    AddModifiedBy(parameters);
                    var result = await connection.QueryAsync<bool>("[dbo].[DELETE_TEMPLATE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),"Template deleted");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<int>> UpdateThumbnailFileName(EditTemplateThumbnailVM vm)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Thumbnail", vm.Thumbnail, DbType.String);
                    parameters.Add("@ThumbnailAlt", vm.ThumbnailAlt, DbType.String);
                    AddId(parameters, vm.Id);
                    AddModifiedBy(parameters);
                    var result = await connection.QueryAsync<bool>("[dbo].[UPDATE_TEMPLATE_THUMBNAIL]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),message:"Thumbnail updated");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<int>> UpdateTemplateFileName(EditTemplateFileVM vm)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@FileName", vm.FileName, DbType.String);
                    AddId(parameters, vm.Id);
                    AddModifiedBy(parameters);
                    var result = await connection.QueryAsync<bool>("[dbo].[UPDATE_TEMPLATE_FILE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),message:"Template updated");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<EditTemplateThumbnailVM>> GetTemplateThumbnail(int Id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters,Id);
                    var result = await connection.QueryAsync<EditTemplateThumbnailVM>("[dbo].[GET_THUMBNAIL_FILE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<EditTemplateThumbnailVM>.Success(result.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Response<EditTemplateThumbnailVM>.Error(ex);
            }
        }
        public async Task<Response<EditTemplateFileVM>> GetTemplateFile(int Id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, Id);
                    var result = await connection.QueryAsync<EditTemplateFileVM>("[dbo].[GET_TEMPLATE_FILE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<EditTemplateFileVM>.Success(result.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Response<EditTemplateFileVM>.Error(ex);
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
                    var result = await connection.QueryAsync<bool>("[dbo].[ACTIVE_INACTIVE_TEMPLATE]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<bool>.Success(parameters.Get<bool>("@IsActive"));
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Error(ex);
            }
        }
    }
}
