using StoresPlace_Business;
using StoresPlace_DataAccess;
using System.Data;
using System.Globalization;

namespace StoresPlace_Front.Stores;


[QueryProperty("CategoryName", "categoryName")]

public partial class pgStores : ContentPage
{

    StoreViewModel store = new StoreViewModel();
    public string CategoryName { get; set; }

    public pgStores()
	{
		InitializeComponent();

        BindingContext = store;

    }
    private void StorePage_Loaded(object sender, EventArgs e)
    {
        store._Load(CategoryName);
    }

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        store._Load(CategoryName);

        refresh.IsRefreshing = false;

    }
}