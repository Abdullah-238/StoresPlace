
using StoresPlace;
using StoresPlace_Front.Strings;
using StoresPlace_Business;
using StoresPlace_Front;


public class clsGlobal
{



    public static clsPerson CurrentUser;

    public static int? TypeID { get; set; }

    public async static void CheckInitialConnectivity(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess != NetworkAccess.Internet)
        {
            await AppShell.Current.GoToAsync("NoInternetConnection");
        }
    }

    public static async void CheckForUpdateAsync()
    {
        var currentVersion = AppInfo.VersionString;

        var latestVersion = clsAppSettings.VersionString();

        if (currentVersion != latestVersion)
        {
            await AppShell.Current.GoToAsync("UpdatePage");
        }
    }

};
