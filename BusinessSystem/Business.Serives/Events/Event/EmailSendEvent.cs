using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Serives.Events
{
    public class EmailSendEvent
    {
        public string Content { get; private set; }
        public string Subject { get; private set; }
        public string ToEmail { get; private set; }
        public DateTime SendTime { get; set; }

        public EmailSendEvent(string content, string subejct, string toEmail)
        {
            Content = content;
            Subject = subejct;
            ToEmail = toEmail;
            SendTime = DateTime.Now;
        }
    }
}
