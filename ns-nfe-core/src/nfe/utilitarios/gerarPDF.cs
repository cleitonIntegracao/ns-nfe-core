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
                var responseAPI = JsonConvert.DeserializeObject<Response>(await NSAPI.postRequest(url, JsonConvert.SerializeObject(requestBody), "json"));
                return responseAPI;
            }

            catch (Exception ex)
            {
                Util.gravarLinhaLog("[ERRO_UTIL_GERAR_PDF]: " + ex.Message);
                return null;
            }

        }
    }
}
