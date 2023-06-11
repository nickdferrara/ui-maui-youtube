using Maui.Apps.Framework.Services;
using System.Net;
using ui_maui_youtube.IServices;
using ui_maui_youtube.Models;

namespace ui_maui_youtube.Services
{
    public class YoutubeService : RestServiceBase, IApiService
    {
        public YoutubeService(IConnectivity connectivity) : base(connectivity)
        {
            SetBaseURL(Constants.ApiServiceURL);
        }

        public async Task<VideoSearchResult> SearchVideos(string searchQuery, string nextPageToken = "")
        {
            var resourceUri = $"search?part=snippet&maxResults=10&type=video&key={Constants.ApiKey}&q={WebUtility.UrlEncode(searchQuery)}"
                +
                (!string.IsNullOrEmpty(nextPageToken) ? $"&pageToken={nextPageToken}" : "");

            var result = await GetAsync<VideoSearchResult>(resourceUri);

            return result;
        }

        public async Task<ChannelSearchResult> GetChannels(string channelIDs)
        {
            var resourceUri = $"channels?part=snippet,statistics&maxResults=10&key={Constants.ApiKey}&id={channelIDs}";

            var result = await GetAsync<ChannelSearchResult>(resourceUri);

            return result;
        }

        public async Task<YoutubeVideoDetail> GetVideoDetails(string videoID)
        {
            var resourceUri = $"videos?part=contentDetails,id,snippet,statistics&key={Constants.ApiKey}&id={videoID}";

            var result = await GetAsync<VideoDetailsResult>(resourceUri);

            return result.Items.First();
        }

        public async Task<CommentsSearchResult> GetComments(string videoID)
        {
            var resourceUri = $"commentThreads?part=snippet&maxResults=100&key={Constants.ApiKey}&videoId={videoID}";

            var result = await GetAsync<CommentsSearchResult>(resourceUri);

            return result;
        }

    }
}
