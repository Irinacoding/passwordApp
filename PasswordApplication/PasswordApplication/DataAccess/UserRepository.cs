using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PasswordApplication.Model;
using System.Reflection;

namespace PasswordApplication.DataAccess
{
    class UserRepository
    {
        private List<User> _incomeUserList;
        private List<User> _succesDeliveredList;
        private List<User> _failDeliveredList;
        private ApplicationType appFlag;
        private List<User> _usersToSendList;

        #region Properties
        public List<User> IncomeUserList
        {
            get
            {
                return _incomeUserList;
            }
            set
            {
                _incomeUserList = value;
            }
        }
        public List<User> SuccesDeliveredList
        {
            get
            {
                return _succesDeliveredList;
            }
            set
            {
                _succesDeliveredList = value;
            }
        }
        public List<User> FailDeliveredList
        {
            get
            {
                return _failDeliveredList;
            }
            set
            {
                _failDeliveredList = value;
            }
        }
        public List<User> UsersToSendlist { get; set; }
        public string FilePath { get; set; }
        public ApplicationType AppFlag { get; set; }

        #endregion

        #region Constructors

        public UserRepository()
        {
            if (!string.IsNullOrEmpty(GetPath()))
                this.FilePath = GetPath();
        }
        #endregion

        #region Public Interface

        public List<User> GetIncomeUserList()
        {
            JObject myObj = null;
            int flag = 0;
            User User = new Model.User();
            UserPropertiesMapper mapper = new UserPropertiesMapper();
            List<User> IncomeUserList = new List<User>();

            myObj = GetJson(this.FilePath);


            if (mapper != null)
            {
                for (int i = 0; i < mapper.ObjectUserPropertiesInElApp.Length; i++)
                {

                    if (PropertiesComparator(mapper.ObjectUserPropertiesInElApp[i], mapper.UserPropertiesInElApp[i]))
                    {
                        continue;
                    }
                    flag++;
                }
                throw new ArgumentNullException("Не удалось сравнить свойства объекта и репозитория.");
            }
            if (myObj != null && flag == 0)
            {
                int len = mapper.UserPropertiesInElApp.Length;
                //TODO Correct programm flow!!!!!!!
                while (len > 0)
                {
                    int i = 0;
                    User.FirstName = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                    User.LastName = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                    User.Patronime = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                    User.Login = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                    User.Email = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                    User.AccessLevel = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                    User.Password = myObj[mapper.UserPropertiesInElApp[i]].ToString(); i++; len--;
                }
                IncomeUserList.Add(User);
            }
            return IncomeUserList;
        }
        #endregion

        #region Helpers
        internal static string GetPath()
        {
            string path = PasswordApplication.Properties.Resources.FilePath;
            if (VerifyPath(path))
                return path;
            return null;
        }
        private static bool VerifyPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine(PasswordApplication.Properties.Resources.PathUndetermined);
                return false;
            }
            if (!File.Exists(path))
            {
                Console.WriteLine(PasswordApplication.Properties.Resources.UnuploadedFile);
                return false;
            }
            if (!Path.GetExtension(path).Equals(".json"))
            {
                Console.WriteLine(PasswordApplication.Properties.Resources.UnallowedFile);
                return false;
            }
            return true;
        }

        public JObject GetJson(string filePath)
        {
            var serialiser = new JsonSerializer();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    using (JsonTextReader jreader = new JsonTextReader(sr))
                    {
                        string jsonString = serialiser.Deserialize(jreader).ToString();
                        return JObject.Parse(jsonString);
                    }
                }
            }
        }
        private static bool PropertiesComparator(string baseProp, string propToCompare)
        {
            bool result = false;
            if (baseProp.Equals(propToCompare, StringComparison.InvariantCultureIgnoreCase))
            {
                result = true;
                return result;

            }
            return result;
        }
        #endregion

    }
}
