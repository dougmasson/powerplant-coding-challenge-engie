using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerCalculator.WebApi.IntegrationTests.Helper
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Invoke method <c>POST</c> and deserialize response.
        /// </summary>
        /// <typeparam name="Req">Request.</typeparam>
        /// <typeparam name="Resp">Response.</typeparam>
        /// <param name="requestUri">Value of Uri.</param>
        /// <param name="request">Value request object.</param>
        /// <returns>Reponse deserialize.</returns>
        public static async Task<Resp> PostAndDeserialize<Req, Resp>(this HttpClient client, string requestUri, Req request)
        {
            var response = await client.PostAsJsonAsync(requestUri, request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Resp>(result, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
        }
    }
}
