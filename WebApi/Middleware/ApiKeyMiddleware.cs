

namespace WebApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// taken from https://www.daniaal.com/configuring-swagger-in-net-6/
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyMiddleware) ");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (apiKey is not null && !apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"Unauthorized client. (Using ApiKeyMiddleware) - provided key: {extractedApiKey}");
                return;
            }
            await _next(context);
        }

    }
}
