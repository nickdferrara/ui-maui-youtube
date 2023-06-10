
using ui_maui_youtube.Views;

namespace ui_maui_youtube;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//Enable Version Tracking
		VersionTracking.Track();

        MainPage = new NavigationPage(new StartPage());
    }
}
