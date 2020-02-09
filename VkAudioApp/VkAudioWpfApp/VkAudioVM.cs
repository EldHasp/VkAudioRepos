using System.Collections.ObjectModel;
using Common;
using VkAudioModel;
using VkNet.Model.Attachments;

namespace VkAudioWpfApp
{

    /// <summary>ViewModel</summary>
    public class VkAudioVM : OnPropertyChangedClass
    {
        /// <summary>Поле для Model</summary>
        private readonly MediaPlayerIMP model = new MediaPlayerIMP(0);

        #region Поля для хранения значений свойств
        private string _login = "Логин";
        private string _password =  "Пароль";
        private bool _isAuthorize;
        #endregion

        /// <summary>Логин</summary>
        public string Login { get => _login; set => SetProperty(ref _login, value); }
        /// <summary>Пароль</summary>
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        /// <summary>Прошла ли авторизация</summary>
        public bool IsAuthorize { get => _isAuthorize; private set => SetProperty(ref _isAuthorize, value); }

        /// <summary>Коллекция треков</summary>
        public ObservableCollection<Audio> Audios { get; }
            = new ObservableCollection<Audio>();

        /// <summary>Команда авторизации</summary>
        public RelayCommand AuthorizeCommand { get; }
        /// <summary>Команда загрузки песен</summary>
        public RelayCommand GetAudiosCommand { get; }

        /// <summary>Исполняющий метод комнды</summary>
        /// <param name="parameter">Параметр команды</param>
        private void AuthorizeMethod(object parameter)
            => IsAuthorize = model.Authorize(Login, Password);

        /// <summary>Метод состояния команды авторизации</summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns><see langword="true"/> если команда может быть выполнена</returns>
        /// <remarks>Команда может быть выполнена, если авторизации ещё не было
        /// и свойства Login и Password не пустые</remarks>
        private bool AuthorizeCanMethod(object parameter)
            => !IsAuthorize && !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);

        /// <summary>Исполняющий метод комнды авторизации загрузки песен</summary>
        /// <param name="parameter">Параметр команды</param>
        private void GetAudiosMethod(object parameter)
        {
            if (parameter is int count || (parameter is string str && int.TryParse(str, out count)))
            {
                Audios.Clear();
                foreach (Audio audio in model.GetAudios(count))
                    Audios.Add(audio);
            }
        }

        /// <summary>Метод состояния команды загрузки песен</summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>see langword="true"/> если команда может быть выполнена</returns>
        /// <remarks>Команда может быть выполнена, если авторизации выполнена
        /// и параметр можно преобразовать в целое число</remarks>
        private bool GetAudiosCanMethod(object parameter)
            => IsAuthorize && (parameter is int || (parameter is string str && int.TryParse(str, out int _)));

        /// <summary>Конструктор по умолчанию</summary>
        public VkAudioVM()
        {
            /// Инициализация команд методами
            AuthorizeCommand = new RelayCommand(AuthorizeMethod, AuthorizeCanMethod);
            GetAudiosCommand = new RelayCommand(GetAudiosMethod, GetAudiosCanMethod);
        }
        /// <summary>Конструктор с передачей ID</summary>
        public VkAudioVM(ulong applicationID)
            : this()
            => model = new MediaPlayerIMP(applicationID);
    }
}
