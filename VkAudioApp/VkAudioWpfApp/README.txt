��� ��������� ApplicationID � ������ ���� �������� ���� App.secret.xaml.cs
��� ����������
/// **************************************
using System.Windows;

namespace VkAudioWpfApp
{
    /// <summary>���������� � ���������� ������ App � ����� ��� ID ����������</summary>
    public partial class App : Application
    {
        /// <summary>�������� ID ����������</summary>
        /// <remarks>ID ���������� ����� �������� �� ������ https://vknet.github.io/vk/authorize/ </remarks>
        private static readonly ulong appID = 1234567;
    }
}
/// **************************************
����� ���� *.secret.* �������� � ������ ������������� � � ����������� �� ��������.