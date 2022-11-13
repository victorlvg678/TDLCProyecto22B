namespace TDLCProyecto 
{
    public class Program 
    {
        public static void Main()
        {
            string schema = Environment.GetEnvironmentVariable("APP_SCHEMA") ?? "http";
            string base_url = Environment.GetEnvironmentVariable("APP_BASE_URL") ?? "localhost";
            string port = Environment.GetEnvironmentVariable("PORT") ?? "9091";

            WebServer webServer = new WebServer($"{schema}://{base_url}:{port}/");
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
