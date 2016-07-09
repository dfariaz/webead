using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.EAD.Models;

namespace WebEAD.Areas.EAD.Application
{
    public class ForumApplication
    {
        private Auxiliares aux = new Auxiliares();
        private String idTopicos, contagem, idA;

        public List<ForumMetadado> ListTopics(long idD)
        {
            var topicos = new List<ForumMetadado>();
            string campos = "tpc.*";
            string resto = "mot_forum_topicos tpc INNER JOIN mot_disciplina dp ON(dp.dp_id = tpc.ftpc_dp_id)";
            resto += "INNER JOIN mot_modulo m ON(m.mod_id = dp.dp_mod_id)";
            resto += "INNER JOIN mot_turma tm ON(tm.tm_mod_id = m.mod_id)";
            resto += "WHERE dp.dp_id = " + idD + " GROUP BY tpc.ftpc_id ORDER BY tpc.ftpc_descricao ASC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                idTopicos = _dt.Rows[i]["ftpc_id"].ToString();
                var TempTopicos = new ForumMetadado
                {
                    id = int.Parse(_dt.Rows[i]["ftpc_id"].ToString()),
                    topicos = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    qtdMensagens = MessagesCount(idD).ToString()
                };
                topicos.Add(TempTopicos);
            }
            return topicos;
        }

        public string MessagesCount(long idD)
        {
            contagem = "0";
            string campos = "COUNT(ptg.fptg_id) AS quantidade";
            string resto = "mot_forum_postagem ptg INNER JOIN mot_forum_topicos tpc ON (ptg.fptg_ftpc_id = tpc.ftpc_id) ";
            resto += "INNER JOIN mot_disciplina dp ON (dp.dp_id = tpc.ftpc_dp_id) ";
            resto += "WHERE tpc.ftpc_id = " + idTopicos + " AND dp.dp_id = " + idD;
            DataTable _dt = aux.Select(campos, resto);
            for (int j = 0; j < _dt.Rows.Count; j++)
            {
                contagem = _dt.Rows[j]["quantidade"].ToString();
                break;
            }
            return contagem;
        }

        public Boolean SaveMessage(String idT, String idL, String mensagem)
        {
            string campos = "eus_id";
            string resto = "mot_ead_usuarios WHERE eus_login = '" + idL + "'";
            DataTable _dt = aux.Select(campos, resto);

            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                idA = _dt.Rows[i]["eus_id"].ToString();
                break;
            }

            DateTime dataAtual = DateTime.Now;
            string data = dataAtual.ToString("yyyy-MM-dd HH:mm:ss");

            string tabela = "mot_forum_postagem";
            string valores = "VALUES (null, '" + mensagem + "', '" + data + "', " + idT + ", " + idA + ", null, null, 2)";
            string retorno = aux.Insert(tabela, valores);

            if (retorno == "true")
                return true;
            else
                return false;            
        }

        public List<TopicsMetadado> ListMessages(long idTopico, long idDisciplina)
        {
            var mensagens = new List<TopicsMetadado>();
            string campos = "*";
            string resto = "mot_forum_postagem ptg";
            resto += " INNER JOIN mot_forum_topicos tpc ON(ptg.fptg_ftpc_id = tpc.ftpc_id)";
            resto += " INNER JOIN mot_disciplina dp ON(dp.dp_id = tpc.ftpc_dp_id)";
            resto += " INNER JOIN mot_ead_usuarios eus ON(eus.eus_id = ptg.fptg_eus_id)";
            resto += " WHERE tpc.ftpc_id = " + idTopico + " AND dp.dp_id = " + idDisciplina + " AND ptg.fptg_fixa = 2";
            resto += " GROUP BY ptg.fptg_id";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempMensagens = new TopicsMetadado
                {
                    id = int.Parse(_dt.Rows[i]["fptg_id"].ToString()),
                    mensagem = _dt.Rows[i]["fptg_mensagem"].ToString(),
                    datapublicacao = DateTime.Parse(_dt.Rows[i]["fptg_datapublicacao"].ToString()),
                    NomeTopico = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    NomeProfessor = _dt.Rows[i]["eus_nome"].ToString(),
                    fixa = int.Parse(_dt.Rows[i]["fptg_fixa"].ToString())
                };
                mensagens.Add(TempMensagens);
            }
            return mensagens;
        }

        public List<TopicsMetadado> ListMessageFixed(long idTopico, long idDisciplina)
        {
            var mensagensF = new List<TopicsMetadado>();
            string campos = "*";
            string resto = "mot_forum_postagem ptg INNER JOIN mot_forum_topicos tpc ON (ptg.fptg_ftpc_id = tpc.ftpc_id)";
            resto += " INNER JOIN mot_disciplina dp ON (dp.dp_id = tpc.ftpc_dp_id)";
            resto += " INNER JOIN mot_modulo m ON(m.mod_id = dp.dp_mod_id)";
            resto += " INNER JOIN mot_turma tm ON(tm.tm_mod_id = m.mod_id)";
            resto += " INNER JOIN mot_turmaprofessor tmp ON(tmp.tp_tm_id = tm.tm_id)";
            resto += " INNER JOIN mot_professor pf ON(pf.pf_id = tmp.tp_pf_id)";
            resto += " INNER JOIN mot_funcionario f ON(f.func_id = pf.pf_func_id)";
            resto += " WHERE tpc.ftpc_id = " + idTopico + " AND dp.dp_id = " + idDisciplina + " AND ptg.fptg_fixa = 1";
            resto += " GROUP BY ptg.fptg_id";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempMensagensF = new TopicsMetadado
                {
                    id = int.Parse(_dt.Rows[i]["fptg_id"].ToString()),
                    mensagem = _dt.Rows[i]["fptg_mensagem"].ToString(),
                    datapublicacao = DateTime.Parse(_dt.Rows[i]["fptg_datapublicacao"].ToString()),
                    NomeTopico = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    NomeProfessor = _dt.Rows[i]["func_nome"].ToString(),
                    fixa = int.Parse(_dt.Rows[i]["fptg_fixa"].ToString())
                };
                mensagensF.Add(TempMensagensF);
            }
            return mensagensF;
        }
    }
}