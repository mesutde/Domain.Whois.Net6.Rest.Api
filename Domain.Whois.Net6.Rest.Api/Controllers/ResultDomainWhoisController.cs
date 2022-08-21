using Domain.Whois.Net6.Rest.Api.Model;
using Domain.Whois.Net6.Rest.Api.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Whois.Net6.Rest.Api.Controllers
{
    [Route("GetResultDomainWhois")]
    [ApiController]
    public class ResultDomainWhoisController : Controller
    {
        [HttpGet(Name = "GetResultDomainWhois")]
        public Response<DomainWhoisModel> Get(string url)
        {
            Response<DomainWhoisModel> retVal = new Response<DomainWhoisModel>();

            string hostName = Helper.GetDomainFromUrl(url);
            DomainWhoisModel DWM = DomainWhoisHelper.GetResultDomainWhois(hostName);

            if (DWM.DomainName != null)
            {
                retVal.Result = true;
                retVal.ResultCode = 200;
                retVal.Message = "İşlem Başarılı";
                retVal.Comment = " ";
                retVal.Data = DWM;
                retVal.UpdateTime = DateTime.Now.ToString();
            }
            else
            {
                retVal.Result = false;
                retVal.ResultCode = -1;
                retVal.Message = "Domain Kayıtlı Değil";
                retVal.Comment = "Veriye Ulaşılamadı.";
                retVal.UpdateTime = DateTime.Now.ToString();
            }

            return retVal;
        }
    }
}