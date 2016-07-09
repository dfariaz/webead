using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Controllers;
using WebEAD.Repository;
using WebEAD.Areas.EAD.Application;
using WebEAD.Areas.EAD.Models;

namespace WebEAD.Areas.EAD.Controllers
{
    [HandleError]
    public class StudentAreaController : MasterController
    {
        private AccountRepository acr;
        private ProfileApplication profile;
        private NoticeApplication notice;

        public StudentAreaController()
        {
            acr = new AccountRepository();
            profile = new ProfileApplication();
            notice = new NoticeApplication();
        }
        // GET: EAD/StudentArea
        public ActionResult Index()
        {
            String raalunos = acr.getStudentLogged().ToString();
            if(raalunos != "")
            {
                if(!profile.validStudent(raalunos))
                {
                    var perfil = profile.ListDataIndex(raalunos);
                    ViewBag.Nome = perfil.al_nome;
                    return View();
                }
                else
                {
                    return RedirectToAction("AlterPassword");
                }
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult BasicInfo()
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                List<ProfileMetadado> perfil = profile.ListData(raalunos);
                return PartialView(perfil);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult Notice()
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                List<NoticeMetadado> avisos = notice.ListAllGeneral();
                return PartialView(avisos);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult Subjects()
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                List<SubjectsMetadado> disciplina = profile.ListSubjects(raalunos);
                return PartialView(disciplina);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        public ActionResult AlterPassword()
        {
            string raalunos = acr.getStudentLogged().ToString();
            if(raalunos != "")
            {
                ViewBag.login = raalunos;
                return View();
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        [HttpPost]
        public ActionResult AlterPassword(ProfilePasswordMetadado perfil)
        {
            if (ModelState.IsValid)
            {
                bool resultado = profile.AlterPassword(perfil.p_eus_senha, perfil.p_eus_login);
                if (resultado)
                {
                    this.ShowMessageInfo("Senha Alterada com Sucesso!");
                }
                else
                {
                    this.ShowMessageError("A Senha não foi alterada!");
                }
            }
            return View(perfil);
        }

        public ActionResult UpdateData()
        {
            string raalunos = acr.getStudentLogged().ToString();
            if (raalunos != "")
            {
                var perfil = profile.ListDataIndex(raalunos);
                return View(perfil);
            }
            else
                return RedirectToAction("ErrorUrl", "Site", new { area = "" });
        }

        [HttpPost]
        public ActionResult UpdateData(ProfileMetadado perfil)
        {
            string ra = acr.getStudentLogged();
            string telefoneNew = perfil.telefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
            string celularNew = perfil.celular.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
            bool resultado = profile.UpdateData(ra, perfil.email, telefoneNew, celularNew);
            if (resultado)
            {
                this.ShowMessageInfo("Dados atualizados com Sucesso!");
            }
            else
            {
                this.ShowMessageError("Os dados não foram atualizados!");
            }
            return View(perfil);
        }
    }
}