using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using PasswordApplication.DataAccess;
using PasswordApplication.Model;

namespace PasswordApplication.Commands.Utilities
{
    class MailMessageTemplate
    {
        public MailMessageTemplate()
        {
            string template = ResourceHelper.GetText("MessageTemplate.txt");
            MessageText = template;
        }
        public string MessageText { get; private set; }
    }
    static class ResourceHelper
    {
        private static Stream GetStream(string resourceName)
        {
            return typeof(ResourceHelper).Assembly.GetManifestResourceStream(resourceName);
        }

        internal static string GetText(string resourceName)
        {
            using (StreamReader reader = new StreamReader(GetStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
        }
        internal static string MailTextGenerator(MailMessageTemplate template, User user)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Уважаемый, {0} {1} !", user.FirstName, user.Patronime);
            sb.AppendLine(template.MessageText);
            sb.AppendLine();
            sb.AppendFormat("Логин: {0} .", user.Login);
            sb.AppendLine();
            sb.AppendFormat("Логин: {0} .", user.Password);
            return sb.ToString();
        }
    }
}
