using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebEAD.DAO;
using WebEAD.Repository;
using WebEAD.Areas.EAD.Models;

namespace WebEAD.Areas.EAD.Application
{
    public class ProfileApplication
    {
        private Auxiliares aux = new Auxiliares();
        private AccountRepository acr = new AccountRepository();
        private Criptografia cry = new Criptografia();

        public Boolean validStudent(String RA)
        {
            string campos = "*";
            string resto = "mot_ead_usuarios WHERE eus_login = '" + RA + "' AND eus_recoverysenha = 1";
            Boolean retorno = aux.SelectBool(campos, resto);
            return retorno;
        }

        public List<ProfileMetadado> ListData(String RA)
        {
            var aluno = new List<ProfileMetadado>();
            string campos = "al.*, CONCAT(tm.tm_mod_id, tm.tm_turma) as Turma, tn.tn_descricao";
            string resto = "mot_aluno al INNER JOIN mot_turmaaluno tma ON (al.al_id = tma.ta_al_id)";
            resto += " INNER JOIN mot_turma tm ON(tma.ta_tm_id = tm_id)";
            resto += " INNER JOIN mot_turno tn ON(tm.tm_tn_id = tn.tn_id) WHERE al_ra = " + RA + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempAluno = new ProfileMetadado
                {
                    al_ra = _dt.Rows[i]["al_ra"].ToString(),
                    al_nome = _dt.Rows[i]["al_nome"].ToString(),
                    turma = _dt.Rows[i]["Turma"].ToString(),
                    email = _dt.Rows[i]["al_email"].ToString(),
                    telefone = _dt.Rows[i]["al_tel"].ToString(),
                    celular = _dt.Rows[i]["al_cel"].ToString(),
                    turno = _dt.Rows[i]["tn_descricao"].ToString()
                };
                aluno.Add(TempAluno);
            }
            return aluno;
        }

        public ProfileMetadado ListDataIndex(String RA)
        {
            var aluno = new List<ProfileMetadado>();
            string campos = "al.*, CONCAT(tm.tm_mod_id, tm.tm_turma) as Turma, tn.tn_descricao";
            string resto = "mot_aluno al INNER JOIN mot_turmaaluno tma ON (al.al_id = tma.ta_al_id)";
            resto += " INNER JOIN mot_turma tm ON(tma.ta_tm_id = tm_id)";
            resto += " INNER JOIN mot_turno tn ON(tm.tm_tn_id = tn.tn_id) WHERE al_ra = " + RA + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempAluno = new ProfileMetadado
                {
                    al_ra = _dt.Rows[i]["al_ra"].ToString(),
                    al_nome = _dt.Rows[i]["al_nome"].ToString(),
                    turma = _dt.Rows[i]["Turma"].ToString(),
                    email = _dt.Rows[i]["al_email"].ToString(),
                    telefone = _dt.Rows[i]["al_tel"].ToString(),
                    celular = _dt.Rows[i]["al_cel"].ToString(),
                    turno = _dt.Rows[i]["tn_descricao"].ToString()
                };
                aluno.Add(TempAluno);
            }
            return aluno.FirstOrDefault();
        }

        public bool AlterPassword(string senha, string ra)
        {
            string novaSenha = cry.Cifrar(senha);

            string tabela = "mot_ead_usuarios";
            string valores = "eus_senha = '" + novaSenha + "', eus_recoverysenha = null";
            string condicao = "eus_login = '" + ra + "'";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateData(string ra, string email, string telefone, string celular)
        {
            string tabela = "mot_aluno";
            string valores = "al_email = '" + email + "', al_tel = '" + telefone + "', al_cel = '" + celular + "'";
            string condicao = "al_ra = '" + ra + "'";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SubjectsMetadado> ListSubjects(String RA)
        {
            var disciplinas = new List<SubjectsMetadado>();
            string campos = "dp.dp_id, dp.dp_descricao";
            string resto = "mot_disciplina dp INNER JOIN mot_modulo m ON (dp.dp_mod_id = m.mod_id)";
            resto += " INNER JOIN mot_turma tm ON (m.mod_id = tm.tm_mod_id) ";
            resto += "INNER JOIN mot_turmaaluno ta ON (tm.tm_id = ta.ta_tm_id) ";
            resto += "INNER JOIN mot_aluno al ON (ta.ta_al_id = al.al_id) ";
            resto += "WHERE al.al_ra = '" + RA + "' ORDER BY dp.dp_descricao ASC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempDisciplina = new SubjectsMetadado
                {
                    dp_id = int.Parse(_dt.Rows[i]["dp_id"].ToString()),
                    dp_descricao = _dt.Rows[i]["dp_descricao"].ToString()
                };
                disciplinas.Add(TempDisciplina);
            }
            return disciplinas;
        }
    }
}