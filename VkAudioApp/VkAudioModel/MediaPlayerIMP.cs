using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkAudioModel
{

    /// <summary>Класс Mодели</summary>
    public class MediaPlayerIMP
    {
        /// <summary>Поле с  VkApi</summary>
        private readonly IVkApi VkApi;
        /// <summary>Поле с ServiceCollection</summary>
        private readonly ServiceCollection ServiceCollection;
        /// <summary>ID приложения - присваивается VK</summary>
        public readonly ulong ApplicationId;

        /// <summary>Конструктор по умолчанию</summary>
        private MediaPlayerIMP()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddAudioBypass();
            VkApi = new VkApi(ServiceCollection);
        }

        /// <summary>Конструктор с передачей ID</summary>
        /// <param name="applicationId"></param>
        public MediaPlayerIMP(ulong applicationId, Func<string> inputSmsCode)
            : this()
        {
            ApplicationId = applicationId;
            InputSmsCode = inputSmsCode;
        }

        /// <summary>Поле с делегатом метода для ввода кода SMS</summary>
        private readonly Func<string> InputSmsCode;

        /// <summary>Метод авторизации. </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>При успешной авторизации возращает <see langword="true"/></returns>
        public Task<bool> AuthorizeTask(string login, string password)
            => Task.Factory.StartNew(() =>
            {
                ApiAuthParams apiAuthParams = new ApiAuthParams
                {
                    ApplicationId = this.ApplicationId,
                    Login = login,
                    Password = password,
                    Settings = Settings.All,
                    TwoFactorAuthorization = InputSmsCode
                };
                VkApi.Authorize(apiAuthParams);

                return VkApi.IsAuthorized;

            });

        /// <summary>Получить песни</summary>
        /// <param name="countTrack">Количество треков песен</param>
        /// <returns>Соллекцию с треками</returns>
        public VkCollection<Audio> GetAudios(int countTrack)
        {
            var audios = VkApi.Audio.Get(new AudioGetParams { Count = countTrack });

            return audios;
        }


        /// <summary>Запуск музыки и управление её  - это методы View
        /// Отсюда их надо буде убрать.</summary>
        void PlaySong()
        {
            // Запустить песню
        }

        void PauseSong()
        {
            // Остановить песню
        }

    }

}
