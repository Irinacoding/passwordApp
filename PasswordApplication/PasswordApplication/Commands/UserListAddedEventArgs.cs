using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordApplication.Model;
using PasswordApplication.DataAccess;

namespace PasswordApplication.Commands
{
    class UserListAddedEventArgs
    {
        public UserListAddedEventArgs(UserRepository repo)
        {
            this.IncomeUserList = repo.IncomeUserList;
            this.AppFlag = repo.AppFlag;
        }

        public List<User> IncomeUserList { get; private set; }
        public ApplicationType AppFlag { get; set; }
    }
}
