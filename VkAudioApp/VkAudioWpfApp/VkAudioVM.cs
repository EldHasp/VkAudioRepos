using System;
using System.Collections.ObjectModel;
using Common;
using VkAudioModel;
using VkAudioModel.DTO;

namespace VkAudioWpfApp
{

    /// <summary>ViewModel</summary>
    public partial  class VkAudioVM : OnPropertyChangedClass
    {
        /// <summary>Поле для Model</summary>
        private readonly MediaPlayerIMP model;

        private static string loginDefault  = "Логин";
        private static string passwordDefault = "Пароль";

        #region Поля для хранения значений свойств
        private string _login = loginDefault;
        private string _password = passwordDefault;
        private bool _isAuthorizeCompleted;
        private bool _isAuthorizeRun;
        #endregion

        /// <summary>Логин</summary>
        public string Login { get => _login; set => SetProperty(ref _login, value); }
        /// <summary>Пароль</summary>
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        /// <summary>Прошла ли авторизация</summary>
        public bool IsAuthorizeCompleted { get => _isAuthorizeCompleted; private set => SetProperty(ref _isAuthorizeCompleted, value); }

        /// <summary>Выполняется авторизация</summary>
        public bool IsAuthorizeRun { get => _isAuthorizeRun; private set => SetProperty(ref _isAuthorizeRun, value); }

        /// <summary>Коллекция треков</summary>
        public ObservableCollection<AudioDTO> Audios { get; }
            = new ObservableCollection<AudioDTO>();

        /// <summary>Команда авторизации</summary>
        public RelayCommand AuthorizeCommand { get; }
        /// <summary>Команда загрузки песен</summary>
        public RelayCommand GetAudiosCommand { get; }

        /// <summary>Исполняющий метод команды</summary>
        /// <param name="parameter">Параметр команды</param>
        private async void AuthorizeMethodAsync(object parameter)
        {
            IsAuthorizeRun = true;
            IsAuthorizeCompleted = await model.AuthorizeTask(Login, Password);
            IsAuthorizeRun = false;
            AuthorizeCommand.Invalidate();
        }

        /// <summary>Метод состояния команды авторизации</summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns><see langword="true"/> если команда может быть выполнена</returns>
        /// <remarks>Команда может быть выполнена, если авторизации ещё не было
        /// и свойства Login и Password не пустые</remarks>
        private bool AuthorizeCanMethod(object parameter)
            => !IsAuthorizeCompleted && !IsAuthorizeRun && !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);

        /// <summary>Исполняющий метод команды авторизации загрузки песен</summary>
        /// <param name="parameter">Параметр команды</param>
        private void GetAudiosMethod(object parameter)
        {
            if (parameter is int count || (parameter is string str && int.TryParse(str, out count)))
            {
                Audios.Clear();
                foreach (AudioDTO audio in model.GetAudios(count))
                    Audios.Add(audio);
            }
        }

        /// <summary>Метод состояния команды загрузки песен</summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>see langword="true"/> если команда может быть выполнена</returns>
        /// <remarks>Команда может быть выполнена, если авторизации выполнена
        /// и параметр можно преобразовать в целое число</remarks>
        private bool GetAudiosCanMethod(object parameter)
            => IsAuthorizeCompleted && (parameter is int || (parameter is string str && int.TryParse(str, out int _)));

        /// <summary>Конструктор по умолчанию</summary>
        public VkAudioVM()
        {
            /// Инициализация команд методами
            AuthorizeCommand = new RelayCommand(AuthorizeMethodAsync, AuthorizeCanMethod);
            GetAudiosCommand = new RelayCommand(GetAudiosMethod, GetAudiosCanMethod);
        }
        /// <summary>Конструктор с передачей ID</summary>
        /// <param name="applicationID">ID приложения полученный от VK</param>
        /// <param name="inputSmsCode">Делегат для получения строки с кодом входа</param>
        public VkAudioVM(ulong applicationID, Func<string> inputSmsCode)
            : this()
            => model = new MediaPlayerIMP(applicationID, inputSmsCode);
    }
}
