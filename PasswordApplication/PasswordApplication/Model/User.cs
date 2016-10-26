using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApplication.Model
{
    class User
    {
        #region Fields
        private string lastName;
        private string firstName;
        private string patronime;
        private string email;
        private string login;
        private string accessLevel;
        private string password;
        #endregion

        #region Constructor

        public User()
        {

        }

        public User(string firstName, string lastName, string patronime)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.patronime = patronime;
        }
        public User(string firstName, string lastName, string patronime, string email)
            : this(firstName, lastName, patronime)
        {
            this.email = email;
        }

        public User(string firstName, string lastName, string patronime, string email, string login, string accessLevel)
            : this(firstName, lastName, patronime, email)
        {
            this.login = login;
            this.accessLevel = accessLevel;
        }
        public User(string firstName, string lastName, string patronime, string email, string accessLevel)
            : this(firstName, lastName, patronime, email)
        {
            this.accessLevel = accessLevel;
        }
        #endregion

        #region Propreties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronime { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string AccessLevel { get; set; }
        public string Password { get; set; }
        #endregion
    }
}
