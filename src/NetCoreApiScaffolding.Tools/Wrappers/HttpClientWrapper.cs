using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Tools.Contracts;

namespace NetCoreApiScaffolding.Tools.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption,
            CancellationToken cancellationToken)
        {
            return _httpClient.SendAsync(request, completionOption, cancellationToken);
        }
    }
}