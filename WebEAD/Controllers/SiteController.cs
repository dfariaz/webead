using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEAD.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuemSomos()
        {
            return View();
        }

        public ActionResult Curso()
        {
            return View();
        }

        public ActionResult FaleConosco()
        {
            return View();
        }

        public ActionResult Pagedoesnotexist()
        {
            return View();
        }

        public ActionResult ErrorUrl()
        {
            return View();
        }
    }
}