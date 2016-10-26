using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApplication.BL
{
    class EventLogger
    {
        private static StringBuilder _logString;
        public static StringBuilder LogString
        {
            get
            {
                return _logString;
            }
            set
            {
                if (_logString == null)
                {
                    _logString = new StringBuilder();
                }
            }
        }
        
    }
}
