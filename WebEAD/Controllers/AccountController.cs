using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Models;
using WebEAD.Repository;
using WebEAD.Application;

namespace WebEAD.Controllers
{
    [HandleError]
    public class AccountController : MasterController
    {
        AccountApplication accountAplication;

        public AccountController()
        {
            accountAplication = new AccountApplication();
        }
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginMetadado login)
        {
            if(ModelState.IsValid)
            {
                if (AccountRepository.UserAuthentication(login.eus_login, login.eus_senha) == false)
                    this.ShowMessageError("O Nome de Usuário ou a Senha informada estão incorretos!");
                else
                {
                    this.ShowMessageInfo("Logado com sucesso!");
                    if (accountAplication.IsTeacher(login.eus_login))
                        return RedirectToAction("Index", "Main", new { area = "Extranet" });
                    else
                        return RedirectToAction("Index", "StudentArea", new { area = "EAD" });
                }
            }
            return View(login);
        }

        public ActionResult Logout()
        {
            AccountRepository.Logout();
            return RedirectToAction("Index", "Site", new { area = "" });
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordMetadado forgot)
        {
            if (ModelState.IsValid)
            {
                Boolean checkLogin = accountAplication.checkLogin(forgot.eus_login);
                if (checkLogin)
                {
                    Boolean result = accountAplication.sendMail(forgot.eus_login);
                    if (result)
                    {
                        this.ShowMessageInfo("Email enviado! Verifique seu email para prosseguir com a solicitação!");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        this.ShowMessageError("Sua solicitação não pode ser atentida! Entre em contato com o suporte!");
                        return RedirectToAction("Login");
                    }
                }
                else
                    ModelState.AddModelError(String.Empty, "Usuário Inválido.");
            }
            return View(forgot);
        }

        public ActionResult ForgotRecovery(String id, String code)
        {
            if(id != "" && code != "")
            {
                Boolean result = accountAplication.SendNewPassword(id, code);
                if (result)
                {
                    this.ShowMessageInfo("Senha Alterada. Um Email foi enviado! Verifique seu email para prosseguir com a solicitação!");
                    return RedirectToAction("Login");
                }
                else
                {
                    this.ShowMessageError("O código ou id não conferem! Entre em contato com o suporte.");
                    return RedirectToAction("Login");
                }
            }
            return PartialView();
        }
    }
}