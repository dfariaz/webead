using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Areas.EAD.Models;

namespace WebEAD.Areas.EAD.Application
{
    public class ContentApplication
    {
        private Auxiliares aux = new Auxiliares();

        public List<ContentMetadado> ListContent(String id)
        {
            var content = new List<ContentMetadado>();
            String campos = "*";
            String resto = "mot_ead_conteudo WHERE econt_dp_id = " + id + " AND econt_st_id = 1";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempContent = new ContentMetadado
                {
                    econt_id = int.Parse(_dt.Rows[i]["econt_id"].ToString()),
                    econt_titulo = _dt.Rows[i]["econt_titulo"].ToString()
                };
                content.Add(TempContent);
            }
            return content;
        }

        public ContentMetadado ContentDinamic(String id)
        {
            var content = new List<ContentMetadado>();
            String campos = "*";
            String resto = "mot_ead_conteudo WHERE econt_dp_id = " + id + "";
            DataTable _dt = aux.Select(campos, resto);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                var TempContent = new ContentMetadado
                {
                    econt_id = int.Parse(_dt.Rows[i]["econt_id"].ToString()),
                    econt_descricao = _dt.Rows[i]["econt_descricao"].ToString(),
                    econt_titulo = _dt.Rows[i]["econt_titulo"].ToString(),
                    econt_datapublicacao = DateTime.Parse(_dt.Rows[i]["econt_datapublicacao"].ToString()),
                    econt_datafinalizacao = DateTime.Parse(_dt.Rows[i]["econt_datafinalizacao"].ToString()),
                    econt_pdf = _dt.Rows[i]["econt_pdf"].ToString()
                };
                content.Add(TempContent);
            }
            return content.FirstOrDefault();
        }
    }
}