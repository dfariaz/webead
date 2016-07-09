using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Controllers;
using WebEAD.Areas.Extranet.Application;
using WebEAD.Areas.Extranet.Models;
using WebEAD.Repository;

namespace WebEAD.Areas.Extranet.Controllers
{
    public class MainController : MasterController
    {
        AccountRepository acr;
        MainApplication main;
        public MainController(){
            acr = new AccountRepository();
            main = new MainApplication();
        }

        // GET: Extranet/Main
        public ActionResult Index()
        {
            String Login = acr.getStudentLogged().ToString();
            if(Login != "")
            {
                var principal = main.ListTeacher(Login);
                return View(principal);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }
    }
}