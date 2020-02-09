Для получения ApplicationID в проект надо добавить файл ProgramAppID.secret.cs
Его содержимое
/// **************************************
namespace VkAudioModel
{
    /// appID содержин номер ID полученный на https://vknet.github.io/vk/
    public partial class Program { private static readonly ulong appID = 1234567; }
}
/// **************************************
Файлы типа *.secret.* включены в список игнорирования и в репозиторий не попадают.