
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.Apps.Framework.Exceptions;
using Maui.Apps.Framework.Extensions;
using System.Collections.ObjectModel;
using ui_maui_youtube.IServices;
using ui_maui_youtube.Models;
using ui_maui_youtube.ViewModels.Base;

namespace ui_maui_youtube.ViewModels
{
    public partial class StartPageViewModel : AppViewModelBase
    {
        private string nextToken = string.Empty;
        private string searchTerm = "iPhone 14";

        [ObservableProperty]
        private ObservableCollection<YoutubeVideo> youtubeVideos;

        [ObservableProperty]
        private bool isLoadingMore;

        public StartPageViewModel(IApiService appApiService) 
            : base(appApiService)
        {
            this.Title = "YouTube";
        }

        public override async void OnNavigatedTo(object parameters)
        {
            await Search();
        }

        private async Task Search()
        {
            SetDataLodingIndicators(true);
            LoadingText = "Loading Videos";
            YoutubeVideos = new();

            try
            {
                await GetYouTubeVideos();
                this.DataLoaded = true;

            }
            catch (InternetConnectionException ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = "Slow or no internet connection." + 
                    Environment.NewLine + 
                    "Please check you internet connection and try again.";
                ErrorImage = "nointernet.png";

            }
            catch (Exception ex)
            {
                this.IsErrorState = true;
                this.ErrorMessage = $"Unable to load videos." +
                    Environment.NewLine +
                    "Please try again.";
                ErrorImage = "error.png";

            }
            finally
            {
                SetDataLodingIndicators(false);
            }

        }

        private async Task GetYouTubeVideos()
        {
            //Search the videos
            var videoSearchResult = await _appApiService.SearchVideos(searchTerm, nextToken);

            nextToken = videoSearchResult.NextPageToken;

            //Get Channel URLs
            var channelIDs = string.Join(",",
                videoSearchResult.Items.Select(video => video.Snippet.ChannelId).Distinct());

            var channelSearchResult = await _appApiService.GetChannels(channelIDs);

            //Set Channel URL in the Video
            videoSearchResult.Items.ForEach(video =>
                video.Snippet.ChannelImageURL = channelSearchResult.Items.Where(channel =>
                    channel.Id == video.Snippet.ChannelId).First().Snippet.Thumbnails.High.Url);

            //Add the Videos for Display
            YoutubeVideos.AddRange(videoSearchResult.Items);
        }

        [RelayCommand]
        private async Task SearchVideos(string searchQuery)
        {
            nextToken = string.Empty;
            searchTerm = searchQuery.Trim();

            await Search();
        }
    }
}
