using CommunityToolkit.Mvvm.DependencyInjection;
using DontMissVulcan.ViewModels.Recruitment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace DontMissVulcan
{
    public partial class App : Application
    {
        private Window? _window;

        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<RecruitmentViewModel>()
                .BuildServiceProvider();
            Ioc.Default.ConfigureServices(_serviceProvider);

            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Closed += WindowClosed;
            _window.Activate();
        }

        private void WindowClosed(object? sender, WindowEventArgs e)
        {
            _serviceProvider.Dispose();
        }
    }
}
