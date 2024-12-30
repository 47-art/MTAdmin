using Dapper;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using MTAdmin.Shared.ViewModels;
using MTAdmin.Shared.ViewModels.Client;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Interfaces;
using System.Data;

namespace MTClient.Application.Services.Implementations
{
    public class TemplateService : BaseService, ITemplateService
    {
        private readonly IHttpContextService _contextService;
        public TemplateService(DapperContext context, IHttpContextService contextService) : base(context)
        {
            _contextService = contextService;
        }
        public async Task<Response<ListTemplateVM>> GetTemplates(TemplateParams @params)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    ListTemplateVM vm = new ListTemplateVM()
                    {
                        CategoryId = @params.CategoryId,
                        FilterById = @params.FilterBy,
                    };
                    
                    DynamicParameters parameters = new DynamicParameters();
                    AddQueryParameters(parameters, @params);
                    parameters.Add("@CategoryId", @params.CategoryId > 0 ? @params.CategoryId : null, DbType.Int32);
                    parameters.Add("@FilterBy", @params.FilterBy, DbType.Int32);
                    var response = await connection.QueryAsync<MTAdmin.Shared.ViewModels.Client.TemplateVM>("[dbo].[LIST_Templates_Client]", parameters, commandType: CommandType.StoredProcedure);
                    var res = parameters.Get<int>("@TotalCount");
                    var responseAry = response.ToArray();

                    int i = 0;
                    while (i < responseAry.Length)
                    {
                        responseAry[i].Thumbnail = GetFileUrl(responseAry[i].Thumbnail);
                        responseAry[i].FileName = GetFileUrl(responseAry[i].FileName);
                        i++;
                    }
                    vm.TemplateList = new PagedList<MTAdmin.Shared.ViewModels.Client.TemplateVM>(responseAry, res, @params.PageNumber, @params.PageSize);
                    if(@params.CategoryId > 0)
                    {
                        vm.CategorySlug = vm.TemplateList.Items.FirstOrDefault()?.CategorySlug;
                    }
                    return Response<ListTemplateVM>.Success(vm);
                }
            }
            catch (Exception ex)
            {
                return Response<ListTemplateVM>.Error(ex, default, "Error");
            }
        }
        public async Task<Response<IReadOnlyList<IdNameVM<int>>>> GetCategories()
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<IdNameVM<int>>("[dbo].[Get_Categories_Dropdown]", commandType: CommandType.StoredProcedure);
                return Response<IReadOnlyList<IdNameVM<int>>>.Success(result.ToArray());
            }
        }
        public async Task<Response<TemplateByIdVM>> GetTemplateById(int Id)
        {
            try
            {
                using(IDbConnection connection = _context.CreateConnection())
                {
                    string ip = _contextService.GetIPAddress();
                    TemplateByIdVM vm = new TemplateByIdVM();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", Id, DbType.Int32);
                    parameters.Add("@Identity", _contextService.GetConnectionId(), DbType.String);
                    parameters.Add("@IPAddress",ip , DbType.String);
                    parameters.Add("@AddState", !string.IsNullOrEmpty(ip), DbType.Boolean);
                    var response = await connection.QueryAsync<TemplateByIdVM>("[dbo].[GET_TEMPLATE_BY_ID_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    //var tags = await connection.QueryAsync<IdNameVM<int>>("[dbo].[GET_TEMPLATE_TAGS_CLIENT]", parameters, commandType: CommandType.StoredProcedure);
                    vm = response.FirstOrDefault();
                    //vm.Tags = tags.ToArray();
                    vm.Thumbnail = GetFileUrl(vm.Thumbnail);
                    vm.FileName = GetFileUrl(vm.FileName);
                    return Response<TemplateByIdVM>.Success(vm);
                }
            }
            catch(Exception ex)
            {
                return Response<TemplateByIdVM>.Error(ex, default, "Error");
            }
        }        
        public async Task<Response<MetaDataModel>> GetHomePageMetaData()
        {
            return await GetPageMetaData(MTAdmin.Shared.Enums.ClientModule.Home);
        }
        public async Task<Response<MetaDataModel>> GetSearchPageMetaData()
        {
            return await GetPageMetaData(MTAdmin.Shared.Enums.ClientModule.Search);
        }
        public async Task<Response<int>> SharePostStatestic(ShareTemplateVM vm)
        {
           return await base.SavePostStatestic(PostTypeEnum.Template,vm.ShareType, _contextService.GetIPAddress(), _contextService.GetConnectionId(), vm.Id);
        }
    }
}
