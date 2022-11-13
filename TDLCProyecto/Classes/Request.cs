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
        private readonly string _body;
        private readonly string _method;
        private string _controller;
        #endregion

        #region constructors
        public Request(HttpListenerRequest request, int sequence)
        {
            _request = request;
            _sequence = sequence;
            _method = request.HttpMethod;
            _body =  getBody().Result;
            _controller = string.Empty;
        }

        public async Task<string> getBody()
        {
            if (_body != null)
            { 
                return _body;
            }

            Stream bodyStream = _request.InputStream;
            byte[] bodyData = new byte[_request.ContentLength64];
            Task<int> bodyStreamTask = bodyStream.ReadAsync(bodyData, 0, (int) _request.ContentLength64);
            
            bodyStreamTask.Wait();
            
            return Encoding.UTF8.GetString(bodyData);
        }

        public string getMethod() => _method;

        public string Controller
        { 
            set => _controller = !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
            get => _controller;
        }

        public int Sequence => _sequence;

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
            stringBuilder.AppendLine($"Method: {_method}");

            stringBuilder.AppendLine($"body: {{\"{_body}\"}}");

            return stringBuilder;
        }


        public override string ToString()
        {
            if (_request == null)
                return string.Empty;
;
            return getRequestData().ToString();
        }
        #endregion
    }
}
