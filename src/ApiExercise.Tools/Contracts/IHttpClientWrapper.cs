using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExercise.Tools.Contracts
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption,
            CancellationToken cancellationToken);
    }
}