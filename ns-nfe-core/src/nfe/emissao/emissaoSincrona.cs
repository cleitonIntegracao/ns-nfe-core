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
            public string xMotivo { get; set; }
            public string nsNRec { get; set; }
            public string chNFe { get; set; }
            public string nProt { get; set; }
            public string xml { get; set; }
            public string json { get; set; }
            public string pdf { get; set; }
            public string erros { get; set; }

        }

        public static async Task<ResponseSincrono> sendPostRequest(TNFe requestBody, string tpDown = "X", bool exibeNaTela = false, string caminhoSalvar = @"NFe/Documentos/")
        {
            var responseSincrono = new ResponseSincrono();

            var emissaoResponse = await Emissao.sendPostRequest(requestBody);

            if ((emissaoResponse.status == "200") || (emissaoResponse.status == "-6") || (emissaoResponse.status == "-6"))
            {
                responseSincrono.statusEnvio = emissaoResponse.status;
                responseSincrono.nsNRec = emissaoResponse.nsNRec;

                StatusProcessamento.Body statusBody = new StatusProcessamento.Body
                {
                    nsNRec = emissaoResponse.nsNRec,
                    CNPJ = requestBody.infNFe.emit.Item
                };

                var statusResponse = await StatusProcessamento.sendPostRequest(statusBody);

                responseSincrono.statusConsulta = statusResponse.status;

                if ((statusResponse.status == "200"))
                {
                    responseSincrono.cStat = statusResponse.cStat;
                    responseSincrono.xMotivo = statusResponse.xMotivo;
                    responseSincrono.motivo = statusResponse.motivo;
                    responseSincrono.nsNRec = emissaoResponse.nsNRec;

                    if ((statusResponse.cStat == "100") || (statusResponse.cStat == "150"))
                    {
                        responseSincrono.chNFe = statusResponse.chNFe;
                        responseSincrono.nProt = statusResponse.nProt;

                        Download.Body downloadBody = new Download.Body
                        {
                            chNFe = statusResponse.chNFe,
                            tpAmb = requestBody.infNFe.ide.tpAmb.ToString(),
                            tpDown = tpDown
                        };

                        var downloadResponse = await Download.sendPostRequest(downloadBody, caminhoSalvar, exibeNaTela);

                        if (downloadResponse.status == "200")
                        {
                            responseSincrono.statusDownload = downloadResponse.status;
                            responseSincrono.xml = downloadResponse.xml;
                            responseSincrono.json = JsonConvert.SerializeObject(downloadResponse.nfeProc);
                            responseSincrono.pdf = downloadResponse.pdf;
                        }
                    }
                }
            }
            return responseSincrono;
        }
    }
}
