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
    public class Emissao
    {
        public class Response
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public string nsNRec { get; set; }

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

        public static async Task<Response> sendPostRequest(TNFe requestBody)
        {
            string url = "https://nfe.ns.eti.br/nfe/issue";

            try { 
                var responseAPI = JsonConvert.DeserializeObject<Response>(await nsAPI.postRequest(url, nfeToXML(requestBody), "xml")); 
                return responseAPI;
            }

            catch (Exception ex) 
            { 
                util.gravarLinhaLog("[ERRO_EMISSAO]: " + ex.Message);
                return null;
            }
            
        }
    }
}
