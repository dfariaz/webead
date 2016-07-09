using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Controllers;
using WebEAD.Repository;
using WebEAD.Areas.Extranet.Application;
using WebEAD.Areas.Extranet.Models;

namespace WebEAD.Areas.Extranet.Controllers
{
    public class NoticeController : MasterController
    {
        private MainApplication main;
        private NoticeApplication notice;
        private AccountRepository acr;

        public NoticeController()
        {
            main = new MainApplication();
            notice = new NoticeApplication();
            acr = new AccountRepository();
        }

        // GET: Extranet/Notice
        public ActionResult Notices()
        {
            String eus_login = acr.getStudentLogged().ToString();
            List<NoticeMetadado> avisos = notice.ListNotices(eus_login);
            return View(avisos);
        }

        public ActionResult GridNotices()
        {
            String eus_login = acr.getStudentLogged().ToString();
            List<NoticeMetadado> a = notice.ListNotices(eus_login);
            return PartialView(a);
        }

        public ActionResult GridNoticesFilter(String disciplina)
        {
            long disc = 0;
            if (disciplina != "0")
            {
                disc = int.Parse(disciplina);
            }
            List<NoticeMetadado> filtro = notice.ListNoticesFilter(disc);
            return View(filtro);
        }

        public ActionResult CreateNotice()
        {
            String eus_login = acr.getStudentLogged().ToString();
            String id = notice.GetID(eus_login);
            ViewBag.IDPF = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateNotice(NoticeMetadado top)
        {
            Boolean resultado = notice.SaveNotice(top.av_titulo, top.av_mensagem, top.av_tm_id.ToString(), top.av_dp_id.ToString(), top.av_pf_id.ToString(), top.av_geral.ToString());
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult EditNotice(long id)
        {
            var fedit = notice.EditNotice(id.ToString());
            ViewBag.ID = fedit.av_id;
            return View(fedit);
        }

        [HttpPost]
        public ActionResult EditNotice(NoticeMetadado top)
        {
            Boolean resultado = notice.UpdateNotice(top.av_id.ToString(), top.av_titulo, top.av_mensagem, top.av_tm_id.ToString(), top.av_dp_id.ToString(), top.av_geral.ToString());
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult FormEditNotice(long id)
        {
            var fedit = notice.EditNotice(id.ToString());
            ViewBag.IDD = fedit.av_dp_id;
            ViewBag.IDT = fedit.av_tm_id;
            return View(fedit);
        }

        public ActionResult ClassDropDownList()
        {
            String eus_login = acr.getStudentLogged().ToString();
            List<ClassMetadado> turma = main.ListClass(eus_login);
            return PartialView(turma);
        }

        public ActionResult SubjectDropDownList()
        {
            List<SubjectMetadado> d = main.ListSubject();
            return PartialView(d);
        }

        public ActionResult SubjectDropDownListFilter()
        {
            List<SubjectMetadado> d = main.ListSubject();
            return PartialView(d);
        }
    }
}