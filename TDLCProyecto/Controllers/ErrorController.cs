using TDLCProyecto.Classes;

namespace TDLCProyecto.Controllers
{
    public static class ErrorController
    {
        public static Response getNotFound() => new Response("asd", 200);
        public static Response getMethodNotAllowed() => new Response("def", 200);
    }
}
