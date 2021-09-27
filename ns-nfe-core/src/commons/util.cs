using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ns_nfe_core.src.commons
{
    class util
    {
        public static void gravarLinhaLog(string registro)
        {
            string caminho = @".\logs\";

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(@".\logs\" + DateTime.UtcNow.ToString("MMddyyyy") + ".log", true))
                {
                    outputFile.WriteLine(DateTime.Now.ToShortDateString() + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff") + " - " + registro);
                }
            } 
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void salvarArquivo(string caminho, string nomeArquivo, string extensao, string conteudo) 
        {
            string caminhoSalvar = Path.Combine(caminho, nomeArquivo + extensao);

            try
            {
                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try { } catch (Exception ex) { }
        }

        public static void salvarXML(string conteudo, string caminho, string nome, string extensao, string tpEvento = "", string nSeqEvento = "")
        {
            string localParaSalvar = caminho + tpEvento + nome + nSeqEvento + extensao;
            string ConteudoSalvar = "";
            ConteudoSalvar = conteudo.Replace(@"\""", "");
            File.WriteAllText(localParaSalvar, ConteudoSalvar);
        }

        public static void salvarJSON(string conteudo, string caminho, string nome, string extensao, string tpEvento = "", string nSeqEvento = "")
        {
            string localParaSalvar = caminho + tpEvento + nome + nSeqEvento + extensao;
            File.WriteAllText(localParaSalvar, conteudo);
        }

        public static void salvarPDF(string conteudo, string caminho, string nome, string extensao, string tpEvento = "", string nSeqEvento = "")
        {
            string localParaSalvar = caminho + tpEvento + nome + nSeqEvento + extensao;
            byte[] bytes = Convert.FromBase64String(conteudo);
            if (File.Exists(localParaSalvar))
                File.Delete(localParaSalvar);
            FileStream stream = new FileStream(localParaSalvar, FileMode.CreateNew);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
        }
    }
}
