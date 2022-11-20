namespace TDLCProyecto.Classes
{
    public class Response
    {
        private readonly string _content;
        private readonly int _statusCode;

        public string Content => _content;
        public int StatusCode => _statusCode;

        public Response()
        {
            _content = string.Empty;
            _statusCode = 500;
        }

        public Response(string content)
        {
            _content = content ?? string.Empty;
        }

        public Response(string content, int statusCode)
        {
            _content = content ?? string.Empty;
            _statusCode = statusCode;
        }
    }
}
