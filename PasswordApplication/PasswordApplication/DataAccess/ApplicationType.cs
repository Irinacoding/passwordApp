using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApplication.DataAccess
{
    enum ApplicationType
    {
        IsNewUsers,
        IsAccessLevelChanging,
        IsUserMigration,
        IsUserBlocking,
        IsPasswordChanging
    }
}
