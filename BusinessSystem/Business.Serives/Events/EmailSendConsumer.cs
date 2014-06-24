using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Serives.Events
{
    public class EmailSendConsumer : IConsumer<EmailSendEvent>
    {

        public void HandleEvent(EmailSendEvent eventMessage)
        {
            Console.WriteLine("我来了 EmailSendEvent ");
        }
    }
    public class EmailSendConsumer2 : IConsumer<EmailSendEvent>
    {

        public void HandleEvent(EmailSendEvent eventMessage)
        {
            Console.WriteLine("我来了 EmailSendEvent 2");
        }
    }
}
