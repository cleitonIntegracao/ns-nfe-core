using System;
using ns_nfe_core.src.emissao;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ns_nfe_core.src.commons;
using ns_nfe_core.src.nfe.eventos;

namespace ns_nfe_core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await cancelarNFe();
        }

        static async Task emitirNFe()
        {
            var NFeXML = layoutNFe.gerarNFeXML();
            var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "X");
            Console.WriteLine(JsonConvert.SerializeObject(retorno));
        }

        static async Task cancelarNFe()
        {
            var requisicaoCancelamento = new cancelamento.Body
            {
                chNFe = "43210907364617000135550000000224671279716655",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nProt = "143210000852376",
                tpAmb = "2",
                xJust = "CANCELAMENTO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await cancelamento.sendPostRequest(requisicaoCancelamento);
            Console.WriteLine(retorno);
        }
    }
}
