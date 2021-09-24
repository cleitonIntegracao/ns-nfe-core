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
    public class EmissaoSincrona
    {
        public class ResponseSincrono
        {
            public string statusEnvio { get; set; }
            public string statusConsulta { get; set; }
            public string statusDownload { get; set; }
            public string cStat { get; set; }
            public string motivo { get; set; }
            public string nsNRec { get; set; }
            public string chNFe { get; set; }
            public string nProt { get; set; }
            public string xml { get; set; }
            public string json { get; set; }
            public string pdf { get; set; }
            public string[] erros { get; set; }

        }

        public static async Task<ResponseSincrono> sendPostRequest(TNFe requestBody)
        {
            var responseSincrono = new ResponseSincrono();

            var responseEmissao = await Emissao.sendPostRequest(requestBody);

            responseSincrono.statusEnvio = responseEmissao.status;
            responseSincrono.nsNRec = responseEmissao.nsNRec;

            StatusProcessamento.Body statusBody = new StatusProcessamento.Body
            {
                nsNRec = responseEmissao.nsNRec,
                CNPJ = requestBody.infNFe.emit.Item
            };

            var statusResponse = await StatusProcessamento.sendPostRequest(statusBody);

            responseSincrono.statusConsulta = statusResponse.status;
            responseSincrono.cStat = statusResponse.cStat;
            responseSincrono.cStat = statusResponse.chNFe;
            responseSincrono.nProt = statusResponse.nProt;

            Download.Body downloadBody = new Download.Body
            {
                chNFe = statusResponse.chNFe,
                tpAmb = requestBody.infNFe.ide.tpAmb.ToString(),
                tpDown = "XP"
            };

            var responseDownload = await Download.sendPostRequest(downloadBody);

            responseSincrono.statusDownload = responseDownload.status;
            responseSincrono.chNFe = responseDownload.chNFe;
            responseSincrono.xml = responseDownload.xml;
            responseSincrono.pdf = responseDownload.pdf;
            responseSincrono.json = responseDownload.json;

            return responseSincrono;
        }
    }
}
