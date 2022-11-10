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

        public override string ToString()
        {
            if (_request == null)
                return string.Empty;

            Stream bodyStream = _request.InputStream;
            byte[] bodyData = new byte[_request.ContentLength64];
            Task<int> bodyStreamTask = bodyStream.ReadAsync(bodyData, 0, (int) _request.ContentLength64);

            StringBuilder stringBuilder = new StringBuilder();

            NameValueCollection nameValueCollection = _request.Headers;
            var headers = nameValueCollection.AllKeys.SelectMany(nameValueCollection.GetValues, (key, value) => new { Key = key, Value = value });

            stringBuilder.AppendLine($"\nRequest #{_sequence}");
            stringBuilder.AppendLine("\nHeaders:{");

            foreach (var header in headers)
                stringBuilder.AppendLine($"\t{header.Key}: \"{header.Value}\";");
            
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine($"URL: \"{_request.Url}\"");
            stringBuilder.AppendLine($"Method: {_request.HttpMethod}");

            bodyStreamTask.Wait();

            if (bodyStreamTask.IsCompleted)
                stringBuilder.AppendLine($"body: {{\" {Encoding.UTF8.GetString(bodyData)} \"}}");

            return stringBuilder.ToString();
        }
        #endregion
    }
}
