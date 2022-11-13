using TDLCProyecto.Classes;
using TDLCProyecto.Classes.LexicalAnalyzer;
using Newtonsoft.Json;
using Serilog;

namespace TDLCProyecto.Controllers
{
    public static class LexicalAnalyzerController
    {
        public static Response getNextState(Request request, ILogger logger)
        {
            if (request == null)
            {
                logger.Error("LexicalAnalyzerController received an empty request!");
                return ErrorController.getNotAcceptable("Empty request", logger);
            }

            if (string.IsNullOrWhiteSpace(request.getBody().Result))
            {
                logger.Error($"Request #{request.Sequence} - Body is empty!");
                return ErrorController.getNotAcceptable("Body must not be empty", logger);
            }

            try
            {
                LexicalAnalyzer lexicalAnalyzer = JsonConvert.DeserializeObject<LexicalAnalyzer>(request.getBody().Result);

                if (lexicalAnalyzer == null)
                {
                    logger.Error($"Request #{request.Sequence} - Wrong body content format!");
                    return ErrorController.getNotAcceptable("Wrong body content", logger);
                }

                logger.Debug($"Request #{request.Sequence} - Input: \"{lexicalAnalyzer.input}\"");
                logger.Debug($"Request #{request.Sequence} - current char: \"{lexicalAnalyzer.currentChar}\"");
                logger.Debug($"Request #{request.Sequence} - Transitioning from state {lexicalAnalyzer.state}");

                lexicalAnalyzer.getNextState();

                logger.Debug($"Request #{request.Sequence} - Transition to next state completed!");
                logger.Debug($"Request #{request.Sequence} - Input: \"{lexicalAnalyzer.input}\"");
                logger.Debug($"Request #{request.Sequence} - current char: \"{lexicalAnalyzer.currentChar}\"");
                logger.Debug($"Request #{request.Sequence} - Transitioned to state {lexicalAnalyzer.state}");
                ResponseMessage responseMessage = new ResponseMessage(true, "Transition to next state completed");

                responseMessage.input = lexicalAnalyzer.input;
                responseMessage.currentChar = lexicalAnalyzer.currentChar;
                responseMessage.state = lexicalAnalyzer.state;
                
                return new Response(JsonConvert.SerializeObject(responseMessage), 200);
            }
            catch (Exception ex)
            {
                logger.Error($"Request #{request?.Sequence} - Something went wrong - Error: \"{ex.Message}\"");

                return ErrorController.getBadRequest(ex.Message, logger);
            }
        }

    }
}
