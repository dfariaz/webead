using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEAD.DAO;
using System.Data;
using WebEAD.Repository;

namespace WebEAD.Application
{
    public static class JobApplication
    {
        public static void PublishContent(String go)
        {
            Auxiliares aux = new Auxiliares();
            if (go == "GO")
            {
                DateTime data = DateTime.Now;
                String dataAtual = data.ToString("yyyy-MM-dd");
                String campos = "*";
                String resto = "mot_ead_conteudo WHERE econt_st_id = 2";
                DataTable _dt = aux.Select(campos, resto);
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    String idContent = _dt.Rows[i]["econt_id"].ToString();
                    DateTime dataContent = DateTime.Parse(_dt.Rows[i]["econt_datapublicacao"].ToString());
                    String dataFinal = dataContent.ToString("yyyy-MM-dd");
                    if (dataFinal == dataAtual)
                    {
                        String tabela = "mot_ead_conteudo";
                        String valores = "econt_st_id = 1";
                        String condicao = "econt_id = " + idContent + "";
                        String retorno = aux.Update(tabela, valores, condicao);
                    }
                }
            }
        }
    }
}