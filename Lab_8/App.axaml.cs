using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Lab_8.ViewModels;
using Lab_8.Views;

namespace Lab_8
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                (desktop.MainWindow.DataContext as MainWindowViewModel).show = desktop.MainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
