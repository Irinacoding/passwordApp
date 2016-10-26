using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PasswordApplication.Model;
using PasswordApplication.DataAccess;

namespace PasswordApplication.Commands
{
    class Upload
    {
        public event EventHandler<UserListAddedEventArgs> on_UserAdded;

        public static UserRepository UploadMethod()
        {
            UserRepository repo = new UserRepository();
            JObject appObject = !string.IsNullOrEmpty(repo.FilePath) ? repo.GetJson(repo.FilePath) : null;
            string appValue = appObject["ApplicationType"].ToString();
            if (!string.IsNullOrEmpty(appValue)) //определение типа заявки
            {
                string[] appType = new string[]
                {
                    Properties.Resources.NewUserCreation,
                    Properties.Resources.UpdateAccessLevel ,
                    Properties.Resources.UserBlocking,
                    Properties.Resources.UserMigration,
                    Properties.Resources.PasswordChanging
                };
                if (appType.Length > 0)
                {
                    if (appValue.Equals(appType[0], StringComparison.InvariantCultureIgnoreCase))
                    {
                        repo.AppFlag = ApplicationType.IsNewUsers;
                    }
                    if (appValue.Equals(appType[1], StringComparison.InvariantCultureIgnoreCase))
                    {
                        repo.AppFlag = ApplicationType.IsAccessLevelChanging;
                    }
                    if (appValue.Equals(appType[2], StringComparison.InvariantCultureIgnoreCase))
                    {
                        repo.AppFlag = ApplicationType.IsUserBlocking;
                    }
                    if (appValue.Equals(appType[3], StringComparison.InvariantCultureIgnoreCase))
                    {
                        repo.AppFlag = ApplicationType.IsUserMigration;
                    }
                    if (appValue.Equals(appType[4], StringComparison.InvariantCultureIgnoreCase))
                    {
                        repo.AppFlag = ApplicationType.IsPasswordChanging;
                    }

                    repo.IncomeUserList = repo.GetIncomeUserList();
                }

                #region Raise Event
                Upload upload = new Upload();
                upload.on_UserAdded += EventDispatcher.upload_on_UserAdded;

                if (upload.on_UserAdded != null)
                {
                    upload.on_UserAdded(upload, new UserListAddedEventArgs(repo));
                }
                #endregion
            }
            return repo;
        }
    }
}
