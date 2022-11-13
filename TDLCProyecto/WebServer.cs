using Serilog;
using TDLCProyecto.Classes;
using TDLCProyecto.Controllers;

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

        public void Start(ILogger logger)
        {
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

                    Task<string> bodyTask = request.getBody();

                    Task task = taskFactory.StartNew(() => 
                    {
                        request.Controller = "";
                        
                        if (context.Request?.Url?.Segments.Count() > 1)
                            request.Controller = context.Request.Url.Segments[1].Replace("/", "");


                        using (System.Net.HttpListenerResponse response = context.Response)
                        {
                            string body = bodyTask.Result;
                            
                            Response resp = request.getMethod().Equals("POST") ?
                                SendDataToController(request, logger) : ErrorController.getMethodNotAllowed($"{request.getMethod()} method not allowed", logger);

                            
                            response.Headers.Set("Content-Type", "text/json");
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(resp.Content);
                            response.ContentLength64 = buffer.Length;
                            response.StatusCode = resp.StatusCode;
                            using (Stream ros = response.OutputStream)
                            {
                                ros.Write(buffer, 0, buffer.Length);
                            }
                        }
                    });
                }
            }
        }

        public Response SendDataToController(Request request, ILogger logger)
        { 
            return request.Controller switch
            {
                "getNextState" => LexicalAnalyzerController.getNextState(request, logger),
                _ => ErrorController.getNotFound("Controller not found", logger)
            };
        }

        public void Stop()
        { 
            _running = false;
        }
    }
}
