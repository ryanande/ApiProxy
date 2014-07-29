using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdFiValidation.ApiProxy.Controllers
{
    public class ValidateSessionController : Controller
    {

        [Route("validatesession/{id}")]
        public ActionResult Index(string id)
        {
            return View();
        }
    }
}