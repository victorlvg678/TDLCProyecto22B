using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TDLCProyecto.Classes;
using Xunit;

namespace TDLCProyecto_UnitTesting
{
    
    public class GETMethodTest
    {
        private static HttpClient _httpClient = new HttpClient();
        private static string _url = $"{Environment.GetEnvironmentVariable("APP_SCHEMA")}://{Environment.GetEnvironmentVariable("APP_BASE_URL")}:{Environment.GetEnvironmentVariable("PORT")}/";
        private static Task _program = Task.Run(() => { TDLCProyecto.Program.Main(); });

        [Fact(DisplayName = "GET request to / - Check if response is received")]
        public void NoControllerResponseTest()
        {
            Task<HttpResponseMessage> response = _httpClient.GetAsync(_url);
            response.Wait();

            if (response == null)
                throw new Exception("No response received");

            Assert.True(response.IsCompleted);
        }

        [Fact(DisplayName = "GET request to / - Check if status code is Method Not Allowed")]
        public void NoControllerStatusCodeTest()
        {
            Task<HttpResponseMessage> response = _httpClient.GetAsync(_url);
            response.Wait();

            if (response == null || !response.IsCompleted)
                throw new Exception("No response received");

            HttpStatusCode statusCode = response.Result.StatusCode;

            Assert.True(HttpStatusCode.MethodNotAllowed.Equals(statusCode));
        }

        [Fact(DisplayName = "GET request to / - Check if response returns no success")]
        public void NoControllerResponseSuccessTest()
        {
            Task<HttpResponseMessage> response = _httpClient.GetAsync(_url);

            response.Wait();

            if (response == null || !response.IsCompleted)
                throw new Exception("No response received");

            string content = response.Result.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("Empty response received");

            ResponseErrorMessage responseContent = JsonConvert.DeserializeObject<ResponseErrorMessage>(content);

            if (responseContent == null)
                throw new Exception("Response error message is empty");


            Assert.False(responseContent.success);
        }

        [Fact(DisplayName = "GET request to / - Check response message")]
        public void NoControllerResponseMessageTest()
        {
            Task<HttpResponseMessage> response = _httpClient.GetAsync(_url);

            response.Wait();

            if (response == null || !response.IsCompleted)
                throw new Exception("No response received");

            string content = response.Result.Content.ReadAsStringAsync().Result;

            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("Empty response received");

            ResponseErrorMessage responseContent = JsonConvert.DeserializeObject<ResponseErrorMessage>(content);

            if (responseContent == null)
                throw new Exception("Response error message is empty");

            if (responseContent.message == null)
                throw new Exception("Response error message content is empty");

            Assert.True(responseContent.message.Equals("GET method not allowed"));
        }
    }
}