using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.Extranet.Models;

namespace WebEAD.Areas.Extranet.Application
{
    public class ForumApplication
    {
        private Auxiliares aux = new Auxiliares();
        private String link, restoFiltro, idLogin, nomeProfessor;
        public Boolean SaveTopic(string descricao, string dp, string st)
        {
            DateTime dataAtual = DateTime.Now;
            string data = dataAtual.ToString("yyyy-MM-dd HH:mm:ss");
            string tabela = "mot_forum_topicos";
            string valores = "VALUES (null, '" + descricao + "', '" + data + "', " + dp + ", " + st + ")";
            string retorno = aux.Insert(tabela, valores);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public List<ForumMetadado> ListForum()
        {
            var forum = new List<ForumMetadado>();
            string campos = "*";
            string resto = "mot_forum_topicos ft INNER JOIN mot_disciplina dp ON (ft.ftpc_dp_id = dp.dp_id)";
            resto += " INNER JOIN mot_status st ON (ft.ftpc_st_id = st.st_id)";
            resto += " WHERE ftpc_st_id = 1 ORDER BY ftpc_descricao ASC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new ForumMetadado
                {
                    ftpc_id = int.Parse(_dt.Rows[i]["ftpc_id"].ToString()),
                    ftpc_descricao = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    ftpc_datacadastro = DateTime.Parse(_dt.Rows[i]["ftpc_datacadastro"].ToString()),
                    ftpc_dp_descricao = _dt.Rows[i]["dp_descricao"].ToString(),
                    status = _dt.Rows[i]["st_nome"].ToString()
                };
                forum.Add(TempForum);
            }
            return forum;
        }

        public List<ForumMetadado> ListForumFilter(long disciplina, long status)
        {
            var forum = new List<ForumMetadado>();
            string campos = "*";
            restoFiltro = "mot_forum_topicos ft INNER JOIN mot_disciplina dp ON (ft.ftpc_dp_id = dp.dp_id)";
            restoFiltro += " INNER JOIN mot_status st ON (ft.ftpc_st_id = st.st_id)";

            if (disciplina != 0 && status != 0)
                restoFiltro += " WHERE ftpc_dp_id = " + disciplina + " AND ftpc_st_id = " + status + "";
            else if (disciplina != 0)
                restoFiltro += " WHERE ftpc_dp_id = " + disciplina + "";
            else
                restoFiltro += " WHERE ftpc_st_id = " + status + "";

            restoFiltro += " ORDER BY ftpc_descricao ASC";
            DataTable _dt = aux.Select(campos, restoFiltro);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new ForumMetadado
                {
                    ftpc_id = int.Parse(_dt.Rows[i]["ftpc_id"].ToString()),
                    ftpc_descricao = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    ftpc_datacadastro = DateTime.Parse(_dt.Rows[i]["ftpc_datacadastro"].ToString()),
                    ftpc_dp_descricao = _dt.Rows[i]["dp_descricao"].ToString(),
                    status = _dt.Rows[i]["st_nome"].ToString()
                };
                forum.Add(TempForum);
            }
            return forum;
        }

        public Boolean EditTopic(string descricao, string dp, string st, string id)
        {
            string tabela = "mot_forum_topicos";
            string valores = "ftpc_descricao = '" + descricao + "', ftpc_dp_id = " + dp + ", ftpc_st_id = " + st;
            string condicao = "ftpc_id = " + id;
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public ForumMetadado FormEditTopic(String id)
        {
            var forum = new List<ForumMetadado>();
            string campos = "*";
            string resto = "mot_forum_topicos WHERE ftpc_id = " + id + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new ForumMetadado
                {
                    ftpc_id = int.Parse(_dt.Rows[i]["ftpc_id"].ToString()),
                    ftpc_descricao = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    ftpc_datacadastro = DateTime.Parse(_dt.Rows[i]["ftpc_datacadastro"].ToString()),
                    ftpc_dp_id = int.Parse(_dt.Rows[i]["ftpc_dp_id"].ToString()),
                    ftpc_st_id = int.Parse(_dt.Rows[i]["ftpc_st_id"].ToString())
                };
                forum.Add(TempForum);
            }
            return forum.FirstOrDefault();
        }

