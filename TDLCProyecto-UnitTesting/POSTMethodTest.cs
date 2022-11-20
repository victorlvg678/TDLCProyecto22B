using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TDLCProyecto.Classes;
using Xunit;

namespace TDLCProyecto_UnitTesting
{
    public class POSTMethodTest
    {
        private static HttpClient _httpClient = new HttpClient();
        private static string _url = $"{Environment.GetEnvironmentVariable("APP_SCHEMA")}://{Environment.GetEnvironmentVariable("APP_BASE_URL")}:{Environment.GetEnvironmentVariable("PORT")}/";
        private static Task _program = Task.Run(() => { TDLCProyecto.Program.Main(); });
        private static Encoding _encoding = Encoding.UTF8;
        private static string _mediaType = "application/json";

        [Fact(DisplayName = "Empty POST request to / - Check if response is being received")]
        public void NoControllerAndNoBodyResponseTest()
        {
            HttpContent body = new StringContent(string.Empty, _encoding, _mediaType);
            Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);
            response.Wait();

            if (response == null)
                throw new Exception("No response received");

            Assert.True(response.IsCompleted);
        }

        [Fact(DisplayName = "Empty POST request to / - Check if response status code is Not Found")]
        public void NoControllerAndNoBodyStatusCodeTest()
        {
            HttpContent body = new StringContent(string.Empty, _encoding, _mediaType);
            Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);
            response.Wait();

            if (response == null || !response.IsCompleted)
                throw new Exception("No response received");

            HttpStatusCode statusCode = response.Result.StatusCode;

            Assert.True(HttpStatusCode.NotFound.Equals(statusCode));
        }

        [Fact(DisplayName = "Empty POST request to / - Check if response returns no success")]
        public void NoControllerAndNoBodyResponseSuccessTest()
        {
            HttpContent body = new StringContent(string.Empty, _encoding, _mediaType);
            Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

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

        [Fact(DisplayName = "Empty POST request to / - Check response message")]
        public void NoControllerAndNoBodyResponseMessageTest()
        {
            HttpContent body = new StringContent(string.Empty, _encoding, _mediaType);
            Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

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

            Assert.True(responseContent.message.Equals("Controller not found"));
        }
    }
}
