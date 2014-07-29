using AutoMapper;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class UseCaseController : Controller
    {
        private readonly IUseCaseQueryService _useCaseQueryService;
        public UseCaseController(IUseCaseQueryService useCaseQueryService)
        {
            _useCaseQueryService = useCaseQueryService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var transactions = _useCaseQueryService.GetAll();
            var model = Mapper.Map<List<ApiUseCaseModel>>(transactions);
            return View(model);
        }

      
    }
}