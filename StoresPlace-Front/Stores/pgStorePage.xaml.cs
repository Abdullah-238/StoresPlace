using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Strings;
using System.Diagnostics;
using System.Globalization;
using System.Net;


namespace StoresPlace_Front.Stores;

[QueryProperty("StoreID", "storeID")]

public partial class pgStorePage : ContentPage
{


    StorePageViewModel StorePageVM = new StorePageViewModel();
    public int StoreID { get; set; }

    public pgStorePage()
	{
		InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        StorePageVM.Load(StoreID);

        StorePage.BindingContext = StorePageVM;

    }
}