using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Repository;
using WebEAD.Areas.EAD.Models;
using WebEAD.Areas.EAD.Application;
using System.IO;
using Rotativa;

namespace WebEAD.Areas.EAD.Controllers
{
    public class ContentController : Controller
    {
        private ContentApplication content;
        private AccountRepository acr;

        public ContentController()
        {
            content = new ContentApplication();
            acr = new AccountRepository();
        }

        // GET: EAD/Content
        public ActionResult Index(String id)
        {
            ViewBag.IDDisciplina = id;
            return View();
        }

        public ActionResult ContentLink(String id)
        {
            String eus_login = acr.getStudentLogged().ToString();
            if (eus_login != "")
            {
                List<ContentMetadado> list = content.ListContent(id);
                return PartialView(list);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult ContentDinamic(String id)
        {
            var listDinamic = content.ContentDinamic(id);
            return PartialView(listDinamic);
        }

        [HttpGet]
        public ActionResult PDFViewer(String Filename)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Archives/" + Filename;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = Filename,
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }
    }
}