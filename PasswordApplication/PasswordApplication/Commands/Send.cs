using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Net.Mail;
using PasswordApplication.BL;
using PasswordApplication.Commands.Utilities;
using PasswordApplication.Commands;
using PasswordApplication.Model;
using PasswordApplication.DataAccess;
using System.Threading;

namespace PasswordApplication.Commands
{
    class Send
    {
        public event EventHandler<StartSendingEventArgs> onEmailStartSending;
        public event EventHandler<StartSendingEventArgs> onEmailPostSending;

        static Exception _savedException = null;
        static ConcurrentDictionary<MailMessage, string> faultAddress = new ConcurrentDictionary<MailMessage, string>();

        public void SendMethod(UserRepository repo)
        {
            //здесь обработчик для события вернет список на отправку
            #region Raise Event

            Send send = new Send();
            send.onEmailStartSending += PasswordApplication.Commands.EventDispatcher.send_onEmailStartSending;

            if (send.onEmailStartSending != null)
            {
                send.onEmailStartSending(send, new StartSendingEventArgs(repo));
            }
            #endregion
            List<User> usersToSend = repo.UsersToSendlist;
            Queue<MailMessage> messagesQueue = new Queue<MailMessage>();
            foreach (var user in usersToSend)
            {
                using (var message = EmailFactory.GetMailMessage(user.Email, user))
                {
                    messagesQueue.Enqueue(message);
                }
            }
            Parallel.ForEach(messagesQueue, (message) =>
                {
                    SendMailToUser(message);
                });

            #region Raise PostSending Event

            send.onEmailPostSending += PasswordApplication.Commands.EventDispatcher.send_onEmailStartSending;

            if (send.onEmailStartSending != null)
            {
                send.onEmailStartSending(send, new StartSendingEventArgs(repo));
            }
            #endregion
        }

        #region SendHelper

        private static SmtpClient CreateSmptClientForThread()
        {
            string fromAddress = Properties.Resources.MailAdressFrom;
            string password = "****"; //TODO Переделать в SecureString
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",//TODO вынести в конфигурацию
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Split('@')[0], password)
            };
            return smtp;
        }
        private static void SendMailToUser(MailMessage message)
        {
            try
            {
                var smpt = CreateSmptClientForThread();
                smpt.Send(message);
           
            }
            catch (Exception ex)
            {
                _savedException = ex;
                faultAddress.Add(message,_savedException.Message);
                          
            }
        }

        #endregion
    }
}
