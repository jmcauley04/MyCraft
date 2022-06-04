using MyGameEngine.Core;
using System.Windows;

namespace MyCraft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceProvider.GameManager.GetPlayer().TakeDamage(10);
        }
    }
}
