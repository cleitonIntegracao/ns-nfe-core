using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ns_nfe_core.src.commons;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;

namespace ns_nfe_core.src.emissao
{
    public class GerarPDF
    {
        public class Body
        {
            public string xml { get; set; }
            public bool printCEAN { get; set; }
            public string obsCanhoto { get; set; }
        }

        public class Response
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public string pdf { get; set; }

        }

        public static async Task<Response> sendPostRequest(Body requestBody)
        {
            string url = "https://nfe.ns.eti.br/util/generatepdf";

            try
            {
                var responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody), "json"));
                return responseAPI;
            }

            catch (Exception ex)
            {
                util.gravarLinhaLog("[ERRO_UTILITARIO_GERAR_PDF]: " + ex.Message);
                return null;
            }

        }
    }
}
