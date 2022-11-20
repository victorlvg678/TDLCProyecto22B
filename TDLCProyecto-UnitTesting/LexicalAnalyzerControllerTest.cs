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
    public class LexicalAnalyzerControllerTest
    {
        private static HttpClient _httpClient = new HttpClient();
        private static string _url = $"{Environment.GetEnvironmentVariable("APP_SCHEMA")}://{Environment.GetEnvironmentVariable("APP_BASE_URL")}:{Environment.GetEnvironmentVariable("PORT")}/getNextState";
        private static Task _program = Task.Run(() => { TDLCProyecto.Program.Main(); });
        private static Encoding _encoding = Encoding.UTF8;
        private static string _mediaType = "application/json";

        [Fact(DisplayName = "Check if response is being received")]
        public void NoBodyResponseTest()
        {
            HttpContent body = new StringContent(string.Empty, _encoding, _mediaType);
            Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);
            response.Wait();

            if (response == null)
                throw new Exception("No response received");

            Assert.True(response.IsCompleted);
        }

        [Fact(DisplayName = "Check if response status code is Not Acceptable")]
        public void NoBodyStatusCodeTest()
        {
            HttpContent body = new StringContent(string.Empty, _encoding, _mediaType);
            Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);
            response.Wait();

            if (response == null || !response.IsCompleted)
                throw new Exception("No response received");

            HttpStatusCode statusCode = response.Result.StatusCode;

            Assert.True(HttpStatusCode.NotAcceptable.Equals(statusCode));
        }

        [Fact(DisplayName = "Check if response returns no success")]
        public void NoBodyResponseSuccessTest()
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

        [Fact(DisplayName = "Check response error message on getNextState controller")]
        public void NoBodyResponseMessageTest()
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

            Assert.True(responseContent.message.Equals("Body must not be empty"));
        }


        [Theory(DisplayName = "Check if validates correct integer numbers")]
        [InlineData("123")]
        [InlineData("0")]
        [InlineData("12456")]
        [InlineData("420")]
        [InlineData("13")]
        [InlineData("-123")]
        [InlineData("-52")]
        [InlineData("-12456")]
        [InlineData("-420")]
        [InlineData("-13")]
        [InlineData("+123")]
        [InlineData("+52")]
        [InlineData("+12456")]
        [InlineData("+420")]
        [InlineData("+13")]
        public void ValidIntegerNumbers(string input)
        {
            int currentState = 0;
            int currentChar = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var payload = new { input = input, currentChar = currentChar, state = currentState};
                var jsonbody = JsonConvert.SerializeObject(payload);

                HttpContent body = new StringContent(jsonbody, _encoding, _mediaType);

                Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

                response.Wait();

                if (response == null || !response.IsCompleted)
                    throw new Exception("No response received");

                string content = response.Result.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(content))
                    throw new Exception("Empty response received");

                ResponseMessage responseContent = JsonConvert.DeserializeObject<ResponseMessage>(content);

                if (responseContent == null)
                    throw new Exception("Response message is empty");

                if (responseContent.message == null)
                    throw new Exception("Response message content is empty");

                if (responseContent.state < 0)
                    throw new Exception("Response returned invalid state");

                if (responseContent.currentChar < 0)
                    throw new Exception("Response returned invalid current char position");

                if (string.IsNullOrWhiteSpace(responseContent.input))
                    throw new Exception("Response return invalid input");

                currentChar = responseContent.currentChar;
                currentState = responseContent.state;
            }

            Assert.True(currentState == 3);
        }

        [Theory(DisplayName = "Check if validates correct float numbers")]
        [InlineData("4.20")]
        [InlineData("0.1")]
        [InlineData("0.001")]
        [InlineData("420.69")]
        [InlineData("123.321")]
        [InlineData("+4.20")]
        [InlineData("+1.1")]
        [InlineData("+1.001")]
        [InlineData("+420.69")]
        [InlineData("+123.321")]
        [InlineData("-4.20")]
        [InlineData("-1.1")]
        [InlineData("-1.001")]
        [InlineData("-420.69")]
        [InlineData("-123.321")]
        public void ValidFloatNumbers(string input)
        {
            int currentState = 0;
            int currentChar = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var payload = new { input = input, currentChar = currentChar, state = currentState };
                var jsonbody = JsonConvert.SerializeObject(payload);

                HttpContent body = new StringContent(jsonbody, _encoding, _mediaType);

                Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

                response.Wait();

                if (response == null || !response.IsCompleted)
                    throw new Exception("No response received");

                string content = response.Result.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(content))
                    throw new Exception("Empty response received");

                ResponseMessage responseContent = JsonConvert.DeserializeObject<ResponseMessage>(content);

                if (responseContent == null)
                    throw new Exception("Response message is empty");

                if (responseContent.message == null)
                    throw new Exception("Response message content is empty");

                if (responseContent.state < 0)
                    throw new Exception("Response returned invalid state");

                if (responseContent.currentChar < 0)
                    throw new Exception("Response returned invalid current char position");

                if (string.IsNullOrWhiteSpace(responseContent.input))
                    throw new Exception("Response return invalid input");

                currentChar = responseContent.currentChar;
                currentState = responseContent.state;
            }

            Assert.True(currentState == 5);
        }


        [Theory(DisplayName = "Check if validates correct arithmetic expressions")]
        [InlineData("4+2+3")]
        [InlineData("5-2-1")]
        [InlineData("2+4-3")]
        [InlineData("2*2/2-3+1")]
        [InlineData("2.2+2.3*3/2")]
        [InlineData("138.9+1.6746")]
        [InlineData("255+249.8")]
        [InlineData("899.8+4.981")]
        [InlineData("2.748+44.75")]
        [InlineData("255-249.8")]
        [InlineData("899.8-4.981")]
        [InlineData("2.748-44.75")]
        [InlineData("255/249.8")]
        [InlineData("899.8/4.981")]
        [InlineData("2.748/44.75")]
        [InlineData("255*249.8")]
        [InlineData("899.8*4.981")]
        [InlineData("2.748*44.75")]
        public void ValidArithmeticExpressions(string input)
        {
            int currentState = 0;
            int currentChar = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var payload = new { input = input, currentChar = currentChar, state = currentState };
                var jsonbody = JsonConvert.SerializeObject(payload);

                HttpContent body = new StringContent(jsonbody, _encoding, _mediaType);

                Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

                response.Wait();

                if (response == null || !response.IsCompleted)
                    throw new Exception("No response received");

                string content = response.Result.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(content))
                    throw new Exception("Empty response received");

                ResponseMessage responseContent = JsonConvert.DeserializeObject<ResponseMessage>(content);

                if (responseContent == null)
                    throw new Exception("Response message is empty");

                if (responseContent.message == null)
                    throw new Exception("Response message content is empty");

                if (responseContent.state < 0)
                    throw new Exception("Response returned invalid state");

                if (responseContent.currentChar < 0)
                    throw new Exception("Response returned invalid current char position");

                if (string.IsNullOrWhiteSpace(responseContent.input))
                    throw new Exception("Response return invalid input");

                currentChar = responseContent.currentChar;
                currentState = responseContent.state;
            }

            Assert.True(currentState == 5 || currentState == 3);
        }

        [Theory(DisplayName = "Check if validates correct exponents")]
        [InlineData("1E+1")]
        [InlineData("2E+2")]
        [InlineData("3E+3")]
        [InlineData("4E+4")]
        [InlineData("5E+5")]
        [InlineData("1E-1")]
        [InlineData("2E-2")]
        [InlineData("3E-3")]
        [InlineData("4E-4")]
        [InlineData("5E-5")]
        [InlineData("1.1E+1")]
        [InlineData("2.2E+2")]
        [InlineData("3.3E+3")]
        [InlineData("4.4E+4")]
        [InlineData("5.5E+5")]
        [InlineData("1.1E-1")]
        [InlineData("2.2E-2")]
        [InlineData("3.3E-3")]
        [InlineData("4.4E-4")]
        [InlineData("5.5E-5")]
        [InlineData("-1E+1")]
        [InlineData("-2E+2")]
        [InlineData("-3E+3")]
        [InlineData("-4E+4")]
        [InlineData("-5E+5")]
        [InlineData("-1E-1")]
        [InlineData("-2E-2")]
        [InlineData("-3E-3")]
        [InlineData("-4E-4")]
        [InlineData("-5E-5")]
        [InlineData("-1.1E+1")]
        [InlineData("-2.2E+2")]
        [InlineData("-3.3E+3")]
        [InlineData("-4.4E+4")]
        [InlineData("-5.5E+5")]
        [InlineData("-1.1E-1")]
        [InlineData("-2.2E-2")]
        [InlineData("-3.3E-3")]
        [InlineData("-4.4E-4")]
        [InlineData("-5.5E-5")]
        
        public void ValidExponents(string input)
        {
            int currentState = 0;
            int currentChar = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var payload = new { input = input, currentChar = currentChar, state = currentState };
                var jsonbody = JsonConvert.SerializeObject(payload);

                HttpContent body = new StringContent(jsonbody, _encoding, _mediaType);

                Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

                response.Wait();

                if (response == null || !response.IsCompleted)
                    throw new Exception("No response received");

                string content = response.Result.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(content))
                    throw new Exception("Empty response received");

                ResponseMessage responseContent = JsonConvert.DeserializeObject<ResponseMessage>(content);

                if (responseContent == null)
                    throw new Exception("Response message is empty");

                if (responseContent.message == null)
                    throw new Exception("Response message content is empty");

                if (responseContent.state < 0)
                    throw new Exception("Response returned invalid state");

                if (responseContent.currentChar < 0)
                    throw new Exception("Response returned invalid current char position");

                if (string.IsNullOrWhiteSpace(responseContent.input))
                    throw new Exception("Response return invalid input");

                currentChar = responseContent.currentChar;
                currentState = responseContent.state;
            }

            Assert.True(currentState == 8);
        }

        [Theory(DisplayName = "Check if denies wrong inputs")]
        [InlineData("+0")]
        [InlineData("-0")]
        [InlineData("?")]
        [InlineData(")")]
        [InlineData("(")]
        [InlineData("%")]
        [InlineData("&")]
        [InlineData("/3")]
        [InlineData("*2")]
        [InlineData("\\")]
        [InlineData("|")]
        [InlineData("1E1")]
        [InlineData("4.4E4")]
        [InlineData("5.5E5")]
        [InlineData("1.1E1")]
        [InlineData("2.2E2")]
        [InlineData("3.3E3")]
        [InlineData("4.4E4")]
        [InlineData("5.5E5")]
        [InlineData("5.5.5")]

        public void WrongInputs(string input)
        {
            int currentState = 0;
            int currentChar = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var payload = new { input = input, currentChar = currentChar, state = currentState };
                var jsonbody = JsonConvert.SerializeObject(payload);

                HttpContent body = new StringContent(jsonbody, _encoding, _mediaType);

                Task<HttpResponseMessage> response = _httpClient.PostAsync(_url, body);

                response.Wait();

                if (response == null || !response.IsCompleted)
                    throw new Exception("No response received");

                string content = response.Result.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(content))
                    throw new Exception("Empty response received");

                ResponseMessage responseContent = JsonConvert.DeserializeObject<ResponseMessage>(content);

                if (responseContent == null)
                    throw new Exception("Response message is empty");

                if (responseContent.message == null)
                    throw new Exception("Response message content is empty");

                if (responseContent.state < 0)
                    throw new Exception("Response returned invalid state");

                if (responseContent.currentChar < 0)
                    throw new Exception("Response returned invalid current char position");

                if (string.IsNullOrWhiteSpace(responseContent.input))
                    throw new Exception("Response return invalid input");

                currentChar = responseContent.currentChar;
                currentState = responseContent.state;
            }

            Assert.True(currentState == 10);
        }
    }
}
