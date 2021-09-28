using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ns_nfe_core.src.commons;
using Newtonsoft.Json;

namespace ns_nfe_core.src.emissao
{
    public class Download
    {
        public class Body
        {
            public string chNFe { get; set; }
            public string tpAmb { get; set; }
            public string tpDown { get; set; }
        };

        public class Response
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public string chNFe { get; set; }
            public string xml { get; set; }
            public string pdf { get; set; }
            public object nfeProc { get; set; }
            public string erros { get; set; }
 
        }

        public static async Task<Response> sendPostRequest(Body requestBody, string caminhoSalvar) 
        {
            try
            {
                string url = "https://nfe.ns.eti.br/nfe/get";

                var responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody)));

                if (responseAPI.nfeProc != null)
                {
                    util.salvarArquivo(caminhoSalvar, responseAPI.chNFe, "-nfeProc.json", JsonConvert.SerializeObject(responseAPI.nfeProc));
                }

                if (responseAPI.pdf != null)
                {
                    util.salvarArquivo(caminhoSalvar, responseAPI.chNFe, "-nfeProc.pdf", responseAPI.pdf);
                    
                }

                if (responseAPI.xml != null)
                {
                    util.salvarArquivo(caminhoSalvar, responseAPI.chNFe, "-nfeProc.xml", responseAPI.xml);
                }

                return responseAPI;
            }

            catch (Exception ex)
            {
                util.gravarLinhaLog("[ERRO_DOWNLOAD]: " + ex.Message);
                return null;
            }
        }
    }
}
