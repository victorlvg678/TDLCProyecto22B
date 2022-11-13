using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDLCProyecto.Classes
{
    public class ResponseMessage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string input { get; set; }
        public int currentChar { get; set; }
        public int state { get; set; }

        public ResponseMessage()
        {

        }

        public ResponseMessage(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }
}
