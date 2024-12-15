using Microsoft.Maui.Controls;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Settings.SettingsViewModel;
using StoresPlace_Front.Strings;
using System.Globalization;
using System.Net;

namespace StoresPlace_Front.Settings.MyStores;

public partial class pgAddNewStore : ContentPage
{


    clsAddUpdateStoreViewModel storeViewModel = new clsAddUpdateStoreViewModel();
    public pgAddNewStore(int? StoreID)
    {
        InitializeComponent();

        storeViewModel._Load(StoreID.Value);

        this.BindingContext = storeViewModel;

    }

    public pgAddNewStore()
    {
        InitializeComponent();

        //Mode = enMode.eAdd;

        storeViewModel._Load();

        this.BindingContext = storeViewModel;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        FileResult photo = await MediaPicker.Default.PickPhotoAsync();

        if (MediaPicker.Default.IsCaptureSupported)
        {
            if (photo != null)
            {
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                storeViewModel.Store.Photo = localFilePath;


                imgCustomerImage.Source = localFilePath;
            }
        }
    }
}
