using System;
using System.Diagnostics;
using System.IO;

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
                gravarLinhaLog("[ERRO_GRAVAR_LINHA_LOG]: " + ex.Message);
            }

        }

        public static void salvarArquivo(string caminho, string nomeArquivo, string extensao, string conteudo) 
        {
            //string caminhoSalvar = Path.Combine(caminho, nomeArquivo + extensao);

            try
            {
                if (!Directory.Exists(caminho))
                    Directory.CreateDirectory(caminho);
            }

            catch(Exception ex)
            {
                gravarLinhaLog("[ERRO_SALVAR_ARQUIVO]: " + ex.Message);
            }

            try 
            { 
                switch (extensao)
                {
                    case var target when extensao.Contains(".xml"):

                        try
                        {
                            conteudo = conteudo.Replace(@"\""", "");
                            using (StreamWriter outputFile = new StreamWriter(caminho + nomeArquivo + extensao))
                            {
                                outputFile.WriteLine(conteudo);
                            };
                        }

                        catch (Exception ex)
                        {
                            gravarLinhaLog("[ERRO_SALVAR_XML]: " + ex.Message);
                        }

                        break;

                    case var target when extensao.Contains(".json"):

                        try
                        {
                            using (StreamWriter outputFile = new StreamWriter(caminho + nomeArquivo + extensao))
                            {
                                outputFile.WriteLine(conteudo);
                            };
                        }

                        catch(Exception ex)
                        {
                            gravarLinhaLog("[ERRO_SALVAR_JSON]: " + ex.Message);
                        }

                        break;

                    case var target when extensao.Contains(".pdf"):

                        try
                        {
                            caminho = Path.Combine(caminho, nomeArquivo + extensao);
                            byte[] bytes = Convert.FromBase64String(conteudo);
                            if (File.Exists(caminho))
                                File.Delete(caminho);
                            FileStream stream = new FileStream(caminho, FileMode.CreateNew);
                            BinaryWriter writer = new BinaryWriter(stream);
                            writer.Write(bytes, 0, bytes.Length);
                            writer.Close();
                        }

                        catch (Exception ex)
                        {
                            gravarLinhaLog("[ERRO_SALVAR_PDF]: " + ex.Message);
                        }

                        break;
                }
            } 

            catch (Exception ex) 
            {
                gravarLinhaLog("[ERRO_SALVAR_ARQUIVOS]: " + ex.Message);
            }
        }

        public static string gerarHashSHA1(string chave, string caminhoImagem)
        {
            byte[] imageArray = File.ReadAllBytes(caminhoImagem);

            string base64ImagemEntrega = Convert.ToBase64String(imageArray);

            string hash = chave + base64ImagemEntrega;

            string hashEntrega = "";

            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(hash);
                System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1Managed();
                byte[] res = sha.ComputeHash(data);
                hashEntrega = Convert.ToBase64String(res);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return hashEntrega;
        }

        public static void exibirPDF(string caminhoSalvar, string chave, string extensao)
        {
            string arquivo = caminhoSalvar.Replace("/", @"\") + chave + extensao;
            
            try {
                Process.Start(new ProcessStartInfo(arquivo) { UseShellExecute = true }); 
            }
            catch (Exception ex)
            { 
                gravarLinhaLog("[ERRO_EXIBIR_PDF]: " + ex.Message + " Arquivo: " + arquivo); 
            }
        }
    }
}
