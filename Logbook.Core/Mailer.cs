using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Core
{
    public class Mailer
    {
        private static SmtpClient _smtpClient;

        private static SmtpClient SmtpClient
        {
            get
            {
                if (_smtpClient == null)
                {
#if DEBUG
                    _smtpClient = new SmtpClient
                    {
                        Host = "localhost",
                        Port = 25
                    };
#endif
                }
                return _smtpClient;
            }
        }

        public Mailer()
        {
        }

        public static bool SendMessage(string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                var msg = new MailMessage();
                msg.To.Add(new MailAddress(toAddress));
                msg.From = new MailAddress(fromAddress);
                msg.Subject = subject;
                msg.Body = body;
                SmtpClient.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
