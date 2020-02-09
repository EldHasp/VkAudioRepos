using System;

namespace VkAudioModel
{
    public partial class Program
    {

        private static MediaPlayerIMP IMP;

        private static void Main(string[] args)
        {
            IMP = new MediaPlayerIMP(appID);
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
            foreach (var audio in audios)
            {
                Console.WriteLine($"{audio.Artist} {audio.Album} {audio.Title}");
            }

            Console.ReadLine();
        }
    }
}
