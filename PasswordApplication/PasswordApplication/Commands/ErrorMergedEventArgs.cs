using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApplication.Commands
{
    class ErrorMergedEventArgs
    {
        public string ErrorMergedMessage { get; private set; }
        public object ErrorSource { get; set; }
        public ErrorMergedEventArgs()
        {
            ErrorMergedMessage = "Произошла ошибка.Приложение не может продолжить свою работу." +
                                 "Все операции будут отменены.Приложение потребует перезапуска." +
                                 "Источник ошибки:" + ErrorSource.ToString();
        }
    }
}
