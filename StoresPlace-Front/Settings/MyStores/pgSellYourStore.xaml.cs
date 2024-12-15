using Microsoft.IdentityModel.Tokens;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Strings;

namespace StoresPlace_Front.Settings.MyStores;

public partial class pgSellYourStore : ContentPage
{
    int? _StoreID;

    public pgSellYourStore(int? StoreID)
	{
		InitializeComponent();

        _StoreID = StoreID;

    }

    private async void btAddYourStoreToSell_Clicked(object sender, EventArgs e)
    {
        if (enStorePrice.Text.IsNullOrEmpty())
        {
            await DisplayAlert(AppStrings.Error, AppStrings.Please_enter_the_price, AppStrings.Ok);
            return;
        }

        clsStoresForSale StoreForSell = new clsStoresForSale(new StoresForSaleDTO(null,_StoreID,decimal.Parse(enStorePrice.Text), 1));

        if (StoreForSell.Save())
        {
            ctvActiveIndector1.IsRunning = true;

            await Task.Delay(10);

            await DisplayAlert(AppStrings.Done, AppStrings.The_request_has_been_successfully_submitted, AppStrings.Ok);

            ctvActiveIndector1.IsRunning = false;

        }
        else
        {
            await DisplayAlert(AppStrings.Error, AppStrings.The_request_was_not_submitted_successfully, AppStrings.Ok);
            return;
        }
    }
}