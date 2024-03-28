using Ninject;
using System.Windows;
using WPFMtgApp.Modules;

namespace WPFMtgApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IKernel? ServiceProvider {get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceProvider = new StandardKernel(new MainModule());
        }
    }

}
