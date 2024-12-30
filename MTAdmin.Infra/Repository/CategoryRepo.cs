using Dapper;
using MTAdmin.Core.Entities.Category;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Repository.Comman;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using System.Data;

namespace MTAdmin.Infra.Repository
{
    public class CategoryRepo : BaseRepository, ICategoryRepo
    {
        public CategoryRepo(DapperContext context, string userId) : base(context) { CurrentUserId = userId; }
        public async Task<Response<PagedList<CategoryPageVM>>> GetCategories(CategoryParameters @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    var response = await connection.QueryAsync<CategoryPageVM>("[dbo].[LIST_CATEGORY]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    return Response<CategoryPageVM>.ToPagedResponse(response.ToArray(), res, @params.PageNumber, @params.PageSize);
                }
            }
            catch (Exception ex)
            {
                return Response<PagedList<CategoryPageVM>>.Error(ex, null, "Error");
            }
        }
        public async Task<Response<int>> CreateCategory(CreateCategoryVM category)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", category.Name, DbType.String);
                    parameters.Add("@Desc", category.Desc, DbType.String);
                    parameters.Add("@Slug", category.Slug, DbType.String);
                    parameters.Add("@Img", category.Img, DbType.String);
                    parameters.Add("@ImgAlt", category.ImgAlt, DbType.String);
                    parameters.Add("@MetaDesc", category.MetaDesc, DbType.String);
                    parameters.Add("@MetaKeywords", category.MetaKeywords, DbType.String);
                    parameters.Add("@IsActive", category.IsActive, DbType.Boolean);
                    AddCreatedBy(parameters);
                    await connection.QueryAsync("[dbo].[ADD_CATEGORY]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("Id"),"Category created");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<CategoryVM>> GetCategoryById(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    var result = await connection.QueryFirstOrDefaultAsync<CategoryVM>("[dbo].[GET_CATEGORY]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<CategoryVM>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return Response<CategoryVM>.Error(ex);
            }
        }
        public async Task<Response<int>> UpdateCategory(CategoryVM category)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, category.Id);
                    parameters.Add("@Name", category.Name, DbType.String);
                    parameters.Add("@Desc", category.Desc, DbType.String);
                    parameters.Add("@Slug", category.Slug, DbType.String);
                    parameters.Add("@ImgAlt", category.ImgAlt, DbType.String);
                    parameters.Add("@MetaDesc", category.MetaDesc, DbType.String);
                    parameters.Add("@MetaKeywords", category.MetaKeywords, DbType.String);
                    parameters.Add("@IsActive", category.IsActive, DbType.Boolean);
                    AddModifiedBy(parameters);
                    await connection.QueryAsync("[dbo].[Update_Category]", parameters, commandType: CommandType.StoredProcedure);
                    int rowsAffected = parameters.Get<int>("RowsAffected");
                    return Response<int>.Success(rowsAffected,"Category updated");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<int>> DeleteCategory(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, id);
                    AddModifiedBy(parameters);
                    var result = await connection.QueryAsync<bool>("[dbo].[Delete_CATEGORY]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("RowsAffected"),"Category deleted");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<int>> UpdateCategoryImg(int Id, string filePath)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    AddId(parameters, Id);
                    AddModifiedBy(parameters);
                    parameters.Add("@Img", filePath, DbType.String);
                    await connection.QueryAsync("[dbo].[Update_CategoryImage]", parameters, commandType: CommandType.StoredProcedure);
                    int rowsAffected = parameters.Get<int>("RowsAffected");
                    return Response<int>.Success(rowsAffected, "Category Image updated");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetCategoriesDropDownValues()
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<IdNameVM<int>>("[dbo].[Get_Categories_Dropdown]", commandType: CommandType.StoredProcedure);
                    return Response<IReadOnlyList<IdNameVM<int>>>.Success(result.ToList());
                }
            }
            catch (Exception ex)
            {
                return Response<IReadOnlyList<IdNameVM<int>>>.Error(ex);
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
                    var result = await connection.QueryAsync<bool>("[dbo].[ACTIV_INACTIVE_CATEGORY]", parameters, commandType: CommandType.StoredProcedure);
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
