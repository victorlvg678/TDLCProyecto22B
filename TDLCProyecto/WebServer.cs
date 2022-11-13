using Serilog;
using TDLCProyecto.Classes;

namespace TDLCProyecto
{
    public class WebServer
    {
        private readonly string _baseURL;
        private bool _running;

        public WebServer(string baseURL)
        {
            _baseURL = baseURL;
            _running = true;
        }

        public void Start()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            ILogger logger = Log.Logger; 
            
            using (System.Net.HttpListener listener = new System.Net.HttpListener())
            {
                listener.Prefixes.Add(_baseURL);
                listener.Start();
                int requests = 0;
                logger.Information($"Listening on \"{_baseURL}\"...");

                while (_running)
                {
                    System.Net.HttpListenerContext context = listener.GetContext();
                    System.Threading.Tasks.TaskFactory taskFactory = new System.Threading.Tasks.TaskFactory();

                    requests++;
                    Request request = new Request(context.Request, requests);
                    logger.Debug(request.ToString());

                    Task task = taskFactory.StartNew(() => 
                    {
                        string methodName = "";
                        
                        if (context.Request?.Url?.Segments.Count() > 1)
                            methodName = context.Request.Url.Segments[1].Replace("/", "");

                        if (string.IsNullOrWhiteSpace(methodName))
                        {
                            logger.Information("User requested /");
                        };

                        using (System.Net.HttpListenerResponse response = context.Response)
                        {
                            response.Headers.Set("Content-Type", "text/plain");

                            string data = "Hello there!";
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);
                            response.ContentLength64 = buffer.Length;

                            using (Stream ros = response.OutputStream)
                            {
                                ros.Write(buffer, 0, buffer.Length);
                            }
                        }
                    });
                }
            }
        }

        public void Stop()
        { 
            _running = false;
        }
    }
}
