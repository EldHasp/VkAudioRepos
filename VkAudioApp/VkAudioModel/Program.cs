using System;

namespace VkAudioModel
{
    public partial class Program
    {
        /// <summary>ApplicationID от VK</summary>
        /// <remarks>ID приложения можно получить по адресу https://vknet.github.io/vk/authorize/ </remarks>
        private static readonly ulong appID = 1234567;

        private static MediaPlayerIMP model;

        private static void Main(string[] args)
        {
            model = new MediaPlayerIMP(appID, InputSmsCode);
            Console.WriteLine(" > Номер телефона/E-mail:");
            var login = Console.ReadLine();

            Console.WriteLine(" > Пароль:");
            var password = Console.ReadLine();

            bool auth = model.AuthorizeTask(login, password).Result;

            if (auth)
                Console.WriteLine("Авторизация успешна");
            else
                Console.WriteLine("Авторизация провалилась");

            var audios = model.GetAudios(5);
            foreach (var audio in audios)
            {
                Console.WriteLine($"{audio.Id} {audio.Artist} {audio.Title}");
            }

            Console.ReadLine();
        }

        private static string InputSmsCode()
        {
            Console.WriteLine("Enter Code:");
            return Console.ReadLine();
        }
    }
}
