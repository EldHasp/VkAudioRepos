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

        static MediaPlayerIMP IMP = new MediaPlayerIMP();

        private static void Main(string[] args)
        {

            Console.WriteLine(" > Номер телефона/E-mail:");
            var login = Console.ReadLine();

            Console.WriteLine(" > Пароль:");
            var password = Console.ReadLine();

            bool auth = IMP.Authorize(login, password);

            if (auth)
                Console.WriteLine("Авторизация успешна");
            else
            Console.WriteLine("Авторизация провалилась");

            var audios = IMP.GetAudios(5);
            foreach(var audio in audios)
            {
                Console.WriteLine($"{audio.Artist} {audio.Album} {audio.Title}");
            }

            Console.ReadLine();
        }
    }

}
