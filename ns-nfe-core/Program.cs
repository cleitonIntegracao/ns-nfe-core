using System;
using ns_nfe_core.src.emissao;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ns_nfe_core.src.commons;
using ns_nfe_core.src.nfe.eventos;

//EXEMPLOS DE USO DA BIBLIOTECA

namespace ns_nfe_core
{
    class Program 
    {
        static async Task Main(string[] args)
        {

        }

        static async Task emitirNFe() // Emitir NFe
        {
            var NFeXML = layoutNFe.gerarNFeXML();
            var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "XP", true, @"NFe/Documentos/");
        }

        static async Task cancelarNFe() // Cancelar NFe
        {
            var requisicaoCancelamento = new Cancelamento.Body
            {
                chNFe = "",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nProt = "",
                tpAmb = "2",
                xJust = "CANCELAMENTO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await Cancelamento.sendPostRequest(requisicaoCancelamento,"XP", @"NFe/Eventos/",true);
        }
        static async Task corrigirNFe() // Corrigir NFe
        {
            var requisicaoCorrecao = new CartaCorrecao.Body
            {
                chNFe = "",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nSeqEvento = "3",
                tpAmb = "2",
                xCorrecao = "CORRECAO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await CartaCorrecao.sendPostRequest(requisicaoCorrecao, "XP", @"NFe/Eventos/",true);
        }
    }
}
