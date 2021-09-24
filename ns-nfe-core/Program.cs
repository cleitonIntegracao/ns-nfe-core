using System;
using ns_nfe_core.src.emissao;
using System.Threading.Tasks;

namespace ns_nfe_core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //download.body corpo = new download.body
            //{
            //    chNFe = "43210907364617000135550000000224301848236016",
            //    tpAmb = "2",
            //    tpDown = "XP"
            //};

            //var retorno = await download.sendPostRequest(corpo);
            //Console.WriteLine(retorno);
            //Console.ReadKey();

            statusProcessamento.body corpo = new statusProcessamento.body
            {
                nsNRec = "3670243",
                CNPJ = configParceiro.CNPJ
            };

            var retorno = await statusProcessamento.sendPostRequest(corpo);
            Console.WriteLine(retorno);
            Console.ReadKey();
        }
    }
}
