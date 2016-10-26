using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using PasswordApplication.Properties;
using PasswordApplication.Model;

namespace PasswordApplication.Commands.Utilities
{
    class EmailFactory
    {
        public static MailMessage GetMailMessage(string toAdress, User user)
        {
            var fromAddress = Properties.Resources.MailAdressFrom;
            var receiver = toAdress;
            var text = new MailMessageTemplate();
            MailMessage message = new MailMessage(fromAddress, receiver)
            {
                Subject = Properties.Resources.MailSubject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                Body = ResourceHelper.MailTextGenerator(text, user),
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = false
            };
            return message;
        }
    }
}
