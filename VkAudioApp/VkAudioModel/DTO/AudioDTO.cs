using VkNet.Model.Attachments;

namespace VkAudioModel.DTO
{
    /// <summary>DTO тип с данными Audio</summary>
    public class AudioDTO
    {
        /// <summary>Идентификатор вложенеия</summary>
        public long? Id { get; }
        /// <summary>Исполнитель аудиозаписи</summary>
        public string Artist { get; }
        /// <summary>Название композиции</summary>
        public string Title { get; }

        /// <summary>Конструктор с заданием значений</summary>
        /// <param name="audio"></param>
        public AudioDTO(long? id, string artist, string title)
        {
            Id = id;
            Artist = artist;
            Title = title;
        }
        /// <summary>Конструтор по экземпляру Audio</summary>
        /// <param name="audio">Экземпляр с данными</param>
        public AudioDTO(Audio audio)
            : this(audio.Id, audio.Artist, audio.Title)
        { }
    }
}
