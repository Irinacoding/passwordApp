using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordApplication.Model;
using PasswordApplication.DataAccess;

namespace PasswordApplication.BL
{
    class LoginGenerator
    {
        public string LoginListGeneration(User user)
        {
            string firstName = user.FirstName;
            string lastName = user.LastName;
            string patronime = user.Patronime;
            int loginLength = firstName.Length + lastName.Length + patronime.Length;

            StringBuilder sbLogin = new StringBuilder(loginLength);
            sbLogin.Append(firstName.Remove(1).ToUpper());
            sbLogin.Append(patronime.Remove(1).ToUpper());
            sbLogin.Append(lastName);
            string newLogin = sbLogin.ToString();
            bool result = LoginGenerator.LoginVerification(newLogin);
            if (!result)
                newLogin = LoginGenerator.MakeLoginUnique(newLogin);
            return newLogin;
        }
        private static bool LoginVerification(string login)
        {
            //TODO вставить логику проверки на уникальность
            throw new NotImplementedException();
        }
        private static string MakeLoginUnique(string newLogin)
        {
            Random rd = new Random(0);
            newLogin = newLogin + rd.Next(0, 999).ToString();//TODO вынести константы в ресурсы
            return newLogin;
        }
    }
}
