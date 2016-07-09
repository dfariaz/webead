using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.Extranet.Models;
using WebEAD.Repository;

namespace WebEAD.Areas.Extranet.Application
{
    public class MainApplication
    {
        private Auxiliares aux = new Auxiliares();

        public MainMetadado ListTeacher(String Login)
        {
            var prof = new List<MainMetadado>();
            string campos = "eus.eus_login, f.func_nome ";
            string resto = "mot_ead_usuarios eus ";
            resto += "INNER JOIN mot_usuarios us ON (eus.eus_login = us.us_login) ";
            resto += "INNER JOIN mot_professor p ON (p.pf_usu_id = us.us_id) ";
            resto += "INNER JOIN mot_funcionario f ON (f.func_id = p.pf_func_id) ";
            resto += "WHERE eus.eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempProf = new MainMetadado
                {
                    eus_login = _dt.Rows[i]["eus_login"].ToString(),
                    func_nome = _dt.Rows[i]["func_nome"].ToString()
                };
                prof.Add(TempProf);
            }
            return prof.FirstOrDefault();
        }

        public List<SubjectMetadado> ListSubject()
        {
            var disciplina = new List<SubjectMetadado>();
            string campos = "*";
            string resto = "mot_disciplina ORDER BY dp_descricao";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempDisciplina = new SubjectMetadado
                {
                    dp_id = int.Parse(_dt.Rows[i]["dp_id"].ToString()),
                    dp_descricao = _dt.Rows[i]["dp_descricao"].ToString()
                };
                disciplina.Add(TempDisciplina);
            }
            return disciplina;
        }

        public List<ModuleMetadado> ListModule()
        {
            var modulo = new List<ModuleMetadado>();
            string campos = "*";
            string resto = "mot_modulo ORDER BY mod_descricao";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempModulo = new ModuleMetadado
                {
                    mod_id = int.Parse(_dt.Rows[i]["mod_id"].ToString()),
                    mod_descricao = _dt.Rows[i]["mod_descricao"].ToString()
                };
                modulo.Add(TempModulo);
            }
            return modulo;
        }

        public List<ArchivesMetadado> ListArchives(String id)
        {
            var arquivos = new List<ArchivesMetadado>();
            string campos = "*";
            string resto = "mot_ead_arquivos aq INNER JOIN mot_ead_conteudo c ON (aq.earq_econt_id = c.econt_id)";
            resto += " INNER JOIN mot_disciplina dp ON(c.econt_dp_id = dp.dp_id)";
            resto += " INNER JOIN mot_modulo m ON(dp.dp_mod_id = m.mod_id)";
            resto += " WHERE econt_id = " + id + " ORDER BY earq_datacadastro DESC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempArquivos = new ArchivesMetadado
                {
                    earq_id = int.Parse(_dt.Rows[i]["earq_id"].ToString()),
                    earq_nome = _dt.Rows[i]["earq_nome"].ToString(),
                    disciplina = _dt.Rows[i]["dp_descricao"].ToString(),
                    earq_datacadastro = DateTime.Parse(_dt.Rows[i]["earq_datacadastro"].ToString()),
                    modulo = _dt.Rows[i]["mod_descricao"].ToString()
                };
                arquivos.Add(TempArquivos);
            }
            return arquivos;
        }

        public List<ArchivesMetadado> ListArchivesEdit(String id)
        {
            var arquivos = new List<ArchivesMetadado>();
            string campos = "*";
            string resto = "mot_ead_arquivos aq INNER JOIN mot_ead_conteudo c ON (aq.earq_econt_id = c.econt_id)";
            resto += " INNER JOIN mot_disciplina dp ON(c.econt_dp_id = dp.dp_id)";
            resto += " INNER JOIN mot_modulo m ON(dp.dp_mod_id = m.mod_id)";
            resto += " WHERE econt_id = " + id + " ORDER BY earq_datacadastro DESC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempArquivos = new ArchivesMetadado
                {
                    earq_id = int.Parse(_dt.Rows[i]["earq_id"].ToString()),
                    earq_nome = _dt.Rows[i]["earq_nome"].ToString()
                };
                arquivos.Add(TempArquivos);
            }
            return arquivos;
        }

        public Boolean SaveArchive(String nome, String id, String link)
        {
            DateTime data = DateTime.Now.Date;
            string datafinal = data.ToString("yyyy-MM-dd");
            string tabela = "mot_ead_arquivos";
            string valores = "VALUES (null, '" + link + "', '" + nome + "', '" + datafinal + "', " + id + ")";
            string retorno = aux.Insert(tabela, valores);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public bool DeleteArchives(String id)
        {
            string tabela = "mot_ead_arquivos";
            string condicao = "earq_id = " + id;
            string retorno = aux.Delete(tabela, condicao);
            if (retorno == "true")
            {
                return true;
            }
            else
                return false;
        }

        public List<ClassMetadado> ListClass(String Login)
        {
            var avisosD = new List<ClassMetadado>();
            string campos = "tm.tm_id, CONCAT(m.mod_descricao,' ',tm.tm_turma) AS tm_mod_turma";
            string resto = "mot_ead_usuarios eus ";
            resto += "INNER JOIN mot_usuarios us ON (eus.eus_login = us.us_login) ";
            resto += "INNER JOIN mot_professor p ON (p.pf_usu_id = us.us_id) ";
            resto += "INNER JOIN mot_turmaprofessor tp ON (tp.tp_pf_id = p.pf_id)";
            resto += "INNER JOIN mot_turma tm ON (tp.tp_tm_id = tm.tm_id)";
            resto += "INNER JOIN mot_modulo m ON (m.mod_id = tm.tm_mod_id)";
            resto += "WHERE eus.eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempAvisos = new ClassMetadado
                {
                    tm_id = int.Parse(!string.IsNullOrEmpty(_dt.Rows[i]["tm_id"].ToString()) ? _dt.Rows[i]["tm_id"].ToString() : "0"),
                    tm_mod_turma = _dt.Rows[i]["tm_mod_turma"].ToString()
                };
                avisosD.Add(TempAvisos);
            }
            return avisosD;
        }
    }
}