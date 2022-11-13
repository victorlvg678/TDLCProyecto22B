using TDLCProyecto.Classes;
using Newtonsoft.Json;
using Serilog;

namespace TDLCProyecto.Controllers
{
    public static class ErrorController
    {
        public static Response getNotFound(string message, ILogger logger)
        {
            int statusCode = 404;

            if (string.IsNullOrWhiteSpace(message))
                message = "Not found";

            logger.Debug($"Sending ({statusCode}) NotFound response - Message: \"{message}\"");
            ResponseErrorMessage response = new ResponseErrorMessage(false, message);

            return new Response(JsonConvert.SerializeObject(response), statusCode);
        }

        public static Response getMethodNotAllowed(string message, ILogger logger)
        {
            int statusCode = 405;

            if (string.IsNullOrWhiteSpace(message))
                message = "Method not Allowed";

            logger.Debug($"Sending ({statusCode}) MethodNotAllowed response - Message: \"{message}\"");
            ResponseErrorMessage response = new ResponseErrorMessage(false, message);

            return new Response(JsonConvert.SerializeObject(response), statusCode);
        }
        
        public static Response getNotAcceptable(string message, ILogger logger)
        {
            int statusCode = 406;

            if (string.IsNullOrWhiteSpace(message))
                message = "Not acceptable";

            logger.Debug($"Sending ({statusCode}) NotAcceptable response - Message: \"{message}\"");
            ResponseErrorMessage response = new ResponseErrorMessage(false, message);

            return new Response(JsonConvert.SerializeObject(response), statusCode);
        }

        public static Response getBadRequest(string message, ILogger logger)
        {
            int statusCode = 400;

            if (string.IsNullOrWhiteSpace(message))
                message = "Bad Request";

            logger.Debug($"Sending ({statusCode}) NotAcceptable response - Message: \"{message}\"");
            ResponseErrorMessage response = new ResponseErrorMessage(false, message);

            return new Response(JsonConvert.SerializeObject(response), statusCode);
        }
    }
}
