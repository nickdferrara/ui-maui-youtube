namespace ui_maui_youtube.Models
{
    public static class Constants
    {
        public static string ApplicationName = "YouTube";
        public static string EmailAddress = @"youtube@gmail.com";
        public static string ApplicationId = "nickdferrara.YouTube.App";
        public static string ApiServiceURL = @"https://youtube.googleapis.com/youtube/v3/";
        public static string ApiKey = @"*";


        public static uint MicroDuration { get; set; } = 100;
        public static uint SmallDuration { get; set; } = 300;
        public static uint MediumDuration { get; set; } = 600;
        public static uint LongDuration { get; set; } = 1200;
        public static uint ExtraLongDuration { get; set; } = 1800;
    }
}
