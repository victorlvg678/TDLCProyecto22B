using TDLCProyecto.Classes;
using Serilog;

namespace TDLCProyecto.Controllers
{
    public static class CORSController
    {
        public static Response getCORSResponse(Request request, ILogger logger)
        {
            if (request == null)
            {
                logger.Error("Request is empty!");
                return ErrorController.getNotAcceptable("Request must not be empty", logger);
            }

            return new Response("CORS headers returned", 200);
        }
    }
}
