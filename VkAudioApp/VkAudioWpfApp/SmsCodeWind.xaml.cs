using System;
using System.Windows;

namespace VkAudioWpfApp
{
    /// <summary>
    /// Логика взаимодействия для SmsCodeWind.xaml
    /// </summary>
    public partial class SmsCodeWind : Window
    {
        public SmsCodeWind()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Code = "";
        }

        public string Code { get; private set; }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Code = tBoxCode.Text;
            DialogResult = true;
            Close();
        }
    }
}
