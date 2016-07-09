using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.Extranet.Models;

namespace WebEAD.Areas.Extranet.Application
{
    public class ContentApplication
    {
        private Auxiliares aux = new Auxiliares();
        private String idPF;
        public List<ContentMetadado> ListContent()
        {
            var conteudo = new List<ContentMetadado>();
            string campos = "*";
            string resto = "mot_ead_conteudo ect INNER JOIN mot_ead_usuarios eus ON (ect.econt_eus_id = eus.eus_id)";
            resto += " INNER JOIN mot_disciplina dp ON (ect.econt_dp_id = dp.dp_id)";
            resto += " INNER JOIN mot_modulo m ON (ect.econt_mod_id = m.mod_id)";
            resto += " INNER JOIN mot_status st ON (ect.econt_st_id = st.st_id)";
            resto += " ORDER BY econt_datapublicacao DESC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempConteudo = new ContentMetadado
                {
                    econt_id = int.Parse(_dt.Rows[i]["econt_id"].ToString()),
                    econt_titulo = _dt.Rows[i]["econt_titulo"].ToString(),
                    econt_descricao = _dt.Rows[i]["econt_descricao"].ToString(),
                    econt_datapublicacao = DateTime.Parse(_dt.Rows[i]["econt_datapublicacao"].ToString()),
                    econt_datafinalizacao = DateTime.Parse(_dt.Rows[i]["econt_datafinalizacao"].ToString()),
                    dp_descricao = _dt.Rows[i]["dp_descricao"].ToString(),
                    mod_descricao = _dt.Rows[i]["mod_descricao"].ToString(),
                    st_descricao = _dt.Rows[i]["st_nome"].ToString()
                };
                conteudo.Add(TempConteudo);
            }
            return conteudo;
        }

        public List<ContentMetadado> ListContentFilter(long disciplina, long status)
        {
            var conteudo = new List<ContentMetadado>();
            string campos = "*";
            string resto = "mot_ead_conteudo ect INNER JOIN mot_ead_usuarios eus ON (ect.econt_eus_id = eus.eus_id)";
            resto += " INNER JOIN mot_disciplina dp ON (ect.econt_dp_id = dp.dp_id)";
            resto += " INNER JOIN mot_modulo m ON (ect.econt_mod_id = m.mod_id)";
            resto += " INNER JOIN mot_status st ON (ect.econt_st_id = st.st_id)";

            if (disciplina != 0 && status != 0)
                resto += " WHERE econt_dp_id = " + disciplina + " AND econt_st_id = " + status;
            else if (disciplina != 0)
                resto += " WHERE econt_dp_id = " + disciplina;
            else
                resto += " WHERE econt_st_id = " + status;

            resto += " ORDER BY econt_datapublicacao DESC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempConteudo = new ContentMetadado
                {
                    econt_id = int.Parse(_dt.Rows[i]["econt_id"].ToString()),
                    econt_titulo = _dt.Rows[i]["econt_titulo"].ToString(),
                    econt_descricao = _dt.Rows[i]["econt_descricao"].ToString(),
                    econt_datapublicacao = DateTime.Parse(_dt.Rows[i]["econt_datapublicacao"].ToString()),
                    econt_datafinalizacao = DateTime.Parse(_dt.Rows[i]["econt_datafinalizacao"].ToString()),
                    dp_descricao = _dt.Rows[i]["dp_descricao"].ToString(),
                    mod_descricao = _dt.Rows[i]["mod_descricao"].ToString(),
                    st_descricao = _dt.Rows[i]["st_nome"].ToString()
                };
                conteudo.Add(TempConteudo);
            }
            return conteudo;
        }

        public Boolean SaveContent(String descricao, String titulo, String datapublicacao, String datafinalizacao, String dp, String mod, String Login)
        {
            DateTime dtPublicacao = DateTime.Parse(datapublicacao);
            DateTime dtFinalizacao = DateTime.Parse(datafinalizacao);
            string login = GetID(Login);
            string dt_pub = dtPublicacao.ToString("yyyy-MM-dd");
            string dt_fin = dtFinalizacao.ToString("yyyy-MM-dd");
            string tabela = "mot_ead_conteudo";
            string valores = "VALUES (null, '" + descricao + "', '" + titulo + "', '" + dt_pub + "', '" + dt_fin + "', " + login + ", " + dp + ", " + mod + ", 2)";
            string retorno = aux.Insert(tabela, valores);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public ContentMetadado EditContent(String id)
        {
            var conteudo = new List<ContentMetadado>();
            string campos = "*";
            string resto = "mot_ead_conteudo ect INNER JOIN mot_ead_usuarios eus ON (ect.econt_eus_id = eus.eus_id)";
            resto += " INNER JOIN mot_disciplina dp ON (ect.econt_dp_id = dp.dp_id)";
            resto += " INNER JOIN mot_modulo m ON (ect.econt_mod_id = m.mod_id)";
            resto += " INNER JOIN mot_status st ON (ect.econt_st_id = st.st_id)";
            resto += " WHERE econt_id = " + id + " ORDER BY econt_datapublicacao DESC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempConteudo = new ContentMetadado
                {
                    econt_id = int.Parse(_dt.Rows[i]["econt_id"].ToString()),
                    econt_titulo = _dt.Rows[i]["econt_titulo"].ToString(),
                    econt_descricao = _dt.Rows[i]["econt_descricao"].ToString(),
                    econt_datapublicacao = DateTime.Parse(_dt.Rows[i]["econt_datapublicacao"].ToString()),
                    econt_datafinalizacao = DateTime.Parse(_dt.Rows[i]["econt_datafinalizacao"].ToString()),
                    econt_dp_id = int.Parse(_dt.Rows[i]["econt_dp_id"].ToString()),
                    econt_mod_id = int.Parse(_dt.Rows[i]["econt_mod_id"].ToString()),
                    econt_st_id = int.Parse(_dt.Rows[i]["econt_st_id"].ToString())
                };
                conteudo.Add(TempConteudo);
            }
            return conteudo.FirstOrDefault();
        }

        public Boolean UpdateContent(String id, String descricao, String titulo, String datapublicacao, String datafinalizacao, String dp, String mod, String status, String Login)
        {
            DateTime dtPublicacao = DateTime.Parse(datapublicacao);
            DateTime dtFinalizacao = DateTime.Parse(datafinalizacao);
            string login = GetID(Login);
            string dt_pub = dtPublicacao.ToString("yyyy-MM-dd");
            string dt_fin = dtFinalizacao.ToString("yyyy-MM-dd");
            string tabela = "mot_ead_conteudo";
            string valores = "econt_descricao = '" + descricao + "', econt_titulo = '" + titulo + "', econt_datapublicacao = '" + dt_pub + "'";
            valores += ", econt_datafinalizacao = '" + dt_fin + "', econt_eus_id = " + login + ", ";
            valores += "econt_dp_id = " + dp + ", econt_mod_id = " + mod + ", econt_st_id = " + status + "";
            string condicao = "econt_id = " + id + "";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public string GetID(String Login)
        {
            string campos = "*";
            string resto = "mot_ead_usuarios eus ";
            resto += "WHERE eus.eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                idPF = _dt.Rows[i]["eus_id"].ToString();
            }
            return idPF;
        }
    }
}