using Ninject;
using System.Windows;
using WPFMtgApp.BusinessLogic;

namespace WPFMtgApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = App.ServiceProvider.Get<MainWindowViewModel>();
            InitializeComponent();
        }
    }
}