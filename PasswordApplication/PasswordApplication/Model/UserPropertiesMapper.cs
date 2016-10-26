using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PasswordApplication.Model
{
    class UserPropertiesMapper
    {
        private string[] _userPropertiesInElApp;
        private string[] _objectUserPropertiesInElApp;

        public UserPropertiesMapper()
        {

        }

        #region Properties
        public string[] UserPropertiesInElApp
        {
            get
            {
                return _userPropertiesInElApp;
            }
            //TODO эту гадость срочно убрать
            private set
            {
                _userPropertiesInElApp = new string[]
                {
                    "FirstName",
                    "LastName",
                    "Patronime",
                    "Login",
                    "E-mail",
                    "AccesLevel",
                    "Password"
                };
            }
        }

        public string[] ObjectUserPropertiesInElApp
        {
            get
            {
                return _objectUserPropertiesInElApp;
            }
            private set
            {
                Type t = typeof(User);
                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                _objectUserPropertiesInElApp = new string[propInfos.Length];
                for (int i = 0; i < propInfos.Length; i++)
                {
                    _objectUserPropertiesInElApp[i] = propInfos[i].Name;
                }
            }
        }

        #endregion
    }
}
