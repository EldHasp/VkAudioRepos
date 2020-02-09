using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VkNet;
using VkNet.Abstractions;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkAudioModel
{
    public class Program
    {
        private static IVkApi _api;

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            MediaPlayerIMP iMP = new MediaPlayerIMP();
            iMP.PL.Add(new PlayList());
            iMP.PL.Last().Name = "favorite";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAudioBypass();

            _api = new VkApi(serviceCollection);

            Console.WriteLine(" > Номер телефона/E-mail:");
            var login = Console.ReadLine();

            Console.WriteLine(" > Пароль:");
            var password = Console.ReadLine();

            _api.Authorize(new ApiAuthParams
            {
                Login = login,
                Password = password,
                TwoFactorAuthorization = () =>
                {
                    Console.WriteLine(" > Код двухфакторной аутентификации:");
                    return Console.ReadLine();
                }
            });

            Console.WriteLine($" > Access Token: {_api.Token}");


            var audios = _api.Audio.Get(new AudioGetParams { Count = 5 });
            foreach (var audio in audios)
            {
                iMP.PL[0].Songs.Add(new Song());
                iMP.PL[0].Songs.Last().SongName = audio.Artist + " - " + audio.Title;
                iMP.PL[0].Songs.Last().UrlFromVk = audio.Url.ToString();
            }

            foreach (var itemSong in iMP.PL[0].Songs)
            {
                Console.WriteLine(itemSong.SongName);
                Console.WriteLine("Ссылка на песню: " + itemSong.UrlFromVk);
            }
            Console.ReadLine();
        }
    }

}
