using System.Collections.Generic;

namespace VkAudioModel
{
    public class PlayList
    {
        // Название плейлиста
        public string Name { get; set; }

        // Список песен в плейлисте
        public List<Song> Songs = new List<Song>();
    }

}
