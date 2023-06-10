using CommunityToolkit.Mvvm.Input;
using Maui.Apps.Framework.MVVM;
using ui_maui_youtube.IServices;

namespace ui_maui_youtube.ViewModels.Base
{
    public partial class AppViewModelBase : ViewModelBase
    {
        public INavigation NavigationService { get; set; }
        public Page PageService { get; set; }

        protected IApiService _appApiService { get; set; }

        public AppViewModelBase(IApiService appApiService) : base()
        {
            _appApiService = appApiService;
        }

        [RelayCommand]
        private async Task NavigateBack() =>
            await NavigationService.PopAsync();

        [RelayCommand]
        private async Task CloseModal() =>
            await NavigationService.PopModalAsync();

    }
}
