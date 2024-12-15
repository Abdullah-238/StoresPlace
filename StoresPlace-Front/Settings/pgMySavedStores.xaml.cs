using StoresPlace_DataAccess;
using StoresPlace_Front.Settings.SettingsViewModel;
using StoresPlace_Front.Stores;

namespace StoresPlace_Front.Settings;

public partial class pgMySavedStores : ContentPage
{
    StoreViewModel clsMyStoreViewModel = new StoreViewModel();

    public pgMySavedStores()
	{
		InitializeComponent();
	}

    private void PreviousOrderPage_Loaded(object sender, EventArgs e)
    {

        clsMyStoreViewModel.LoadSavedStores();

        this.BindingContext = clsMyStoreViewModel;
    }

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        clsMyStoreViewModel.LoadSavedStores();

        refresh.IsRefreshing = false;
    }
}