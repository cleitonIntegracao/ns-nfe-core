using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ns_nfe_core.src.commons
{

    class NSAPI
    {
        public static async Task<string> postRequest(string url, string body, string tpConteudo = "json") 
        {
            string responseAPI;
            var apiClient = new HttpClient();

            StringContent requestBody = new StringContent(body, Encoding.UTF8, "application/" + tpConteudo);

            apiClient.DefaultRequestHeaders.Add("X-AUTH-TOKEN", ConfigParceiro.token);

            try
            {
                Util.gravarLinhaLog("[URL_ENVIO] " + url);
                Util.gravarLinhaLog("[DADOS_ENVIO] " + body);

                var getResponse = await apiClient.PostAsync(url, requestBody);
                responseAPI = await getResponse.Content.ReadAsStringAsync();

                Util.gravarLinhaLog("[DADOS_RESPOSTA] " + responseAPI);

                return responseAPI;
            }

            catch (Exception ex)
            {
                Util.gravarLinhaLog("[ERRO_ENVIO_DADOS_API]: " + ex.Message);
                return ex.Message;
            }

        }
    }

}
