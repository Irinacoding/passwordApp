using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using PasswordApplication.DataAccess;
using PasswordApplication.Properties;

namespace PasswordApplication
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex mutex;
        private void App_Startup(object sender, StartupEventArgs e)
        {
            bool createdNew;
            string mutName = "Applic";
            mutex = new System.Threading.Mutex(true, mutName, out createdNew);
            if(!createdNew)
            {
                this.Shutdown();
            }
        }

        private void OnLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {       
            string path = PasswordApplication.Properties.Resources.MinPasswordLength;
             if(string.IsNullOrEmpty(path))
                 MessageBox.Show("Не указан путь к файлу обработки.");
             if (!File.Exists(path))
                 MessageBox.Show("Файл обработки не загружен.");
             if (!Path.GetExtension(path).Equals(".json"))
                 MessageBox.Show("Формат файла не соотвествует требованию.");
        }

    }
}
