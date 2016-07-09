using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Repository;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace WebEAD.Application
{
    public class AccountApplication
    {
        private String valida, email, codigo;
        private Boolean retornoEmail;
        Auxiliares aux = new Auxiliares();
        Criptografia cry = new Criptografia();

        public bool IsTeacher(String Login)
        {
            String campos = "*";
            String resto = "mot_ead_usuarios WHERE eus_login = '"+Login+"'";
            DataTable _dt = aux.Select(campos, resto);
            for(int i=0;i < _dt.Rows.Count; i++)
            {
                valida = _dt.Rows[i]["eus_valida"].ToString();
                break;
            }
            if (valida == "1")
                return true;
            else
                return false;
        }

        public Boolean sendMail(string Login)
        {
            if (checkLogin(Login))
            {
                MailMessage objEmail = new MailMessage();
                string remetenteEmail = "testeemailniza@gmail.com";
                string destinatario = getMail(Login);
                if (destinatario != "")
                {
                    string link = generateLink(Login);
                    string mensagem = "";
                    mensagem = "<h2>Você requisitou a alteração da senha!</h2>";
                    mensagem += "<br>";
                    mensagem += "<h3>Por medidas de segurança sua senha foi bloqueada temporariamente e deve ser alterada por meio de novo login ou alteração de senha.</h3>";
                    mensagem += "<br>";
                    mensagem += "<p>Segue o link para alteração da senha: <br>";
                    mensagem += "<a href='localhost:32322/Account/ForgotRecovery?id=" + Login + "&code=" + link + "'> Link para alteração </a>";
                    mensagem += "</p><br>";
                    mensagem += "<p> Caso o link não apareça, copie o seguinte texto e cole em seu navegador: <br>";
                    mensagem += "localhost:32322/Account/ForgotRecovery?id=" + Login + "&code=" + link + " </p><br>";
                    mensagem += "<p>Caso você não tenha requisitado a alteração de senha, desconsidere este email e altere sua senha o mais breve possível.</p>";
                    mensagem += "<br>";
                    mensagem += "<p>Equipe Web EAD - Suporte</p>";

                    objEmail.From = new MailAddress(remetenteEmail);
                    objEmail.To.Add(destinatario);
                    objEmail.Priority = MailPriority.High;
                    objEmail.IsBodyHtml = true;
                    objEmail.Subject = "Recuperar Senha - Web EAD";
                    objEmail.Body = mensagem;
                    objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                    objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                    try
                    {
                        SmtpClient objSmtp = new SmtpClient();
                        objSmtp.UseDefaultCredentials = false;
                        objSmtp.Host = "smtp.gmail.com";
                        objSmtp.Port = 587;
                        objSmtp.EnableSsl = true;
                        objSmtp.Credentials = new NetworkCredential(remetenteEmail, "739182465");
                        objSmtp.Send(objEmail);
                        retornoEmail = true;
                    }
                    catch (Exception ex)
                    {
                        retornoEmail = false;
                    }
                }
                else
                    retornoEmail = false;
            }
            else
                return false;

            return retornoEmail;
        }

        public Boolean checkLogin(string Login)
        {
            String campos = "*";
            String resto = "mot_ead_usuarios WHERE eus_login = '"+Login+"'";
            Boolean retorno = aux.SelectBool(campos, resto);
            return retorno;
        }

        public String getMail(String Login)
        {
            String camposOne = "*";
            String restoOne = "mot_ead_usuarios WHERE eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(camposOne, restoOne);
            for(int i = 0; i < _dt.Rows.Count; i++)
            {
                String camposTwo = "*", restoTwo, valid;
                valid = _dt.Rows[i]["eus_valida"].ToString();
                if (valid == "1")
                    restoTwo = "mot_funcionario func INNER JOIN mot_usuarios ON (func_us_id = us_id) WHERE us_login = '" + Login + "'";
                else
                    restoTwo = "mot_aluno WHERE al_ra = '" + Login + "'";

                DataTable _dtTwo = aux.Select(camposTwo, restoTwo);
                for(int j = 0; j < _dtTwo.Rows.Count; j++)
                {
                    if (valid == "1")
                        email = _dtTwo.Rows[j]["func_email"].ToString();
                    else
                        email = _dtTwo.Rows[j]["al_email"].ToString();
                    break;
                }
                break;
            }
            return email;
        }

        public String generateLink(String Login)
        {
            codigo = System.Guid.NewGuid().ToString();
            string tabela = "mot_ead_usuarios";
            string valores = "eus_confirmasenha = '" + codigo + "'";
            string condicao = "eus_login = '" + Login + "'";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
                return codigo;
            else
                return "";
        }

        public Boolean SendNewPassword(String Login, String Code)
        {
            if (checkCode(Login, Code))
            {
                MailMessage objEmail = new MailMessage();
                string remetenteEmail = "testeemailniza@gmail.com";
                string destinatario = getMail(Login);
                if (destinatario != "")
                {
                    string senha = generateNewPassword(Login);
                    string mensagem = "";
                    mensagem = "<h2>Você requisitou a alteração da senha!</h2>";
                    mensagem += "<br>";
                    mensagem += "<p>Segue sua nova Senha: <br> " + senha + "";
                    mensagem += "</p><br>";
                    mensagem += "<p> Não se esqueça de alterar sua senha! <br>";
                    mensagem += "<p>Equipe Web EAD - Suporte</p>";

                    objEmail.From = new MailAddress(remetenteEmail);
                    objEmail.To.Add(destinatario);
                    objEmail.Priority = MailPriority.High;
                    objEmail.IsBodyHtml = true;
                    objEmail.Subject = "Nova Senha - Web EAD";
                    objEmail.Body = mensagem;
                    objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                    objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                    try
                    {
                        SmtpClient objSmtp = new SmtpClient();
                        objSmtp.UseDefaultCredentials = false;
                        objSmtp.Host = "smtp.gmail.com";
                        objSmtp.Port = 587;
                        objSmtp.EnableSsl = true;
                        objSmtp.Credentials = new NetworkCredential(remetenteEmail, "739182465");
                        objSmtp.Send(objEmail);
                        if (cleanCode(Login))
                        {
                            retornoEmail = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        retornoEmail = false;
                    }
                }
                else
                    retornoEmail = false;
            }
            else
                return false;

            return retornoEmail;
        }

        public Boolean checkCode(String Login, String Code)
        {
            String campos = "*";
            String resto = "mot_ead_usuarios WHERE eus_login = '" + Login + "' && eus_confirmasenha = '" + Code + "'";
            Boolean retorno = aux.SelectBool(campos, resto);
            return retorno;
        }

        public String generateNewPassword(String Login)
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoSenha = clsRan.Next(4, 8);

            string senha = "";
            for (Int32 i = 0; i <= tamanhoSenha; i++)
            {
                senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            string novaSenha = cry.Cifrar(senha);

            string tabela = "mot_ead_usuarios";
            string valores = "eus_senha = '" + novaSenha + "'";
            string condicao = "eus_login = '" + Login + "'";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
                return senha;
            else
                return "";
        }

        public Boolean cleanCode(String Login)
        {
            string tabela = "mot_ead_usuarios";
            string valores = "eus_confirmasenha = null, eus_recoverysenha = 1";
            string condicao = "eus_login = '" + Login + "'";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
                return true;
            else
                return false;
        }
    }
}