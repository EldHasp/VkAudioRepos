Для получения ApplicationID в проект надо добавить файл App.secret.xaml.cs
Его содержимое
/// **************************************
using System.Windows;

namespace VkAudioWpfApp
{
    /// <summary>Добавление к частичному классу App с полем для ID приложения</summary>
    public partial class App : Application
    {
        /// <summary>Значение ID приложения</summary>
        /// <remarks>ID приложения можно получить по адресу https://vknet.github.io/vk/authorize/ </remarks>
        private static readonly ulong appID = 1234567;
    }
}
/// **************************************
Файлы типа *.secret.* включены в список игнорирования и в репозиторий не попадают.