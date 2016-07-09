using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Controllers;
using WebEAD.Areas.Extranet.Models;
using WebEAD.Areas.Extranet.Application;
using WebEAD.Repository;
using System.IO;

namespace WebEAD.Areas.Extranet.Controllers
{
    public class ContentController : MasterController
    {
        private MainApplication main;
        private ContentApplication content;
        private AccountRepository acr;
        private string novoNome, linkName;

        public ContentController()
        {
            main = new MainApplication();
            content = new ContentApplication();
            acr = new AccountRepository();
        }
        // GET: Extranet/Content
        public ActionResult Didactic()
        {
            return View();
        }

        public ActionResult GridDidactic()
        {
            List<ContentMetadado> didatico = content.ListContent();
            return PartialView(didatico);
        }

        public ActionResult GridDidacticFilter(String disciplina, String status)
        {
            long disc = 0, st = 0;
            if (disciplina != "0")
            {
                disc = int.Parse(disciplina);
            }
            if (status != "0")
            {
                st = int.Parse(status);
            }
            List<ContentMetadado> didatico = content.ListContentFilter(disc, st);
            return PartialView(didatico);
        }

        public ActionResult CreateDidacticContent()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDidacticContent(ContentMetadado contents)
        {
            String eus_login = acr.getStudentLogged().ToString();
            Boolean resultado = content.SaveContent(
                contents.econt_descricao, contents.econt_titulo, contents.econt_datapublicacao.ToString(), 
                contents.econt_datafinalizacao.ToString(), contents.econt_dp_id.ToString(), 
                contents.econt_mod_id.ToString(), eus_login);
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult EditDidacticContent(String id)
        {
            var edit = content.EditContent(id);
            string valor = edit.econt_descricao.Replace("\n", "");
            valor = valor.Replace("\r", "");
            ViewBag.descricaoEcont = valor;
            ViewBag.ID = edit.econt_id;
            return View(edit);
        }

        [HttpPost]
        public ActionResult EditDidacticContent(ContentMetadado contents)
        {
            String eus_login = acr.getStudentLogged().ToString();
            Boolean resultado = content.UpdateContent(
                contents.econt_id.ToString(), contents.econt_descricao, contents.econt_titulo, 
                contents.econt_datapublicacao.ToShortDateString(), contents.econt_datafinalizacao.ToShortDateString(), 
                contents.econt_dp_id.ToString(), contents.econt_mod_id.ToString(),
                contents.econt_st_id.ToString(), eus_login);
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult DetailsArchives(String id)
        {
            ViewBag.ID = id;
            var edit = content.EditContent(id);
            string nomefinal = edit.econt_titulo.Replace(" ", "");
            ViewBag.Nome = nomefinal;
            return View();
        }

        [HttpPost]
        public virtual ActionResult DetailsArchives(UploadArchivesMetadado arq)
        {
            HttpPostedFileBase myFile = Request.Files["MyFile"];
            string message = "Arquivo não enviado";
            Boolean isUploaded = false;
            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Archives");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        string arquivoAtual = myFile.FileName.Split('.').First();
                        string tipoAtual = myFile.FileName.Split('.').Last();
                        string FileNameNew = CreateName(arquivoAtual, arq.conteudoNome, arq.conteudoID, tipoAtual);
                        myFile.SaveAs(Path.Combine(pathForSaving, FileNameNew));
                        isUploaded = true;
                        linkName = Server.MapPath("~/Archives/") + FileNameNew;
                        bool resultado = main.SaveArchive(FileNameNew, arq.conteudoID, FileNameNew);
                        if (resultado == true)
                            message = "Salvo";
                        else
                        {
                            message = "NSalvo";
                            bool exc = DeleteArchive(linkName);
                        }
                    }
                    catch (Exception ex)
                    {
                        message = "NSalvo";
                    }
                }
            }
            return Json(new { message = message });
        }

        [HttpPost]
        public ActionResult DeleteArchives(ArchivesMetadado arq)
        {
            string message = "";
            Boolean resultado = main.DeleteArchives(arq.earq_id.ToString());
            if (resultado)
            {
                string caminho = Server.MapPath("~/Archives/") + arq.earq_nome;
                if (DeleteArchive(caminho))
                    message = "Salvo";
                else
                    message = "NSalvo";
            }
            return Json(new { message = message });
        }

        public ActionResult DetailsArchivesList(String id)
        {
            List<ArchivesMetadado> arq = main.ListArchives(id);
            return PartialView(arq);
        }

        public ActionResult DetailsArchivesListEdit(String id)
        {
            List<ArchivesMetadado> arq = main.ListArchivesEdit(id);
            return PartialView(arq);
        }

        public ActionResult SubjectDropDownList()
        {
            List<SubjectMetadado> dp = main.ListSubject();
            return PartialView(dp);
        }

        public ActionResult SubjectDropdownListFilter()
        {
            List<SubjectMetadado> subject = main.ListSubject();
            return PartialView(subject);
        }

        public ActionResult ModuleDropDownList()
        {
            List<ModuleMetadado> mod = main.ListModule();
            return PartialView(mod);
        }

        private string CreateName(string nomeAtual, string conteudoNome, string idcont, string tipo)
        {
            string data = DateTime.Now.ToString("dd-MM-yyyy");
            novoNome = "" + conteudoNome + "-" + nomeAtual + "-" + idcont + "-" + data + "." + tipo;
            return novoNome;
        }

        private Boolean CreateFolderIfNeeded(String path)
        {
            Boolean result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        private Boolean DeleteArchive(String carquivo)
        {
            if (System.IO.File.Exists(carquivo))
            {
                System.IO.File.Delete(carquivo);
                return true;
            }
            else
                return false;
        }
    }
}