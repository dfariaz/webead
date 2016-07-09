using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Controllers;
using WebEAD.Areas.EAD.Models;
using WebEAD.Areas.EAD.Application;
using WebEAD.Repository;

namespace WebEAD.Areas.EAD.Controllers
{
    [HandleError]
    public class CoursesController : MasterController
    {
        private AccountRepository acr;
        private NoticeApplication notice;
        private ForumApplication forum;
        private long idDisciplina;

        public CoursesController()
        {
            acr = new AccountRepository();
            notice = new NoticeApplication();
            forum = new ForumApplication();
        }

        // GET: EAD/Courses
        public ActionResult Index(long id)
        {
            string raalunos = acr.getStudentLogged().ToString();
            if(raalunos != "")
            {
                idDisciplina = id;
                List<NoticeMetadado> avisos = notice.ListAll(id, raalunos);
                ViewBag.IDDisciplina = id;
                return View(avisos);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult Forum(long id)
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                List<ForumMetadado> topicos = forum.ListTopics(id);
                ViewBag.IDDisciplina = id;
                return View(topicos);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult TopicMessages(long idT, long idD)
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                ViewBag.IDDisciplina = idD;
                ViewBag.idT = idT.ToString();
                ViewBag.idD = idD.ToString();
                ViewBag.idL = raalunos;
                return View();
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TopicMessages(MessagesMetadado mensagem)
        {
            if (ModelState.IsValid)
            {
                bool retorno = forum.SaveMessage(mensagem.idtopico, mensagem.idlogin, mensagem.fptg_mensagem);
                if (!retorno)
                {
                    this.ShowMessageError("Mensagem não Enviada! Entre em contato com o Suporte.");
                }
                else
                {
                    this.ShowMessageInfo("Mensagem Enviada!");
                }
            }
            ViewBag.IDDisciplina = mensagem.iddisc;
            string raaluno = acr.getStudentLogged().ToString();
            ViewBag.idT = mensagem.idtopico;
            ViewBag.idD = mensagem.iddisc;
            ViewBag.idL = raaluno;
            return View(mensagem);
        }

        public ActionResult MessageFixed(long idT, long idD)
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                List<TopicsMetadado> mensagensf = forum.ListMessageFixed(idT, idD);
                return PartialView(mensagensf);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult Messages(long idT, long idD)
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                List<TopicsMetadado> mensagens = forum.ListMessages(idT, idD);
                return PartialView(mensagens);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }
    }
}