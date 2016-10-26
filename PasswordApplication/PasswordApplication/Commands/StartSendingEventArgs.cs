using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordApplication.DataAccess;

namespace PasswordApplication.Commands
{
    class StartSendingEventArgs
    {
        public StartSendingEventArgs(UserRepository repo)
        {
            Repo = repo;
        }
        public UserRepository Repo { get; private set; }
    }
}