        public List<PostsMetadado> ListPosts(String id)
        {
            var postagem = new List<PostsMetadado>();
            string campos = "*";
            string resto = "mot_forum_postagem ptg";
            resto += " INNER JOIN mot_forum_topicos tpc ON(ptg.fptg_ftpc_id = tpc.ftpc_id)";
            resto += " INNER JOIN mot_disciplina dp ON(dp.dp_id = tpc.ftpc_dp_id)";
            resto += " INNER JOIN mot_ead_usuarios eus ON(eus.eus_id = ptg.fptg_eus_id)";
            resto += " WHERE tpc.ftpc_id = " + id + " AND ptg.fptg_fixa = 2";
            resto += " GROUP BY ptg.fptg_id ORDER BY ptg.fptg_datapublicacao ASC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempPostagem = new PostsMetadado
                {
                    fptg_id = int.Parse(_dt.Rows[i]["fptg_id"].ToString()),
                    fptg_mensagem = _dt.Rows[i]["fptg_mensagem"].ToString(),
                    fptg_datapublicacao = DateTime.Parse(_dt.Rows[i]["fptg_datapublicacao"].ToString()),
                    NomeProfessor = _dt.Rows[i]["eus_nome"].ToString(),
                    linkarquivo = linkArchive(_dt.Rows[i]["fptg_id"].ToString()),
                    nomearquivo = NameArchive(_dt.Rows[i]["fptg_id"].ToString())
                };
                postagem.Add(TempPostagem);
            }
            return postagem;
        }

        public List<PostsMetadado> ListPostsModel(String id)
        {
            var postagem = new List<PostsMetadado>();
            string campos = "*";
            string resto = "mot_forum_postagem ptg";
            resto += " INNER JOIN mot_forum_topicos tpc ON(ptg.fptg_ftpc_id = tpc.ftpc_id)";
            resto += " INNER JOIN mot_disciplina dp ON(dp.dp_id = tpc.ftpc_dp_id)";
            resto += " INNER JOIN mot_ead_usuarios eus ON(eus.eus_id = ptg.fptg_eus_id)";
            resto += " WHERE tpc.ftpc_id = " + id + " AND ptg.fptg_fixa = 2";
            resto += " GROUP BY ptg.fptg_id ORDER BY ptg.fptg_datapublicacao ASC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempPostagem = new PostsMetadado
                {
                    fptg_id = int.Parse(_dt.Rows[i]["fptg_id"].ToString()),
                    fptg_mensagem = _dt.Rows[i]["fptg_mensagem"].ToString(),
                    fptg_datapublicacao = DateTime.Parse(_dt.Rows[i]["fptg_datapublicacao"].ToString()),
                    NomeProfessor = _dt.Rows[i]["eus_nome"].ToString()
                };
                postagem.Add(TempPostagem);
            }
            return postagem;
        }

        public List<PostsMetadado> ListPostFixed(String id)
        {
            var postagem = new List<PostsMetadado>();
            string campos = "*";
            string resto = "mot_forum_postagem ptg INNER JOIN mot_forum_topicos tpc ON (ptg.fptg_ftpc_id = tpc.ftpc_id)";
            resto += " INNER JOIN mot_disciplina dp ON (dp.dp_id = tpc.ftpc_dp_id)";
            resto += " INNER JOIN mot_modulo m ON(m.mod_id = dp.dp_mod_id)";
            resto += " INNER JOIN mot_turma tm ON(tm.tm_mod_id = m.mod_id)";
            resto += " INNER JOIN mot_turmaprofessor tmp ON(tmp.tp_tm_id = tm.tm_id)";
            resto += " INNER JOIN mot_professor pf ON(pf.pf_id = tmp.tp_pf_id)";
            resto += " INNER JOIN mot_funcionario f ON(f.func_id = pf.pf_func_id)";
            resto += " WHERE tpc.ftpc_id = " + id + " AND ptg.fptg_fixa = 1";
            resto += " GROUP BY ptg.fptg_id ORDER BY ptg.fptg_datapublicacao ASC";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempPostagem = new PostsMetadado
                {
                    fptg_id = int.Parse(_dt.Rows[i]["fptg_id"].ToString()),
                    fptg_mensagem = _dt.Rows[i]["fptg_mensagem"].ToString(),
                    fptg_datapublicacao = DateTime.Parse(_dt.Rows[i]["fptg_datapublicacao"].ToString()),
                    NomeProfessor = _dt.Rows[i]["func_nome"].ToString(),
                    linkarquivo = linkArchive(_dt.Rows[i]["fptg_id"].ToString()),
                    nomearquivo = NameArchive(_dt.Rows[i]["fptg_id"].ToString())
                };
                postagem.Add(TempPostagem);
            }
            return postagem;
        }

        public ForumMetadado EditForumTopic(String id)
        {
            var forum = new List<ForumMetadado>();
            string campos = "*";
            string resto = "mot_forum_topicos WHERE ftpc_id = " + id + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new ForumMetadado
                {
                    ftpc_id = int.Parse(_dt.Rows[i]["ftpc_id"].ToString()),
                    ftpc_descricao = _dt.Rows[i]["ftpc_descricao"].ToString(),
                    ftpc_datacadastro = DateTime.Parse(_dt.Rows[i]["ftpc_datacadastro"].ToString()),
                    ftpc_dp_id = int.Parse(_dt.Rows[i]["ftpc_dp_id"].ToString()),
                    ftpc_st_id = int.Parse(_dt.Rows[i]["ftpc_st_id"].ToString())
                };
                forum.Add(TempForum);
            }
            return forum.FirstOrDefault();
        }

        public Boolean CreatePost(String mensagem, String dp, String login, String id)
        {
            DateTime dataAtual = DateTime.Now;
            string data = dataAtual.ToString("yyyy-MM-dd HH:mm:ss");
            string idLgn = ReturnID(login);
            string tabela = "mot_forum_postagem";
            string valores = "VALUES (null, '" + mensagem + "', '" + data + "', " + id + ", " + idLgn + ", null, null, 1)";
            string retorno = aux.Insert(tabela, valores);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public TopicMetadado EditPost(String id)
        {
            var forum = new List<TopicMetadado>();
            string campos = "*";
            string resto = "mot_forum_postagem WHERE fptg_ftpc_id = " + id + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempForum = new TopicMetadado
                {
                    fptg_mensagem = _dt.Rows[i]["fptg_mensagem"].ToString(),
                    idtopico = _dt.Rows[i]["fptg_ftpc_id"].ToString(),
                    idlogin = _dt.Rows[i]["fptg_eus_id"].ToString()
                };
                forum.Add(TempForum);
            }
            return forum.FirstOrDefault();
        }

        public Boolean UpdatePost(String mensagem, String dp, String login, String id)
        {
            string tabela = "mot_forum_postagem";
            string valores = "fptg_mensagem = '" + mensagem + "'";
            string condicao = "fptg_ftpc_id = " + id + " AND fptg_eus_id = " + login + "";
            string retorno = aux.Update(tabela, valores, condicao);
            if (retorno == "true")
                return true;
            else
                return false;
        }

        public String ReturnID(String Login)
        {
            string campos = "*";
            string resto = "mot_ead_usuarios WHERE eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                idLogin = _dt.Rows[i]["eus_id"].ToString();
            }
            return idLogin;
        }

        public DataTable ListForumPDF(string id)
        {
            string campos = "*";
            string resto = "mot_forum_topicos ft INNER JOIN mot_disciplina dp ON (ft.ftpc_dp_id = dp.dp_id)";
            resto += " WHERE ft.ftpc_id = " + id + " ORDER BY ftpc_descricao ASC";
            DataTable _dt = aux.Select(campos, resto);
            return _dt;
        }

        public String validatePost(String id)
        {
            string campos = "*";
            string resto = "mot_forum_postagem ptg";
            resto += " INNER JOIN mot_forum_topicos tpc ON(ptg.fptg_ftpc_id = tpc.ftpc_id)";
            resto += " INNER JOIN mot_disciplina dp ON(dp.dp_id = tpc.ftpc_dp_id)";
            resto += " INNER JOIN mot_ead_usuarios eus ON(eus.eus_id = ptg.fptg_eus_id)";
            resto += " WHERE tpc.ftpc_id = " + id + " AND ptg.fptg_fixa = 1";
            bool resultado = aux.SelectBool(campos, resto);
            if (resultado == true)
                return "1";
            else
                return "0";
        }

        public String linkArchive(string idMensagem)
        {
            string campos = "*";
            string resto = "mot_ead_arquivos ea INNER JOIN mot_forum_postagem ptg ON (ptg.fptg_earq_id = ea.earq_id)";
            resto += " WHERE ptg.fptg_id = " + idMensagem;
            DataTable _dt = aux.Select(campos, resto);
            for (int j = 0; j < _dt.Rows.Count; j++)
            {
                link = _dt.Rows[j]["earq_link"].ToString();
                break;
            }
            return link;
        }

        public string NameArchive(string idMensagem)
        {
            string campos = "*";
            string resto = "mot_ead_arquivos ea INNER JOIN mot_forum_postagem ptg ON (ptg.fptg_earq_id = ea.earq_id)";
            resto += " WHERE ptg.fptg_id = " + idMensagem;
            DataTable _dt = aux.Select(campos, resto);
            for (int j = 0; j < _dt.Rows.Count; j++)
            {
                link = _dt.Rows[j]["earq_nome"].ToString();
                break;
            }
            return link;
        }

        public String NameTeacher(String Login)
        {
            string campos = "eus.eus_login, f.func_nome ";
            string resto = "mot_ead_usuarios eus ";
            resto += "INNER JOIN mot_usuarios us ON (eus.eus_login = us.us_login) ";
            resto += "INNER JOIN mot_professor p ON (p.pf_usu_id = us.us_id) ";
            resto += "INNER JOIN mot_funcionario f ON (f.func_id = p.pf_func_id) ";
            resto += "WHERE eus.eus_login = '" + Login + "'";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                nomeProfessor = _dt.Rows[i]["func_nome"].ToString();
                break;
            }
            return nomeProfessor;
        }
    }
}