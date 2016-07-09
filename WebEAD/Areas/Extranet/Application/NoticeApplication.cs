using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.Extranet.Models;

namespace WebEAD.Areas.Extranet.Application
{
    public class NoticeApplication
    {
        private Auxiliares aux = new Auxiliares();
        private String idPF, nomeD, link, restoFiltro, idLogin;
        public List<NoticeMetadado> ListNotices(String Login)
        {
            var avisos = new List<NoticeMetadado>();
            string campos = "eus.eus_login, av.av_id, av.av_mensagem, av.av_titulo, av.av_datacadastro, av.av_tm_id, av.av_dp_id, av.av_pf_id, av_geral ";
            string resto = "mot_ead_usuarios eus ";
            resto += "INNER JOIN mot_usuarios us ON (eus.eus_login = us.us_login) ";
            resto += "INNER JOIN mot_professor p ON (p.pf_usu_id = us.us_id) ";
            resto += "INNER JOIN mot_avisos av ON (av.av_pf_id = p.pf_id) ";
            resto += "WHERE eus.eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempAvisos = new NoticeMetadado
                {
                    eus_login = _dt.Rows[i]["eus_login"].ToString(),
                    av_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_id"].ToString()) ? _dt.Rows[i]["av_id"].ToString() : "0"),
                    av_mensagem = _dt.Rows[i]["av_mensagem"].ToString(),
                    av_titulo = _dt.Rows[i]["av_titulo"].ToString(),
                    av_datacadastro = DateTime.Parse(_dt.Rows[i]["av_datacadastro"].ToString()),
                    dp_descricao = NameSubject(_dt.Rows[i]["av_dp_id"].ToString()),
                    av_tm_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_tm_id"].ToString()) ? _dt.Rows[i]["av_tm_id"].ToString() : "0"),
                    av_geral = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_geral"].ToString()) ? _dt.Rows[i]["av_geral"].ToString() : "0")
                };
                avisos.Add(TempAvisos);
            }
            return avisos;
        }

        public List<NoticeMetadado> ListNoticesFilter(long disciplina)
        {
            var forum = new List<NoticeMetadado>();
            string campos = "*";
            restoFiltro = "mot_avisos av INNER JOIN mot_disciplina dp ON (av.av_dp_id = dp.dp_id)";
            if (disciplina != 0)
                restoFiltro += " WHERE av_dp_id = " + disciplina + "";

            restoFiltro += " ORDER BY av_id ASC";
            DataTable _dt = aux.Select(campos, restoFiltro);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new NoticeMetadado
                {
                    av_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_id"].ToString()) ? _dt.Rows[i]["av_id"].ToString() : "0"),
                    av_mensagem = _dt.Rows[i]["av_mensagem"].ToString(),
                    av_titulo = _dt.Rows[i]["av_titulo"].ToString(),
                    av_datacadastro = DateTime.Parse(_dt.Rows[i]["av_datacadastro"].ToString()),
                    dp_descricao = NameSubject(_dt.Rows[i]["av_dp_id"].ToString()),
                    av_tm_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_tm_id"].ToString()) ? _dt.Rows[i]["av_tm_id"].ToString() : "0"),
                    av_geral = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_geral"].ToString()) ? _dt.Rows[i]["av_geral"].ToString() : "0")
                };
                forum.Add(TempForum);
            }
            return forum;
        }

        public Boolean SaveNotice(string titulo, string mensagem, string tm, string dp, string pf, string geral)
        {
            if (tm == "0") { tm = "null"; }
            if (dp == "0") { dp = "null"; }
            DateTime dataAtual = DateTime.Now;
            string data = dataAtual.ToString("yyyy-MM-dd HH:mm:ss");
            string tabela = "mot_avisos";
            string valores = "VALUES (null, '" + data + "', '" + titulo + "', '" + mensagem + "', " + tm + ", " + dp + ",  " + pf + ", " + geral + ")";
            string retorno = aux.Insert(tabela, valores);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public NoticeMetadado EditNotice(String id)
        {
            var edav = new List<NoticeMetadado>();
            string campos = "*";
            string resto = "mot_avisos WHERE av_id = " + id + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new NoticeMetadado
                {
                    av_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_id"].ToString()) ? _dt.Rows[i]["av_id"].ToString() : "0"),
                    av_titulo = _dt.Rows[i]["av_titulo"].ToString(),
                    av_mensagem = _dt.Rows[i]["av_mensagem"].ToString(),
                    av_datacadastro = DateTime.Parse(_dt.Rows[i]["av_datacadastro"].ToString()),
                    av_dp_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_dp_id"].ToString()) ? _dt.Rows[i]["av_dp_id"].ToString() : "0"),
                    av_tm_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_tm_id"].ToString()) ? _dt.Rows[i]["av_tm_id"].ToString() : "0"),
                    av_geral = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["av_geral"].ToString()) ? _dt.Rows[i]["av_geral"].ToString() : "0")
                };
                edav.Add(TempForum);
            }
            return edav.FirstOrDefault();
        }

        public Boolean UpdateNotice(string id, string titulo, string mensagem, string tm, string dp, string geral)
        {
            if (tm == "0") { tm = "null"; }
            if (dp == "0") { dp = "null"; }
            string tabela = "mot_avisos";
            string colunavalores = " av_titulo = '" + titulo + "', av_mensagem = '" + mensagem + "', av_tm_id = " + tm + ", av_dp_id = " + dp + ", av_geral = " + geral + "";
            string condicao = "av_id = " + id + "";
            string retorno = aux.Update(tabela, colunavalores, condicao);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public string NameSubject(string id)
        {
            string campos = "*";
            string resto = "mot_disciplina WHERE dp_id = " + id + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                nomeD = _dt.Rows[i]["dp_descricao"].ToString();
            }
            return nomeD;
        }

        public string GetID(String Login)
        {
            string campos = "p.*";
            string resto = "mot_ead_usuarios eus ";
            resto += "INNER JOIN mot_usuarios us ON (eus.eus_login = us.us_login) ";
            resto += "INNER JOIN mot_professor p ON (p.pf_usu_id = us.us_id) ";
            resto += "WHERE eus.eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                idPF = _dt.Rows[i]["pf_id"].ToString();
            }
            return idPF;
        }
    }
}