using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Areas.Extranet.Models
{
    public class MainMetadado
    {
        public string eus_login { get; set; }
        public string eus_senha { get; set; }
        public int eus_valida { get; set; }
        public string func_nome { get; set; }
    }

    public class SubjectMetadado
    {
        [Key]
        public long dp_id { get; set; }
        public string dp_descricao { get; set; }
    }

    public class ModuleMetadado
    {
        [Key]
        public long mod_id { get; set; }
        public String mod_descricao { get; set; }
    }

    public class ArchivesMetadado
    {
        [Key]
        public long earq_id { get; set; }
        public string earq_link { get; set; }
        public string earq_nome { get; set; }
        public DateTime earq_datacadastro { get; set; }
        public long earq_econt_id { get; set; }
        public string modulo { get; set; }
        public string disciplina { get; set; }
    }

    public class UploadArchivesMetadado
    {
        public string conteudoID { get; set; }
        public string conteudoNome { get; set; }
        public string caminhoArquivo { get; set; }
        public HttpPostedFileBase MyFile { get; set; }
    }

    public class ClassMetadado
    {
        [Key]
        public long tm_id { get; set; }
        public String tm_mod_turma { get; set; }
    }
}