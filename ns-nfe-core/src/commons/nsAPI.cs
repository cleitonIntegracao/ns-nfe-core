using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace ns_nfe_core.src.commons
{

    class nsAPI
    {
        public static async Task<string> postRequest(string url, string body, string tpConteudo = "json") 
        {
            string responseAPI = "";

            var apiClient = new HttpClient();

            StringContent requestBody = new StringContent(body,Encoding.UTF8, "application/"+tpConteudo);

            apiClient.DefaultRequestHeaders.Add("X-AUTH-TOKEN", configParceiro.token);

            try
            {
                var getResponse = await apiClient.PostAsync(url, requestBody);
                responseAPI = await getResponse.Content.ReadAsStringAsync();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return responseAPI;
        }
    }

}
