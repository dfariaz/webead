using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebEAD.Areas.Extranet.Application;
using WebEAD.Areas.Extranet.Models;
using WebEAD.Repository;
using WebEAD.Controllers;
using Rotativa;
using Rotativa.Options;

namespace WebEAD.Areas.Extranet.Controllers
{
    public class ForumController : MasterController
    {
        private AccountRepository acr;
        private ForumApplication forum;
        private MainApplication main;
        private String topico;

        public ForumController()
        {
            acr = new AccountRepository();
            main = new MainApplication();
            forum = new ForumApplication();
        }

        // GET: Extranet/Forum
        public ActionResult Topics()
        {
            return View();
        }

        public ActionResult CreateTopics()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateTopics(ForumMetadado topic)
        {
            Boolean resultado = forum.SaveTopic(topic.ftpc_descricao, topic.ftpc_dp_id.ToString(), topic.ftpc_st_id.ToString());
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult GridForum()
        {
            List<ForumMetadado> gforum = forum.ListForum();
            return PartialView(gforum);
        }

        public ActionResult GridForumFilter(String disciplina, String status)
        {
            long disc = 0, st = 0;
            if (disciplina != "0")
                disc = int.Parse(disciplina);
            if (status != "0")
                st = int.Parse(status);
            List<ForumMetadado> gforumfilter = forum.ListForumFilter(disc, st);
            return PartialView(gforumfilter);
        }

        public ActionResult EditTopic(long id)
        {
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult EditTopic(ForumMetadado topic)
        {
            Boolean resultado = forum.EditTopic(
                topic.ftpc_descricao, topic.ftpc_dp_id.ToString(), topic.ftpc_st_id.ToString(), topic.ftpc_id.ToString()
                );
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult FormEditTopic(long id)
        {
            var form = forum.FormEditTopic(id.ToString());
            ViewBag.IDD = form.ftpc_dp_id;
            return View(form);
        }

        public ActionResult Posts(long id)
        {
            List<PostsMetadado> posts = forum.ListPosts(id.ToString());
            return PartialView(posts);
        }

        public ActionResult PostsFixed(long id)
        {
            ViewBag.validacao = forum.validatePost(id.ToString());
            ViewBag.id = id;
            List<PostsMetadado> posts = forum.ListPostFixed(id.ToString());
            return PartialView(posts);
        }

        public ActionResult SubjectDropdownList()
        {
            List<SubjectMetadado> subject = main.ListSubject();
            return PartialView(subject);
        }

        public ActionResult SubjectDropdownListFilter()
        {
            List<SubjectMetadado> subject = main.ListSubject();
            return PartialView(subject);
        }

        public ActionResult CreatePost(String id)
        {
            var topico = forum.EditForumTopic(id);
            ViewBag.idT = topico.ftpc_id;
            string login = acr.getStudentLogged().ToString();
            ViewBag.idL = login;
            ViewBag.idD = topico.ftpc_dp_id;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(TopicMetadado mensagem)
        {
            Boolean resultado = forum.CreatePost(mensagem.fptg_mensagem, mensagem.ftpc_dp_id, mensagem.idlogin, mensagem.idtopico);
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult EditPost(String id)
        {
            var topico = forum.EditForumTopic(id);
            var post = forum.EditPost(id);
            ViewBag.idT = post.idtopico;
            ViewBag.idL = post.idlogin;
            ViewBag.idD = topico.ftpc_dp_id;
            string valor = post.fptg_mensagem.Replace("\n", "");
            valor = valor.Replace("\r", "");
            ViewBag.descricaoPost = valor;
            return View(post);
        }

        [HttpPost]
        public ActionResult EditPost(TopicMetadado mensagem)
        {
            bool resultado = forum.UpdatePost(mensagem.fptg_mensagem, mensagem.ftpc_dp_id, mensagem.idlogin, mensagem.idtopico);
            if (resultado)
                return Json(new { msg = "Salvo" });
            else
                return Json(new { msg = "NSalvo" });
        }

        public ActionResult ModelPDF(long id)
        {
            String eus_login = acr.getStudentLogged().ToString();
            ViewBag.ID = id;
            ViewBag.Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            ViewBag.Nome = forum.NameTeacher(eus_login);
            List<PostsMetadado> postsModel = forum.ListPostsModel(id.ToString());
            return PartialView(postsModel);
        }

        public ActionResult ExportPDF(long id)
        {
            DataTable fedit = forum.ListForumPDF(id.ToString());
            String data;
            for (int i = 0; i < fedit.Rows.Count; i++)
            {
                topico = fedit.Rows[i]["ftpc_descricao"].ToString();
            }
            data = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            var pdf = new ActionAsPdf("ModelPDF", new { id = id })
            {
                FileName = "" + topico + "-mensagensalunos-" + data + ".pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape
            };
            return pdf;
        }
    }
}