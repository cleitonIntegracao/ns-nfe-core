using System;
using System.Threading.Tasks;
using ns_nfe_core.src.commons;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using ns_nfe_core.src.emissao.xsd;

namespace ns_nfe_core.src.emissao
{
    public class Previa
    {
        public class Response
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public string pdf { get; set; }
            public string erros { get; set; }

        }
        private static string nfeToXML(TNFe NFe)
        {
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(NFe.GetType());
                serializer.Serialize(stringwriter, NFe);
                return stringwriter.ToString();
            }
        }

        public static async Task<Response> sendPostRequest(TNFe requestBody, bool exibeNaTela)
        {
            string url = "https://nfe.ns.eti.br/util/preview/nfe";

            try
            {
                var responseAPI = JsonConvert.DeserializeObject<Response>(await NSAPI.postRequest(url, nfeToXML(requestBody), "xml"));
                if (exibeNaTela)
                {
                    try { 
                        Util.salvarArquivo(@"NFe/Previa/", "previaNFe" + requestBody.infNFe.ide.nNF, ".pdf", responseAPI.pdf); 
                    }

                    catch (Exception ex)
                    {
                        Util.gravarLinhaLog("[ERRO_SALVAR_PREVIA]: " + ex.Message);
                        return null;
                    }

                    try
                    {
                        Util.exibirPDF(@"NFe/Previa/", "previaNFe" + requestBody.infNFe.ide.nNF, ".pdf");
                    }

                    catch (Exception ex)
                    {
                        Util.gravarLinhaLog("[ERRO_EXIBIR_PREVIA]: " + ex.Message);
                        return null;
                    }

                }
                return responseAPI;
            }

            catch (Exception ex)
            {
                Util.gravarLinhaLog("[ERRO_PREVIA]: " + ex.Message);
                return null;
            }

        }
    }
}
