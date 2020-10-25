using System.Net.Http;
using System.Threading.Tasks;

namespace SPapi.NET
{
    internal static class Request
    {
        private static HttpClient _client;
        static Request()
        {
            _client = new HttpClient();
        }
        private static readonly string route = "https://sp-api.ru/spm/";

        public static async Task<string> Get(string request)
        {
            var path = route + request;

            var result = await _client.GetAsync(path);

            var response = await result.Content.ReadAsStringAsync();

            return response;
        }
    }
}
