using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.EAD.Models;

namespace WebEAD.Areas.EAD.Application
{
    public class NoticeApplication
    {
        private Auxiliares aux = new Auxiliares();

        public List<NoticeMetadado> ListAllGeneral()
        {
            var avisos = new List<NoticeMetadado>();
            string campos = "*";
            string resto = "mot_avisos WHERE av_geral = 1 ORDER BY av_datacadastro DESC LIMIT 5";
            DataTable rows = aux.Select(campos, resto);
            for (int i = 0; i < rows.Rows.Count; i++)
            {
                var TempAviso = new NoticeMetadado
                {
                    av_id = int.Parse(!string.IsNullOrEmpty(rows.Rows[i]["av_id"].ToString()) ? rows.Rows[i]["av_id"].ToString() : "0"),
                    av_datacadastro = DateTime.Parse(rows.Rows[i]["av_datacadastro"].ToString()),
                    av_titulo = rows.Rows[i]["av_titulo"].ToString(),
                    av_mensagem = rows.Rows[i]["av_mensagem"].ToString(),
                    av_geral = int.Parse(rows.Rows[i]["av_geral"].ToString())
                };
                avisos.Add(TempAviso);
            }
            return avisos;
        }

        public List<NoticeMetadado> ListAll(long dp, string ra)
        {
            var avisos = new List<NoticeMetadado>();
            string campos = "av.*, f.func_nome";
            string resto = "mot_avisos av INNER JOIN mot_turma tm ON(av.av_tm_id = tm.tm_id)";
            resto += "INNER JOIN mot_modulo m ON(m.mod_id = tm.tm_mod_id)";
            resto += "INNER JOIN mot_disciplina dp ON(dp.dp_mod_id = m.mod_id)";
            resto += "INNER JOIN mot_turmaprofessor tmp ON(tmp.tp_tm_id = tm.tm_id)";
            resto += "INNER JOIN mot_professor pf ON(pf.pf_id = tmp.tp_pf_id)";
            resto += "INNER JOIN mot_professor_disciplina pfd ON(pfd.pfd_pf_id = pf.pf_id)";
            resto += "INNER JOIN mot_funcionario f ON(f.func_id = pf.pf_func_id)";
            resto += "INNER JOIN mot_turmaaluno ta ON(ta.ta_tm_id = tm.tm_id)";
            resto += "INNER JOIN mot_aluno al ON(al.al_id = ta.ta_al_id)";
            resto += "WHERE av.av_geral = 2 AND av.av_dp_id = " + dp + " AND al.al_ra = '" + ra + "' ";
            resto += "GROUP BY av.av_id ORDER BY av_datacadastro DESC LIMIT 5";
            DataTable rows = aux.Select(campos, resto);
            for (int i = 0; i < rows.Rows.Count; i++)
            {
                var TempAviso = new NoticeMetadado
                {
                    av_id = int.Parse(!string.IsNullOrEmpty(rows.Rows[i]["av_id"].ToString()) ? rows.Rows[i]["av_id"].ToString() : "0"),
                    av_datacadastro = DateTime.Parse(rows.Rows[i]["av_datacadastro"].ToString()),
                    av_titulo = rows.Rows[i]["av_titulo"].ToString(),
                    av_mensagem = rows.Rows[i]["av_mensagem"].ToString(),
                    av_geral = int.Parse(rows.Rows[i]["av_geral"].ToString()),
                    professor = rows.Rows[i]["func_nome"].ToString()
                };
                avisos.Add(TempAviso);
            }
            return avisos;
        }
    }
}