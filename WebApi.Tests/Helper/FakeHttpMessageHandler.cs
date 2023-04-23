using System.Net;

namespace WebApi.Tests.Helper
{
    /// <summary>
    /// credit: https://stackoverflow.com/questions/68688904/how-to-mock-httpclient-using-nsubstitute
    /// </summary>
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(MockSend(request, cancellationToken));
        }
        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return MockSend(request, cancellationToken);
        }

        public virtual HttpResponseMessage MockSend(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
