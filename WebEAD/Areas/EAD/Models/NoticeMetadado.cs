using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Areas.EAD.Models
{
    public class NoticeMetadado
    {
        [Key]
        public long av_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime av_datacadastro { get; set; }

        public string av_titulo { get; set; }
        [DataType(DataType.MultilineText)]
        public String av_mensagem { get; set; }

        public string professor { get; set; }

        public int av_geral { get; set; }
    }
}