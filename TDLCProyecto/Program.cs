namespace TDLCProyecto 
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            WebServer webServer = new WebServer("http://localhost:8001/");
            webServer.Start();
        }
    }

}
