using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApplication.Commands.Utilities
{
    class PassAppResourcesConverter
    {
        internal int ResorcesStringToInt32Convert(string str)
        {
            int returnedValue;
            if (str != null)
            {
                try
                {
                    returnedValue = Convert.ToInt32(str);
                }
                catch (ArgumentException)
                {
                    //TODO Вставить обработчик ошибок из EventDispatcher
                    throw new ArgumentException("Преобразование значения строки файла ресурсов закончилось с ошибкой.");
                }
                catch (Exception)
                {
                    throw new Exception("Преобразование значения строки файла ресурсов закончилось с ошибкой.");
                }
                return returnedValue;
            }
            throw new ArgumentNullException("Заполните соответствующие строки длины пароля в файле ресурсов.");
        }
    }
}
