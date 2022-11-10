using Serilog;
using TDLCProyecto.Classes;

namespace TDLCProyecto
{
    public class WebServer
    {
        private readonly string _baseURL;

        public WebServer(string baseURL)
        {
            _baseURL = baseURL;
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

                while (true)
                {
                    System.Net.HttpListenerContext ctx = listener.GetContext();
                    System.Threading.Tasks.TaskFactory taskFactory = new System.Threading.Tasks.TaskFactory();

                    requests++;
                    Request request = new Request(ctx.Request, requests);
                    logger.Debug(request.ToString());

                    Task task = taskFactory.StartNew(() => 
                    {
                        string methodName = "";
                        
                        if (ctx.Request?.Url?.Segments.Count() > 1)
                            methodName = ctx.Request.Url.Segments[1].Replace("/", "");

                        if (string.IsNullOrWhiteSpace(methodName))
                        {
                            logger.Information("User requested /");
                            //HttpListenerResponse
                        };
                        

                    });
                }
            }
        }
    }
}
