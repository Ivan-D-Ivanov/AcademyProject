using AcademyProjectModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AcademyProjectSL.ServiceProviders
{
    public class PurchaseServiceProvider
    {

        public async Task<AuthorAdditionalInfo> GetAdditionalInfo()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7056/AdditionalInfo/GetAdditionalInfo");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var authorAddInfo = JsonConvert.DeserializeObject<AuthorAdditionalInfo>(result);

            return authorAddInfo;
        }
    }
}
