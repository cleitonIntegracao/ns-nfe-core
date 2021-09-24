using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ns_nfe_core.src.commons;
using Newtonsoft.Json;

namespace ns_nfe_core.src.emissao
{
    public class statusProcessamento
    {
        public class body
        {
            public string CNPJ { get; set; }
            public string nsNRec { get; set; }
        };

        public class response
        {
            public string status { get; set; }
            public string motivo { get; set; }
            public string chNFe { get; set; }
            public string cStat { get; set; }
            public string xMotivo { get; set; }
            public string nProt { get; set; }
            public string dhRecbto { get; set; }
            public string xml { get; set; }
            public string erro { get; set; }

        }

        public static async Task<response> sendPostRequest(body requestBody)
        {
            string url = "https://nfe.ns.eti.br/nfe/issue/status";
            var responseAPI = JsonConvert.DeserializeObject<response>(await nsAPI.postRequest(url, JsonConvert.SerializeObject(requestBody)));
            return responseAPI;
        }
    }
}
