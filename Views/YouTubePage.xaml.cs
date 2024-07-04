using System;
using System.Collections.Generic;
using ABI.Microsoft.UI.Xaml;
using AngleSharp.Dom;
using Microsoft.UI.Xaml.Media.Imaging;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using Nexos_Creation.Models;

namespace Nexos_Creation.Views;

public sealed partial class YouTubePage : Page
{
    private readonly YoutubeClient _YtClient;

    public YouTubePage()
    {
        this.InitializeComponent();
        _YtClient = new YoutubeClient();
        LoadChannelAsync();
        LoadVideosAsync();
        LoadPlaylistAsync();
    }

    private async void LoadChannelAsync()
    {
        try
        {
            // Show loading indicator
            ChannelTitleTextBlock.Text = "Loading...";
            ChannelThumbline.Source = null;

            var NexosUrl = "https://youtube.com/channel/UClFiZsXegdz1iNwnZPspFRA";
            var channel = await _YtClient.Channels.GetAsync(NexosUrl);

            var thumbnailUrl = channel.Thumbnails.GetWithHighestResolution().Url;
            var title = channel.Title;
            var subscribeUrl = channel.Url;

            // Update UI with channel title
            ChannelTitleTextBlock.Text = title;
            ChannelSubscibeBtn.NavigateUri = new Uri(subscribeUrl);
            ChannelThumbline.Source = new BitmapImage(new Uri(thumbnailUrl));
        }
        catch (Exception ex)
        {
            // Handle exception (e.g., network error, API limit reached)
            ChannelTitleTextBlock.Text = "Error loading channel title";
            Console.WriteLine($"Error loading channel title: {ex.Message}");
        }
    }

    private async void LoadVideosAsync()
    {
        try
        {
            var channelUrl = "https://youtube.com/channel/UClFiZsXegdz1iNwnZPspFRA";
            var videos = await _YtClient.Channels.GetUploadsAsync(channelUrl);

            var videoItems = new List<VideoItem>();

            foreach (var video in videos)
            {
                var title = video.Title;
                var author = video.Author.ChannelTitle;
                var videoId = video.Id;
                var Url = $"https://www.youtube.com/embed/{videoId}?controls=0&disablekb=0&modestbranding=0&color=white&hd=1";
                var duration = video.Duration.HasValue ? video.Duration.Value.ToString() : string.Empty;
                var thumbnailUrl = video.Thumbnails?.TryGetWithHighestResolution();

                videoItems.Add(new VideoItem
                {
                    Title = title,
                    ThumbnailUrl = thumbnailUrl?.Url ?? string.Empty,
                    Author = author,
                    Duration = duration,
                    VideoUrl = Url
                });
            }

            videoGridView.ItemsSource = videoItems;

            // Handle click event on GridView items
            videoGridView.ItemClick += VideoGridView_ItemClick;
        }
        catch (Exception ex)
        {
            // Handle exception (e.g., network error, API limit reached)
            Console.WriteLine($"Error loading videos: {ex.Message}");
        }
    }

    private async void LoadPlaylistAsync()
    {
        try
        {
            var playlistUrl = "https://youtube.com/playlist?list=PLsi_twzeQSKTLIkTWFfthCaRo9_JgIiBr&si=KgTq7xy3SQfSfp7-";
            var videos = await _YtClient.Playlists.GetVideosAsync(playlistUrl);
            var playlist = await _YtClient.Playlists.GetAsync(playlistUrl);

            if (playlist == null)
            {
                throw new ArgumentException("Invalid playlist URL");
            }
            else
            {
                var VideoTitle = playlist.Title;
                var infoauthor = playlist.Author?.ChannelTitle ?? "Unknown";  // Safe null handling
                PlayListVideoTitle.Text = VideoTitle;
            }

            var VideoInfo = new List<VideoItem>();

            foreach (var video in videos)
            {
                var title = video.Title;
                var thumbnailUrl = video.Thumbnails?.GetWithHighestResolution(); // Safe null handling

                VideoInfo.Add(new VideoItem
                {
                    Title = title,
                    ThumbnailUrl = thumbnailUrl?.Url ?? string.Empty  // Safe null handling
                });
            }

            PlayListView.ItemsSource = VideoInfo;
        }
        catch (Exception ex)
        {
            // Handle exception (e.g., network error, API limit reached)
            Console.WriteLine($"Error loading videos: {ex.Message}");
        }
    }

    private void VideoGridView_ItemClick(object sender, Microsoft.UI.Xaml.Controls.ItemClickEventArgs e)
    {
        // Navigate to VideoPlayerPage with video URL as parameter
        if (e.ClickedItem is VideoItem clickedVideo)
        {
            var videoUrl = clickedVideo.VideoUrl; // Replace with actual property that holds the video URL
            Frame.Navigate(typeof(VideoPlayerPage), videoUrl);
        }
    }
}
