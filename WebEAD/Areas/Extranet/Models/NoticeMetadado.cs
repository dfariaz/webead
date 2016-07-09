using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Areas.Extranet.Models
{
    public class NoticeMetadado
    {
        [Key]
        public long av_id { get; set; }
        [DataType(DataType.Date)]
        public DateTime av_datacadastro { get; set; }
        public string av_titulo { get; set; }
        public string av_mensagem { get; set; }
        public int av_geral { get; set; }
        public int av_tm_id { get; set; }
        public int av_pf_id { get; set; }
        public int av_dp_id { get; set; }
        public string eus_login { get; set; }
        public int dp_id { get; set; }
        public string dp_descricao { get; set; }
        public bool av_g { get; set; }
        public bool av_d { get; set; }
    }
}