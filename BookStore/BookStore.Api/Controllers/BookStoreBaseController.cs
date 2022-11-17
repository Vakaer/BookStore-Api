using BookStore.WebContracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BookStore.Api.Controllers
{
    public class BookStoreBaseController : Controller
    {
        private readonly ILogger _logger;

        public BookStoreBaseController(ILogger logger): base()
        {
            _logger = logger;
        }
        
        protected virtual ActionResult Respond(HttpStatusCode status)
        {
            if (status == HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }
            else if(status == HttpStatusCode.Unauthorized)
            {
                return Unauthorized();  
            }
            return new StatusCodeResult((int)status);

        }
        protected virtual ActionResult Respond(HttpStatusCode status, List<ServiceError> errors)
        {
            string content = JsonConvert.SerializeObject(errors);
            return new ContentResult
            {
                StatusCode = (int)status,
                Content = content,
                ContentType = "application/json"
            };
        }
        protected virtual ActionResult Respond(ServiceResult serviceResult)
        {
            ActionResult result;
            if (serviceResult.ErrorMessages.Any())
            {
                 result = Respond(serviceResult.Status,serviceResult.ErrorMessages);
            }
            else
            {
                result = Respond(serviceResult.Status);
            }
            return result;
        }
    }
}
