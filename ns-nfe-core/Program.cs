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
            await emitirNFe();
        }

        static async Task emitirNFe()
        {
            var NFeXML = layoutNFe.gerarNFeXML();
            var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "XP", true);
            Console.WriteLine(JsonConvert.SerializeObject(retorno));
        }

        static async Task cancelarNFe()
        {
            var requisicaoCancelamento = new Cancelamento.Body
            {
                chNFe = "43210907364617000135550000000224731295459987",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nProt = "143210000854126",
                tpAmb = "2",
                xJust = "CANCELAMENTO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await Cancelamento.sendPostRequest(requisicaoCancelamento);
            Console.WriteLine(retorno);
        }
        static async Task corrigirNFe()
        {
            var requisicaoCorrecao = new CartaCorrecao.Body
            {
                chNFe = "43210907364617000135550000000224741625597056",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nSeqEvento = "1",
                tpAmb = "2",
                xCorrecao = "CANCELAMENTO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await CartaCorrecao.sendPostRequest(requisicaoCorrecao, "XP");
            Console.WriteLine(retorno);
        }
    }
}
