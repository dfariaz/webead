using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEAD.Areas.EAD.Models
{
    public class ForumMetadado
    {
        [Key]
        public long id { get; set; }
        public string topicos { get; set; }
        public string qtdMensagens { get; set; }
    }

    public class MessagesMetadado
    {
        [AllowHtml]
        [Required(ErrorMessage = "Informe a mensagem", AllowEmptyStrings = false)]
        public String fptg_mensagem { get; set; }

        public String idtopico { get; set; }

        public String idlogin { get; set; }

        public String iddisc { get; set; }
    }

    public class TopicsMetadado
    {
        [Key]
        public long id { get; set; }
        public String mensagem { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime datapublicacao { get; set; }
        public String NomeTopico { get; set; }
        public String NomeProfessor { get; set; }
        public int fixa { get; set; }
    }
}