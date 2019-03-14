using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AspNetCore.Services
{
    public class SharePointService:ISharePointService
    {
        private string _apiUrl;

        public SharePointService(IOptions<AppSettings> settings)
        {
            _apiUrl = settings.Value.SharePointEndpoint;
        }
        async Task<T> ISharePointService.GetData<T>()
        {
            HttpClientHandler handler = new HttpClientHandler {UseDefaultCredentials = true};
            var client = new HttpClient(handler);
            var result = await client.GetStringAsync(_apiUrl);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }

    public interface ISharePointService
    {
        Task<T> GetData<T>();
    }
}