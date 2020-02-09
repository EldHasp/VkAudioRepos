namespace VkAudioModel
{
    public class Song
    {
        // Название песни (Автор + название)
        public string SongName { get; set; }

        // Ссылка на плейлист, которому принадлежит песня
        public PlayList PL { get; set; }

        // Прямая ссылка на песню
        public string UrlFromVk { get; set; }
    }

}
