using System;
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
            new MainWindow() { DataContext = new VkAudioVM(appID, InputString) }
            .Show();
        }

        /// <summary>Метод получения данных вызываемый из любого потока</summary>
        /// <returns>Возвращает string с введёнными даными</returns>
        private string InputString()
            => Dispatcher.Invoke(InputStringShowDialog);

        /// <summary>Вызывает диалоговое окно для ввода данных. 
        /// Метод может вызываться только из основного UI потока</summary>
        /// <returns>Возвращает string с введёнными даными</returns>
        private string InputStringShowDialog()
        {
            SecondAuthorizationCodeWind smsCodeWind = new SecondAuthorizationCodeWind();
            smsCodeWind.ShowDialog();
            if (smsCodeWind.DialogResult == true)
                return smsCodeWind.Code;
            return "";
        }
    }
}
