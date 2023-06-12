
using ui_maui_youtube.IServices;
using ui_maui_youtube.ViewModels.Base;

namespace ui_maui_youtube.ViewModels
{
    public partial class StartPageViewModel : AppViewModelBase
    {
        public StartPageViewModel(IApiService appApiService) : base(appApiService)
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

            try
            {
                await Task.Delay(3000);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                SetDataLodingIndicators(false);
            }

        }
    }
}
