using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Strings;

namespace StoresPlace_Front.Settings.MyStores;

public partial class pgAddNewCoupoun : ContentPage
{
    int? _StoreID;

    public pgAddNewCoupoun(int? StoreID)
	{
		InitializeComponent();

        _StoreID = StoreID;

    }

    private async void btAddNewCoupoun_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(enCoupon.Text))
        {
            await DisplayAlert(AppStrings.Done, AppStrings.Please_fill_all_fields_before_continue, AppStrings.Ok);
            return;
        }

        clsCoupon Coupon = new clsCoupon(new CouponDTO(null, enCoupon.Text,_StoreID,dtpExpiredDate.Date , ckbIsActive.IsChecked));

        if (Coupon.Save())
        {
            ctvActiveIndector1.IsRunning = true;

            await Task.Delay(10);

            await DisplayAlert(AppStrings.Done, AppStrings.Your_offer_has_been_added_successfully, AppStrings.Ok);

            ctvActiveIndector1.IsRunning = false;

        }
        else
        {
            await DisplayAlert(AppStrings.Error, AppStrings.Your_Coupon_has_been_added_falied, AppStrings.Ok);
            return;
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        dtpExpiredDate.MinimumDate = DateTime.Now;
    }
}