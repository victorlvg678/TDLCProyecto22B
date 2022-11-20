using System;
namespace TDLCProyecto.Classes
{
    public class ResponseErrorMessage
    {
        public bool success { get; set; }
        public string message { get; set; }

        public ResponseErrorMessage()
        {

        }

        public ResponseErrorMessage(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }
}
