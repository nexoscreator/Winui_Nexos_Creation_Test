using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI.WebUI;
using Microsoft.Web.WebView2.Core;

namespace Nexos_Creation.Views;

public sealed partial class VideoPlayerPage : Page
{
    public VideoPlayerPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter != null && e.Parameter is string videoUrl)
        {
            // Load video URL into WebView
            webView.Source = (new Uri(videoUrl));
        }
    }

    private async void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        await webView.CoreWebView2.ExecuteScriptAsync("document.querySelector('iframe').contentWindow.postMessage('{\"event\":\"command\",\"func\":\"playVideo\",\"args\":\"\"}','*');");
    }

    private async void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        await webView.CoreWebView2.ExecuteScriptAsync("document.querySelector('iframe').contentWindow.postMessage('{\"event\":\"command\",\"func\":\"pauseVideo\",\"args\":\"\"}','*');");
    }

    private async void ResizeButton_Click(object sender, RoutedEventArgs e)
    {
        await ResizePlayerAsync(640, 480); // Adjust dimensions as needed
    }

    private async void InitializeAsync()
    {
        await webView.EnsureCoreWebView2Async(null);

        // Enable necessary WebView2 settings
        webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true; // Enable context menus
        webView.CoreWebView2.Settings.IsScriptEnabled = true; // Enable scripts
        webView.CoreWebView2.Settings.IsWebMessageEnabled = true; // Enable postMessages

    }
    private async System.Threading.Tasks.Task ResizePlayerAsync(int width, int height)
    {
        await webView.CoreWebView2.ExecuteScriptAsync($"document.querySelector('iframe').setAttribute('width', '{width}px');");
        await webView.CoreWebView2.ExecuteScriptAsync($"document.querySelector('iframe').setAttribute('height', '{height}px');");
    }

}
