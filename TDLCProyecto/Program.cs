namespace TDLCProyecto 
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            WebServer webServer = new WebServer($"http://localhost:9091/");
            try
            {
                webServer.Start();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                webServer.Stop();
            }
        }
    }

}
