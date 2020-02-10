using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VkAudioWpfApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>Значение ID приложения</summary>
        /// <remarks>ID приложения можно получить по адресу https://vknet.github.io/vk/authorize/ </remarks>
        private static readonly ulong appID = 1234567;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindow() { DataContext = new VkAudioVM(appID) }
            .Show();
        }
    }
}
