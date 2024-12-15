using StoresPlace.Global;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Stores;
using StoresPlace_Front.Strings;
using System.Globalization;

namespace StoresPlace_Front.Settings.MyStores;

[QueryProperty("StoreID", "storeID")]

public partial class pgMyStorePage : ContentPage
{
    StorePageViewModel StorePageVM = new StorePageViewModel();
    public int StoreID { get; set; }
    public pgMyStorePage()
	{
		InitializeComponent();  
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        StorePageVM.Load(StoreID);

        StorePage.BindingContext = StorePageVM;
    }

 
    private async void btUpdateStore_Clicked(object sender, EventArgs e)
    {

        pgAddNewStore newStore = new pgAddNewStore(StoreID);

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;


        await Navigation.PushAsync(newStore);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        pgAddNewOffer newOffer = new pgAddNewOffer(StoreID);


        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;


        await Navigation.PushAsync(newOffer);
    }

    private void StorePage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        LoadingIndicator.IsVisible = false;
        LoadingIndicator.IsRunning = false;
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        pgAddNewCoupoun newCoupoun = new pgAddNewCoupoun(StoreID);

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        await Navigation.PushAsync(newCoupoun);
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        bool Delete = await AppShell.Current.DisplayAlert(AppStrings.Delete_Account, AppStrings.Are_you_sure_you_want_to_delete_your_store, AppStrings.Ok, AppStrings.Cancel);

        if (Delete)
        {
            if (clsStore.Delete(StoreID))
            {
                MyTool.MyToast(AppStrings.The_store_has_been_deleted_successfully);
            }
            else
                MyTool.MyToast(AppStrings.The_store_has_not_been_deleted_successfully);
        }

    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        pgSellYourStore StoreToSell = new pgSellYourStore(StoreID);

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        await Navigation.PushAsync(StoreToSell);
    }
}