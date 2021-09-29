using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ns_nfe_core.src.commons;
using Newtonsoft.Json;

namespace ns_nfe_core.src.eventos
{
    public class DownloadEvento
    {
        public class Body
        {
            public string chNFe { get; set; }
            public string tpAmb { get; set; }
            public string tpDown { get; set; }
            public string tpEvento { get; set; }
            public string nSeqEvento { get; set; }
        };

        public class Response
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public dynamic retEvento { get; set; }
            public string erros { get; set; }
            public string xml { get; set; }
            public string pdf { get; set; }
            public string json { get; set; }

        }

        public static async Task<Response> sendPostRequest(Body requestBody, string caminhoSalvar, bool exibeNaTela)
        {
            try
            {
                string url = "https://nfe.ns.eti.br/nfe/get/event";

                var responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody)));

                string idEvento = "";

                switch (requestBody.tpEvento)
                {
                    case "CANC":
                        idEvento = "110111";
                        break;

                    case "CCE":
                        idEvento = "110110";
                        break;
                }

                if (responseAPI.json != null)
                {
                    util.salvarArquivo(caminhoSalvar, idEvento + responseAPI.retEvento.chNFe + requestBody.nSeqEvento, "-procEven.json", responseAPI.json);
                }

                if (responseAPI.pdf != null)
                {
                    util.salvarArquivo(caminhoSalvar, idEvento + responseAPI.retEvento.chNFe + requestBody.nSeqEvento, "-procEven.pdf", responseAPI.pdf);
                    
                    if (exibeNaTela)
                    {
                        util.exibirPDF(caminhoSalvar, idEvento + responseAPI.retEvento.chNFe + requestBody.nSeqEvento, "-procEven.pdf"); 
                    };
                }

                if (responseAPI.xml != null)
                {
                    util.salvarArquivo(caminhoSalvar, idEvento + responseAPI.retEvento.chNFe + requestBody.nSeqEvento, "-procEven.xml", responseAPI.xml);
                }

                return responseAPI;
            }

            catch (Exception ex)
            {
                util.gravarLinhaLog("[ERRO_DOWNLOAD_EVENTO]: " + ex.Message);
                return null;
            }
        }
    }
}
