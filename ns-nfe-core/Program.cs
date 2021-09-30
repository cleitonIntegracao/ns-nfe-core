using System;
using ns_nfe_core.src.emissao;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ns_nfe_core.src.commons;
using ns_nfe_core.src.nfe.eventos;
using ns_nfe_core.src.nfe.utilitarios;

//EXEMPLOS DE USO DA BIBLIOTECA

namespace ns_nfe_core
{
    class Program 
    {
        static async Task Main(string[] args)
        {
            await previa();
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
                nSeqEvento = "5",
                tpAmb = "2",
                xCorrecao = "CORRECAO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await CartaCorrecao.sendPostRequest(requisicaoCorrecao, "XP", @"NFe/Eventos/",true);
        }

        static async Task inutilizarNFe()
        {
            var requisicaoInutilizar = new Inutilizacao.Body
            {
                ano = "21",
                tpAmb = "2",
                CNPJ = "",
                cUF = 43,
                nNFIni = "",
                nNFFin = "",
                serie = "0",
                xJust = "NUMERACAO INUTILIZADA PARA TESTES DE INTEGRACAO ESTRUTURA XSD - CORE"
            };

            var retorno = await Inutilizacao.sendPostRequest(requisicaoInutilizar, "XP", @"NFe/Eventos/", true);
            Console.WriteLine();
        }

        static async Task consultarCadastro()
        {
            var requisicaoConsultaCadastro = new ConsultarCadastro.Body
            {
                CNPJCont = "",
                CNPJ = "",
                UF = "RS"
            };

            var retorno = await ConsultarCadastro.sendPostRequest(requisicaoConsultaCadastro);
            Console.WriteLine();
        }
        static async Task consultarNFe()
        {
            var requisicaoConsultarNFe = new ConsultarSituacao.Body
            {
                chNFe = "",
                tpAmb = "2",
                licencaCNPJ = "",
                versao = "4.00"
            };

            var retorno = await ConsultarSituacao.sendPostRequest(requisicaoConsultarNFe);
            Console.WriteLine();
        }

        static async Task consultarWS()
        {
            var requisicaoConsultarWS = new ConsultarWebService.Body
            {
                CNPJCont = "",
                tpAmb = "2",
                UF = 43,
                versao =  "4.00"

            };

            var retorno = await ConsultarWebService.sendPostRequest(requisicaoConsultarWS);
            Console.WriteLine();
        }

        static async Task enviarEmail()
        {
            string[] destinatarios = new string[1] { "fernando.konflanz@nstecnologia.com.br" };

            var requisicaoEnviarEmail = new EnvioEmail.Body
            {
                chNFe = "",
                anexarEvento = true,
                anexarPDF = true,
                tpAmb = "2",
                email = destinatarios
            };

            var retorno = await EnvioEmail.sendPostRequest(requisicaoEnviarEmail);
            Console.WriteLine();
        }

        static async Task gerarPDF()
        {
            //string xml = System.IO.File.ReadAllText(@"./arquivoGerarPDF.xml");

            var requisicaoGerarPDF = new GerarPDF.Body
            {
                xml = "",
            };

            var retorno = await GerarPDF.sendPostRequest(requisicaoGerarPDF);
            Console.WriteLine();
        }
        static async Task listarNSNRec()
        {

            var requisicaoListarNSNRec = new ListarNSNRec.Body
            {
                chNFe = ""
            };

            var retorno = await ListarNSNRec.sendPostRequest(requisicaoListarNSNRec);
            Console.WriteLine();
        }

        static async Task previa()
        {

            var retorno = await Previa.sendPostRequest(layoutNFe.gerarNFeXML(),true);
            Console.WriteLine();
        }
    }
}
