using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EdFiValidation.ApiProxy.Core.Queries;
using EdFiValidation.ApiProxy.Models;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class UseCaseController : Controller
    {
        private readonly IUseCaseQueryService _useCaseQueryService;
        public UseCaseController(IUseCaseQueryService useCaseQueryService)
        {
            _useCaseQueryService = useCaseQueryService;
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var transactions = _useCaseQueryService.GetAll();
            var model = Mapper.Map<List<ApiUseCaseModel>>(transactions);
            return View(model);
        }

      
    }
}