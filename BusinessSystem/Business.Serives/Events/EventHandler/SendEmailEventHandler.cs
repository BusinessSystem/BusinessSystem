using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Serives.Events.Event;
using Business.Utils;

namespace Business.Serives.Events
{
    public class SendEmailEventHandler : IEventHandler<EmailSendEvent>
    {

        public void HandleEvent(EmailSendEvent eventMessage)
        {
            string hostIp = ConfigHelper.GetAppSetting("EmailHost");
            string port = ConfigHelper.GetAppSetting("EmailHosPort");
            string userName = ConfigHelper.GetAppSetting("UserName");
            string password = ConfigHelper.GetAppSetting("UserPassword");
            List<string> address = new List<string>();
            address.Add(eventMessage.ToEmail);
            MailHelper.SendMail(eventMessage.Subject, eventMessage.Content, userName, address, hostIp, "",
                userName, password, true);
        }
    }
}
