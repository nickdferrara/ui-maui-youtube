
using ui_maui_youtube.ViewModels;
using ui_maui_youtube.Views.Base;

namespace ui_maui_youtube.Views;

public partial class StartPage : ViewBase<StartPageViewModel>
{
	public StartPage()
	{
		InitializeComponent();
	}

    void txtSearchQuery_Completed(System.Object sender, System.EventArgs e)
    {
		ViewModel.SearchVideosCommand.Execute(txtSearchQuery.Text);
    }
}