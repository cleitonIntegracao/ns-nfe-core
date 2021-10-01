# ns-nfe-core

Esta biblioteca possibilita a comunicação e o consumo da solução API para NFe da NS Tecnologia.

Para imlementar esta bibilioteca em seu projeto, você pode:

1. Realizar a instalação do [pacote](https://www.nuget.org/packages/ns-nfe-core/1.0.5?_src=template) através do Microsoft NuGet no Visual Studio

![Screenshot_4](https://user-images.githubusercontent.com/49299197/135635665-bfb55be5-59e6-4f1e-80dc-0992787c66bf.jpg)

3. Realizar o download da biblioteca pelo [GitHub](https://github.com/konflanzzz/ns-nfe-core/archive/refs/heads/main.zip) e adicionar a pasta "src" em seu projeto no C# (.NET Core )

# Exemplos de uso do pacote

Apos instalação através do gerenciador de pacotes NuGet, faça referência dela em seu projeto:

    using ns_nfe_core;

Para que a comunicação com a API possa ser feita, é necessário informar o seu Token no cabeçalho das requisicoes. 
Com este pacote, você pode fazê-lo assim:

    configParceiro.token = "4dec0a34f460169dd6fb2ef9193003e0"

## Emissão

Para realizarmos a emissão de uma NFe, vamos utilizar os seguintes métodos. Tenhamos como exemplo:

    using ns_nfe_core.src.emissao;
    
    static async Task emitirNFe() // Emitir NFe
    {
        var NFeXML = layoutNFe.gerarNFeXML();
        var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "XP", true, @"NFe/Documentos/");
    }

Primeiramente, construimos um objeto da NFe, e um método ( exemplo ) que retorna este objeto:

    using ns_nfe_core.src.emissao;
    
    namespace ns_nfe_core
    {
        class layoutNFe
        {
            public static TNFe gerarNFeXML()
            {
                TNFe NFe = new TNFe
                {
                    infNFe = new TNFeInfNFe
                    {
                        versao = "4.00",
                        ide = new TNFeInfNFeIde{...}
                        emit = new TNFeInfNFeEmit{...}
                        dest = new TNFeInfNFeDest{...}
                        det = new TNFeInfNFeDet[1]
                        {
                            new TNFeInfNFeDet
                            {
                                nItem = "1",
                                prod = new TNFeInfNFeDetProd{...},
                                imposto = new TNFeInfNFeDetImposto{...},
                            }
                        },
                        total = new TNFeInfNFeTotal{...},
                        transp = new TNFeInfNFeTransp{...},
                        pag = new TNFeInfNFePag{...},
                        infAdic = new TNFeInfNFeInfAdic{...},
                    }
                };
                return NFe;
            }
        }
    }
Apos isso, vamo utilizar o método **sendPostRequest** da classe *EmissaoSincrona* para realizar o envio deste documento NFe para a API.
Este método realiza a emissão, a consulta de status de processamento e o download de forma sequencial.

Os parametros deste método são:

    var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "XP", true, @"NFe/Documentos/");
    
+ *NFeXML* = objeto NFe que será serializado para envio;
+ *"XP"* = tpDown = tipo de download, indicando quais os tipos de arquivos serão obtidos no Download;
+ *true* = exibeNaTela = parametro boolean que indica se será exibido na tela, ou não, o DANFE obtido no download;
+ *@"NFe/Documentos/"* = diretório onde serão salvos os documentos obtidos no download;
    
Podemos acessarmos os dados de retorno e aplicarmos validações da seguinte forma. Tenhamos como exemplo:
            
            // Verifica se houve sucesso na emissão
            if (retorno.statusEnvio == "200" || retorno.statusEnvio == "-6" || retorno.statusEnvio == "-7")
            {
                string statusEnvio = retorno.statusEnvio;
                string nsNRec = retorno.nsNRec;

                // Verifica se houve sucesso na consulta
                if (retorno.statusConsulta == "200")
                {
                    string statusConsulta = retorno.statusConsulta;
                    string motivo = retorno.motivo;
                    string xMotivo = retorno.xMotivo;
                    
                    // Verifica se a nota foi autorizada
                    if (retorno.cStat == "100" || retorno.cStat == "150")
                    {
                        // Documento autorizado com sucesso
                        string cStat = retorno.cStat;
                        string chNFe = retorno.chNFe;
                        string nProt = retorno.nProt;
                        string statusDownload = retorno.statusDownload;
                        
                        if (retorno.statusDownload == "200")
                        {
                            // Verifica de houve sucesso ao realizar o downlaod da NFe
                            string xml = retorno.xml;
                            string json = retorno.json;
                            string pdf = retorno.pdf;
                        }
                        else {
                            // Aqui você pode realizar um tratamento em caso de erro no download
                            statusDownload = retorno.statusDownload;
                            dynamic erros = retorno.erros;
                        }
                    }
                    else
                    {
                        // NFe não foi autorizada com sucesso ou retorno diferente de 100 / 150
                        motivo = retorno.motivo;
                        xMotivo = retorno.xMotivo;
                        dynamic erros = retorno.erros;
                    }
                }
                else
                {
                    // Consulta não foi realizada com sucesso ou com retorno diferente de 200
                    string motivo = retorno.motivo;
                    string xMotivo = retorno.xMotivo;
                    dynamic erros = retorno.erros;
                }
            }
            else
            {
                // NFe não foi enviada com sucesso
                string statusEnvio = retorno.statusEnvio;
                string motivo = retorno.motivo;
                string xMotivo = retorno.xMotivo;
                dynamic erros = retorno.erros;
            }

## Eventos

### Cancelar NFe

Para realizarmos um cancelamento de uma NFe, devemos gerar o objeto do corpo da requisição, utilizando a classe *Cancelamento.Body*, e utilzar o método *Cancelamento.sendPostRequest*, da seguinte forma:
        
        using ns_nfe_core.src.eventos;
        
        static async Task cancelarNFe()
        {
            var requisicaoCancelamento = new Cancelamento.Body
            {
                chNFe = "43210914139046000109550000000257891100116493",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nProt = "143210000654108",
                tpAmb = "2",
                xJust = "CANCELAMENTO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await Cancelamento.sendPostRequest(requisicaoCancelamento,"XP", @"NFe/Eventos/",true);
        }
        
Os parametros informados no método são:

+ *requisicaoCancelamento* =  Objeto contendo as informações do corpo da requisição de cancelamento;
+ "XP" = tpDown = tipo de download, indicando quais os tipos de arquivos serão obtidos no download do evento de cancelamento;
+ *@"NFe/Eventos/"* = diretório onde serão salvos os arquivos obtidos no download do evento de cancelamento;
+ *true* = exibeNaTela = parametro boolean que indica se será exibido na tela, ou não, o PDF obtido no download do evento de cancelamento;

## Carta de Correção para NFe

Para emitirmos uma carta de correção de uma NFe, devemos gerar o objeto do corpo da requisição, utilizando a classe *CartaCorrecao.Body*, e utilzar o método *CartaCorrecao.sendPostRequest*, da seguinte forma:
        
        using ns_nfe_core.src.eventos;
        
        static async Task corrigirNFe()
        {
            var requisicaoCorrecao = new CartaCorrecao.Body
            {
                chNFe = "43210914139046000109550000000257891100116493",
                dhEvento = DateTime.Now.ToString("s") + "-03:00",
                nSeqEvento = "1",
                tpAmb = "2",
                xCorrecao = "CORRECAO REALIZADO PARA FINS DE TESTE DE INTEGRACAO DE EXEMPLO NFE-CORE"
            };

            var retorno = await CartaCorrecao.sendPostRequest(requisicaoCorrecao, "XP", @"NFe/Eventos/",true);
        }
        
Os parametros informados no método são:

+ *requisicaoCorrecao* =  Objeto contendo as informações do corpo da requisição de cancelamento;
+ "XP" = tpDown = tipo de download, indicando quais os tipos de arquivos serão obtidos no download do evento de cancelamento;
+ *@"NFe/Eventos/"* = diretório onde serão salvos os arquivos obtidos no download do evento de cancelamento;
+ *true* = exibeNaTela = parametro boolean que indica se será exibido na tela, ou não, o PDF obtido no download do evento de cancelamento;

## Utilitários


### Informações Adicionais

Para saber mais sobre o projeto NFe API da NS Tecnologia, consulte a [documentação](https://docsnstecnologia.wpcomstaging.com/docs/ns-nfe/)
