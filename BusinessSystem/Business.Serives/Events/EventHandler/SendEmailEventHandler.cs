using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Utils;

namespace Business.Serives.Events
{
    public class SendEmailEventHandler : IEventHandler<EmailSendEvent>
    {

        public void HandleEvent(EmailSendEvent eventMessage)
        {
              
        }
    }
}
