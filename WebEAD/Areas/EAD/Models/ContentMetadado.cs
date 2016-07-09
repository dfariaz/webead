using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Areas.EAD.Models
{
    public class ContentMetadado
    {
        [Key]
        public long econt_id { get; set; }
        public String econt_descricao { get; set; }
        public String econt_titulo { get; set; }
        [DataType(DataType.Date)]
        public DateTime econt_datapublicacao { get; set; }
        [DataType(DataType.Date)]
        public DateTime econt_datafinalizacao { get; set; }
        public long econt_dp_id { get; set; }
        public long econt_mod_id { get; set; }
        public long econt_st_id { get; set; }
        public String econt_pdf { get; set; }
        public String econt_video { get; set; }
    }
}