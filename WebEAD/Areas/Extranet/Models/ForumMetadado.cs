using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEAD.Areas.Extranet.Models
{
    public class ForumMetadado
    {
        [Key]
        public long ftpc_id { get; set; }
        [Required(ErrorMessage = "Informe a Descrição", AllowEmptyStrings = false)]
        public String ftpc_descricao { get; set; }
        [DataType(DataType.Date)]
        public DateTime ftpc_datacadastro { get; set; }
        public long ftpc_dp_id { get; set; }
        public long ftpc_st_id { get; set; }
        public String ftpc_dp_descricao { get; set; }
        public bool ftpc_status { get; set; }
        public String status { get; set; }
    }

    public class PostsMetadado
    {
        [Key]
        public long fptg_id { get; set; }
        public String fptg_mensagem { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fptg_datapublicacao { get; set; }
        public String linkarquivo { get; set; }
        public String nomearquivo { get; set; }
        public String NomeProfessor { get; set; }
    }

    public class TopicMetadado
    {
        [AllowHtml]
        [Required(ErrorMessage = "Informe a mensagem", AllowEmptyStrings = false)]
        public String fptg_mensagem { get; set; }
        public String idtopico { get; set; }
        public String idlogin { get; set; }
        [Required(ErrorMessage = "Informe a Disciplina", AllowEmptyStrings = false)]
        public String ftpc_dp_id { get; set; }
    }
}