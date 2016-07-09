using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebEAD.DAO;

namespace WebEAD.Repository
{
    public class AccountRepository
    {
        public static bool UserAuthentication(String Login, String Password)
        {
            Auxiliares aux = new Auxiliares();
            Criptografia cry = new Criptografia();

            String newPass = cry.Cifrar(Password);
            String campos = "eus_login, eus_senha";
            String resto = "mot_ead_usuarios WHERE eus_login = '" + Login + "' && eus_senha = '" + newPass + "'";

            if (!aux.SelectBool(campos, resto))
                return false;
            else
            {
                FormsAuthentication.SetAuthCookie(Login, false);
                return true;
            }
        }

        public String getStudentLogged()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value != "")
                {
                    HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    string _ralogin = ticket.Name;

                    return _ralogin;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}