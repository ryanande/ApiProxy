using AutoMapper;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiLogItemQueryService _apiLogItemQueryService;
        public HomeController(IApiLogItemQueryService apiLogItemQueryService)
        {
            _apiLogItemQueryService = apiLogItemQueryService;
        }


        public ActionResult Index(string sessionId = "")
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                return View(new List<ApiLogModel>());

            var transactions = _apiLogItemQueryService.GetOnSessionId(sessionId);
            var model = Mapper.Map<List<ApiLogModel>>(transactions);
            return View(model);
        }
    }
}
