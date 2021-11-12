﻿using System;
using System.Threading.Tasks;
using ns_nfe_core.src.commons;
using Newtonsoft.Json;

namespace ns_nfe_core.src.emissao
{
    public class StatusProcessamento
    {
        public class Body
        {
            public string CNPJ { get; set; }
            public string nsNRec { get; set; }
        };

        public class Response
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

        public static async Task<Response> sendPostRequest(Body requestBody)
        {
            string url = "https://nfe.ns.eti.br/nfe/issue/status";
            try {
                var responseAPI = JsonConvert.DeserializeObject<Response>(await NSAPI.postRequest(url, JsonConvert.SerializeObject(requestBody)));
                return responseAPI;
            }
            
            catch (Exception ex) 
            { 
                Util.gravarLinhaLog("[ERRO_CONSULTA_STATUS]: " + ex.Message);
                return null;
            }
        }
    }
}
