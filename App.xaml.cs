﻿using Nexos_Creation.ViewModels.Website;
using Nexos_Creation.Views.Website;

namespace Nexos_Creation;

public partial class App : Application
{
    public static Window CurrentWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;
    public string AppVersion { get; set; } = AssemblyInfoHelper.GetAssemblyVersion();
    public string AppName { get; set; } = "Nexos_Creation";

    public static T GetService<T>() where T : class
    {
        if ((App.Current as App)!.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IJsonNavigationViewService>(factory =>
        {
            var json = new JsonNavigationViewService();
            json.ConfigDefaultPage(typeof(HomeLandingPage));
            json.ConfigSettingsPage(typeof(SettingsPage));            
            return json;
        });

        // Views and ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<GeneralSettingViewModel>();
        services.AddTransient<ThemeSettingViewModel>();
        services.AddTransient<AppUpdateSettingViewModel>();
        services.AddTransient<AboutUsSettingViewModel>();
        services.AddTransient<HomeLandingViewModel>();
        services.AddTransient<DownloadViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<BreadCrumbBarViewModel>();
        services.AddTransient<DownloadViewModel>();
        services.AddTransient<DownloadPage>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<SettingsPage>();
        services.AddTransient<Nexos_CrafterViewModel>();
        services.AddTransient<Nexos_CrafterPage>();
        services.AddTransient<Nexos_CreationViewModel>();
        services.AddTransient<Nexos_CreationPage>();
        services.AddTransient<Nexos_CreatorViewModel>();
        services.AddTransient<Nexos_CreatorPage>();
        services.AddTransient<YouTubeViewModel>();
        services.AddTransient<YouTubePage>();
        services.AddTransient<ToolsPage>();
        services.AddTransient<ToolsViewModel>();

        return services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        CurrentWindow = new Window();

        CurrentWindow.AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        CurrentWindow.AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;

        if (CurrentWindow.Content is not Frame rootFrame)
        {
            CurrentWindow.Content = rootFrame = new Frame();
        }

        rootFrame.Navigate(typeof(MainPage));

        CurrentWindow.Title = CurrentWindow.AppWindow.Title = $"{AppName} v{AppVersion}";
        CurrentWindow.AppWindow.SetIcon("Assets/icon.ico");

        CurrentWindow.Activate();
    }
}

