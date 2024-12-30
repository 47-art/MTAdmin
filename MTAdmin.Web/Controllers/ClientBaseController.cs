using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;

namespace MTAdmin.Web.Controllers
{
    public class ClientBaseController : Controller
    {
        [NonAction]
        internal void AddMetaData(MetaDataModel model)
        {
            ViewData["MetaInfo"] = model;
        }
        [NonAction]
        internal (string conId,string ip) GetIPAndConnectionId()
        {
           string connectionId = HttpContext.Connection.Id;
            string ip = string.Empty;
            if(HttpContext.Connection.RemoteIpAddress != null)
            {
                ip = HttpContext.Connection.RemoteIpAddress.ToString();
            }
           return (connectionId,ip);
        }
        [NonAction]
        internal TemplateTypeEnum GetFileContentType(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType);
            if (contentType.Contains("video"))
                return TemplateTypeEnum.Video;
            else if (contentType.Contains("image"))
                return TemplateTypeEnum.Image;
            else
                return TemplateTypeEnum.Undefined;
        }
    }
}
