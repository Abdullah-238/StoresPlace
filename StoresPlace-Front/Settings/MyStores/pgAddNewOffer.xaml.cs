using Microsoft.IdentityModel.Tokens;
using StoresPlace.Global;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Strings;

namespace StoresPlace_Front.Settings.MyStores;

public partial class pgAddNewOffer : ContentPage
{

	int? _StoreID;
	public pgAddNewOffer(int ? StoreID)
	{
		InitializeComponent();

        _StoreID = StoreID;

    }

    private async void btAddNewOffer_Clicked(object sender, EventArgs e)
    {
        if (enOffer.Text.IsNullOrEmpty())
        {
            await DisplayAlert(AppStrings.Done, AppStrings.Please_fill_all_fields_before_continue, AppStrings.Ok);
            return;
        }

        clsOffer offer  = new clsOffer(new OfferDTO(10, enOffer.Text, _StoreID));

        if (offer.Save())
        {
            ctvActiveIndector1.IsRunning = true;

            await Task.Delay(10);

            await DisplayAlert(AppStrings.Done, AppStrings.Your_offer_has_been_added_successfully, AppStrings.Ok);

            ctvActiveIndector1.IsRunning = false;

        }
        else
        {
            await DisplayAlert(AppStrings.Error, AppStrings.Your_offer_has_been_added_falied, AppStrings.Ok);
            return;
        }
    }
}