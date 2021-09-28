using System;
using ns_nfe_core.src.emissao;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ns_nfe_core.src.commons;

namespace ns_nfe_core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var NFeXML = layoutNFe.gerarNFeXML();
            var retorno = await EmissaoSincrona.sendPostRequest(NFeXML, "X");
            Console.WriteLine(JsonConvert.SerializeObject(retorno));
            
        }
    }
}
