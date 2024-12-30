using Dapper;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels.Client;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Interfaces;
using System.Data;

namespace MTClient.Application.Services.Implementations
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpContextService _contextService;
        public CategoryService(DapperContext context,IHttpContextService contextService) : base(context)
        {
            _contextService = contextService;
        }
        public async Task<Response<ListCategoryVM>> GetCategories(CategoryParams @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    ListCategoryVM vm = new ListCategoryVM() { FilterBy = @params.FilterBy };
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    parameters.Add("@FilterBy", @params.FilterBy, DbType.Int32);
                    var response = await connection.QueryAsync<MTAdmin.Shared.ViewModels.Client.CategoryVM>("[dbo].[LIST_CATEGORY_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    var responseAry = response.ToArray();

                    int i = 0;
                    while (i < responseAry.Length)
                    {
                        responseAry[i].Img = GetFileUrl(responseAry[i].Img);
                        i++;
                    }
                    vm.CategoryList = new PagedList<MTAdmin.Shared.ViewModels.Client.CategoryVM>(responseAry, res, @params.PageNumber, @params.PageSize);
                    return Response<ListCategoryVM>.Success(vm);
                }
            }
            catch (Exception ex)
            {
                return Response<ListCategoryVM>.Error(ex, default, "Error");
            }
        }
        public async Task<Response<MetaDataModel>> GetCategoryMetaData()
        {
            return await GetPageMetaData(MTAdmin.Shared.Enums.ClientModule.Categories);
        }
        public async Task<Response<MetaDataModel>> GetCategoryMetaDataById(int id)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    TemplateByIdVM vm = new TemplateByIdVM();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int32);
                    var response = await connection.QueryAsync<MetaDataModel>("[dbo].[GET_CATEGORYMETADATA_BY_ID_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<MetaDataModel>.Success(response.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return Response<MetaDataModel>.Error(ex, default, "Error");
            }
        }

        public Task<Response<int>> SaveCatogyPostViewState(int catId)
        {
            return base.SavePostStatestic(PostTypeEnum.Category, PostEventTypeEnum.View,_contextService.GetIPAddress(),_contextService.GetConnectionId(),catId);
        }
    }
}
