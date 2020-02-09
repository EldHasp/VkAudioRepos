using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
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

        /// <summary>Конструктор по умолчанию</summary>
        public MediaPlayerIMP()
        {

            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddAudioBypass();
            VkApi = new VkApi(ServiceCollection);
        }

        /// <summary>Метод авторизации. </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>При успешной авторизации возращает <see langword="true"/></returns>
        public bool Authorize(string login, string password)
        {
            ApiAuthParams apiAuthParams = new ApiAuthParams
            {
                Login = login,
                Password = password,
                TwoFactorAuthorization = () =>
                {
                    /// Тестовый код
                    Console.WriteLine(" > Код двухфакторной аутентификации:");
                    return Console.ReadLine();
                }
            };
            VkApi.Authorize(apiAuthParams);

            return VkApi.IsAuthorized;

        }

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
