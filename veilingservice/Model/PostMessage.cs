using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Model
{
    public class PostMessage
    {
        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        public PostMessage() {
            Message = "succes";

            ErrorMessage = "";
        }

        public PostMessage(string error)
        {
            Message = "failure";
            ErrorMessage = error;
        }
    }
}
