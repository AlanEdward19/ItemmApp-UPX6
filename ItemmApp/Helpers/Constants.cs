namespace ItemmApp.Helpers
{
    public class Constants
    {
        public static string ApiUrl => DeviceInfo.Platform == DevicePlatform.WinUI ? "https://localhost:5000" : "192.168.184.109:5000";
    }
}

