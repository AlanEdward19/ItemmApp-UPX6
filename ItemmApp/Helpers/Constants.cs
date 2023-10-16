namespace ItemmApp.Helpers
{
    public class Constants
    {
        public static string ApiUrl => DeviceInfo.Platform == DevicePlatform.WinUI ? "https://localhost:5000" : "http://10.0.2.2:5000";
    }
}

