using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace TDLCProyecto.Classes
{
    public class Request
    {
        #region private members
        private readonly HttpListenerRequest _request;
        private readonly int _sequence;
        #endregion

        #region constructors
        public Request(HttpListenerRequest request, int sequence)
        {
            _request = request;
            _sequence = sequence;
        }

        public async Task<string> getBody()
        { 
            Stream bodyStream = _request.InputStream;
            byte[] bodyData = new byte[_request.ContentLength64];
            Task<int> bodyStreamTask = bodyStream.ReadAsync(bodyData, 0, (int) _request.ContentLength64);
            
            bodyStreamTask.Wait();
            
            return Encoding.UTF8.GetString(bodyData);
        }

        public List<Header> getHeaders()
        { 
            NameValueCollection nameValueCollection = _request.Headers;
            return nameValueCollection.AllKeys.SelectMany(nameValueCollection.GetValues, (key, value) => new Header(key, value)).ToList();
        }

        public StringBuilder getRequestData()
        { 
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"\nRequest #{_sequence}");
            stringBuilder.AppendLine("\nHeaders:{");

            foreach (Header header in getHeaders())
                stringBuilder.AppendLine($"\t{header.Key}: \"{header.Value}\";");
            
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine($"URL: \"{_request.Url}\"");
            stringBuilder.AppendLine($"Method: {_request.HttpMethod}");

            return stringBuilder;
        }


        public override string ToString()
        {
            if (_request == null)
                return string.Empty;

            Task<string> bodyTask = getBody();
            StringBuilder stringBuilder = getRequestData();

            stringBuilder.AppendLine($"body: {{\"{bodyTask.Result}\"}}");

            return stringBuilder.ToString();
        }
        #endregion
    }
}
