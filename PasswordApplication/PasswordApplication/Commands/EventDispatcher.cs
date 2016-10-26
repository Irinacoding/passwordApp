using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordApplication.Model;
using PasswordApplication.BL;
using PasswordApplication.DataAccess;
using PasswordApplication.Commands.Utilities;

namespace PasswordApplication.Commands
{
    class EventDispatcher
    {
        #region Event Handlers
        //TODO добавить логику логирования для других типов заявки
        internal static void upload_on_UserAdded(object sender, UserListAddedEventArgs e)
        {
            if (e.IncomeUserList != null)
            {
                EventLogger.LogString.AppendLine("Upload is successfull:  " +
                                                  e.IncomeUserList.Count + "Access Level will be created: "
                                                  + e.AppFlag.ToString() + DateTime.Now);
            }
        }

        internal static void send_onEmailStartSending(object sender, StartSendingEventArgs e)
        {
            new EventDispatcher().PrepareUsersToSend(e.Repo);
        }

        internal static void send_onEmailPostSending(object sender,)

        public event EventHandler<ErrorMergedEventArgs> ErrorMerged;
        //обработчик ошибок при отправке (неправильный адрес, не )
        void EventDispather_ErrorMerged(object sender, ErrorMergedEventArgs e)
        {
            e.ErrorSource = sender.ToString();
            EventLogger.LogString.AppendLine(e.ErrorMergedMessage + DateTime.Now);
        }

        #endregion

        #region Helpers

        //TODO Migrate in other Place

        private List<User> PrepareUsersToSend(UserRepository repo)
        {
            repo.UsersToSendlist = repo.UsersToSendlist == null ? repo.UsersToSendlist : null;
            if (repo.IncomeUserList != null)
            {
                ApplicationType appType = repo.AppFlag;
                switch (appType)
                {
                    case ApplicationType.IsNewUsers:
                        return repo.UsersToSendlist = EventDispatcher.PrepareNewUsersToSend(repo);

                    case ApplicationType.IsPasswordChanging:
                        return repo.UsersToSendlist = EventDispatcher.PrepareUsersToChangePassword(repo);

                    case ApplicationType.IsUserBlocking:
                        return repo.UsersToSendlist = EventDispatcher.PrepareUsersToBlock(repo);

                    case ApplicationType.IsAccessLevelChanging:
                        return repo.UsersToSendlist = EventDispatcher.PrepareUsersToAccessLevelChanging(repo);

                    case ApplicationType.IsUserMigration:
                        return repo.UsersToSendlist = EventDispatcher.PrepareUsersToMigrate(repo);

                    default:
                        ErrorMerged += EventDispather_ErrorMerged;
                        ErrorMerged(this, new ErrorMergedEventArgs());
                        return repo.UsersToSendlist = null;
                }
            }
            return repo.UsersToSendlist;
        }

        private static List<User> PrepareUsersToMigrate(UserRepository repo)
        {
            throw new NotImplementedException();//TODO реализовать, когда будет готов код работы с бД
        }

        private static List<User> PrepareUsersToAccessLevelChanging(UserRepository repo)
        {
            throw new NotImplementedException();//TODO реализовать, когда будет готов код работы с бД
        }

        private static List<User> PrepareUsersToBlock(UserRepository repo)
        {
            throw new NotImplementedException();//TODO реализовать, когда будет готов код работы с бД
        }

        private static List<User> PrepareUsersToChangePassword(UserRepository repo)
        {
            repo.FailDeliveredList = repo.FailDeliveredList != null ? repo.FailDeliveredList : new List<User>();
            foreach (var user in repo.IncomeUserList)
            {
                PassAppResourcesConverter converter = new PassAppResourcesConverter();
                user.Password = PasswordGenerator.Generate(converter.ResorcesStringToInt32Convert(Properties.Resources.MinPasswordLength),
                                                           converter.ResorcesStringToInt32Convert(Properties.Resources.MaxPasswordLength));
                repo.UsersToSendlist.Add(user);
            }
            return repo.UsersToSendlist;
        }

        private static List<User> PrepareNewUsersToSend(UserRepository repo)
        {
            foreach (var user in repo.IncomeUserList)
            {
               PassAppResourcesConverter converter = new PassAppResourcesConverter();
                user.Password = PasswordGenerator.Generate(converter.ResorcesStringToInt32Convert(Properties.Resources.MinPasswordLength),
                                                           converter.ResorcesStringToInt32Convert(Properties.Resources.MaxPasswordLength));
                user.Login = new LoginGenerator().LoginListGeneration(user);
                repo.UsersToSendlist.Add(user);
            }
            return repo.UsersToSendlist;
        }


        #endregion
    }
}
