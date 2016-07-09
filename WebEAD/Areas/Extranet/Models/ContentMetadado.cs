using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Areas.Extranet.Models
{
    public class ContentMetadado
    {
        [Key]
        public long econt_id { get; set; }
        public string econt_descricao { get; set; }
        public String econt_titulo { get; set; }
        public DateTime econt_datapublicacao { get; set; }
        public DateTime econt_datafinalizacao { get; set; }
        public long econt_eus_id { get; set; }
        public long econt_dp_id { get; set; }
        public long econt_mod_id { get; set; }
        public long econt_st_id { get; set; }
        public String dp_descricao { get; set; }
        public String mod_descricao { get; set; }
        public String st_descricao { get; set; }

        public bool econt_status { get; set; }
    }
}