using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;
using ui_maui_youtube.IServices;
using ui_maui_youtube.Services;
using ui_maui_youtube.ViewModels;

namespace ui_maui_youtube;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        // Initialise the toolkit
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FiraSans-Light.ttf", "RegularFont");
                fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
            })
             .ConfigureLifecycleEvents(events =>
             {
#if ANDROID
                 events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

                 static void MakeStatusBarTranslucent(Android.App.Activity activity)
                 {
                     activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

                     activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

                     activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
                 }
#endif
             });

        RegisterAppServices(builder.Services);

        return builder.Build();
    }

    private static void RegisterAppServices(IServiceCollection services)
    {
        //Add Platform specific Dependencies
        services.AddSingleton<IConnectivity>(Connectivity.Current);

        //Register API Service
        services.AddSingleton<IApiService, YoutubeService>();

        //Register View Models
        services.AddSingleton<StartPageViewModel>();
    }
}
