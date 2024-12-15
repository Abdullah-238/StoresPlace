using Microsoft.IdentityModel.Tokens;
using Microsoft.Maui.Storage;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Sqlite.Regions;
using StoresPlace_Front.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace StoresPlace_Front.Settings.SettingsViewModel
{
    public class clsAddUpdateStoreViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        List<string> _regions { get; set; }
        public List<string> Regions
        {
            get => _regions;
            set
            {
                _regions = value;
                OnPropertyChanged();
            }
        }
        List<string> _cities { get; set; }
        public List<string> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }
        List<string> _districts { get; set; }
        public List<string> Districts
        {
            get => _districts;
            set
            {
                _districts = value;
                OnPropertyChanged();
            }
        }

        List<string> _categories { get; set; }
        public List<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        List<string> _types { get; set; }
        public List<string> Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged();
            }
        }
        public clsStore Store { get; set; }

        public bool IsAccept { get; set; }

        private string _selectedRegion;

        public string SelectedRegion
        {
            get => _selectedRegion;
            set
            {
                if (_selectedRegion != value)
                {
                    _selectedRegion = value;
                    OnPropertyChanged(nameof(SelectedRegion));
                    OnRegionSelected();
                }
            }
        }

        private string _selectedCity;

        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                    OnCitySelected();
                }
            }
        }


        private string _selectedDistrict;

        public string SelectedDistrict
        {
            get => _selectedDistrict;
            set
            {
                if (_selectedDistrict != value)
                {
                    _selectedDistrict = value;
                    OnPropertyChanged(nameof(SelectedDistrict));
                    // OnDistrictSelected();
                }
            }
        }


        private string _selectedCategory;

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(_selectedCategory));
                }
            }
        }

        private string _selectedType;

        public string SelectedType
        {
            get => _selectedType;
            set
            {
                if (_selectedType != value)
                {
                    _selectedType = value;
                    OnPropertyChanged(nameof(_selectedType));
                }
            }
        }

        public ICommand TakePhotoCommand { get; set; }

        public ICommand SaveData { get; set; }

        public clsAddUpdateStoreViewModel()
        {
            TakePhotoCommand = new Command(TakePhoto);
            SaveData = new Command(btCreate_Clicked);
        }
        public void OnRegionSelected()
        {
            string selectedRegion = _selectedRegion;

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Cities = clsCity.GetAllCitiesByRegionNameAr(selectedRegion);
            }
            else
            {
                Cities = clsCity.GetAllCitiesByRegionNameEn(selectedRegion);
            }

        }

        public void OnCitySelected()
        {
            string selectedCity = _selectedCity;

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Districts = clsDistrict.GetAllDistrictByCityNameAr(selectedCity); ;
            }
            else
            {
                Districts = clsDistrict.GetAllDistrictByCityNameEn(selectedCity); ;
            }
        }


        public void _Load()
        {
            IsBusy = true;

            Task.Delay(10);

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                if (!clsRegionArDataLite.isRegionsArSaved())
                {
                    clsRegionArDataLite.SaveRegionsArAsync(clsRegion.GetAllRegionsByNameAr());
                }

                Regions = clsRegionArDataLite.LoadRegionsArAsync();

                Types = clsType.GetAllTypeAr();

                Categories = clsCategories.GetAllCategoryAr();
            }
            else
            {
                if (!clsRegionEnDataLite.isRegionsEnSaved())
                {
                    clsRegionEnDataLite.SaveRegionsEnAsync(clsRegion.GetAllRegionsByNameEn());
                }

                Regions = clsRegionEnDataLite.LoadRegionsEnAsync();

                Categories = clsCategories.GetAllCategoryEn();

                Types = clsType.GetAllTypeEn();
            }

            Store = new clsStore();
            Store.Rating = 5;
            Store.NumbersOfClick = 0;
            Store.NumberOfRates = 0;
            Store.Status = 2;
            Store.PeronID = clsGlobal.CurrentUser.PersonID;


            IsBusy = false;

        }

        public void _Load(int StoreID)
        {
            IsBusy = true;

            Task.Delay(10);

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Regions = clsRegion.GetAllRegionsByNameAr();

                Store = clsStore.Find(StoreID);

                Types = clsType.GetAllTypeAr();

                Categories = clsCategories.GetAllCategoryAr();

                SelectedCategory = clsCategories.Find(Store.CategoryID).CategoryNameAr;

                SelectedType = clsType.Find(Store.TypeID).TypeNameAr;

                SelectedRegion = clsRegion.Find(clsCity.Find(clsDistrict.Find(Store.DistrictsID).CityID).RegionID).RegionNameAr;

                SelectedCity = clsCity.Find(clsDistrict.Find(Store.DistrictsID).CityID).CityNameAr;

                SelectedDistrict = clsDistrict.Find(Store.DistrictsID).DistrictsNameAr;
            }
            else
            {
                Regions = clsRegion.GetAllRegionsByNameEn();

                Store = clsStore.Find(StoreID);

                Categories = clsCategories.GetAllCategoryEn();

                Types = clsType.GetAllTypeEn();

                SelectedRegion = clsRegion.Find(clsCity.Find(clsDistrict.Find(Store.DistrictsID).CityID).RegionID).RegionNameEn;

                SelectedCity = clsCity.Find(clsDistrict.Find(Store.DistrictsID).CityID).CityNameEn;

                SelectedDistrict = clsDistrict.Find(Store.DistrictsID).DistrictsNameEn;

                SelectedCategory = clsCategories.Find(Store.CategoryID).CategoryNameEn;

                SelectedType = clsType.Find(Store.TypeID).TypeNameEn;
            }

            IsBusy = false;

        }


        public static bool IsValidUri(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }


        private async void btCreate_Clicked()
        {
            IsBusy = true;

            await Task.Delay(10);

            if (string.IsNullOrEmpty(Store.Name) || string.IsNullOrEmpty(Store.Website) ||
                string.IsNullOrEmpty(Store.Address) || string.IsNullOrEmpty(Store.CommercialNumber))
            {
                await AppShell.Current.DisplayAlert(null, AppStrings.Please_fill_all_fields_before_continue, AppStrings.Ok);

                return;
            }

            if (!IsAccept)
            {
                await AppShell.Current.DisplayAlert(AppStrings.Terms_Conditions, AppStrings.Accept_Terms, AppStrings.Ok);
                return;
            }

            if (!IsValidUri(Store.Website))
            {
                await AppShell.Current.DisplayAlert(AppStrings.Error, AppStrings.Please_Check_Webiste_link, AppStrings.Ok);
                return;
            }


            if (SelectedCategory == null || SelectedCity == null || SelectedType == null || SelectedRegion == null)
            {
                await AppShell.Current.DisplayAlert(null, AppStrings.Please_fill_all_fields_before_continue, AppStrings.Ok);

                return;
            }

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Store.TypeID = clsType.FindTypeAr(SelectedType).TypeID;

                Store.CategoryID = clsCategories.FindCategoryByCategoryNameAr(SelectedCategory).CategoryID;

                if (SelectedDistrict != null)
                    Store.DistrictsID = clsDistrict.FindDistrictsNameAr(SelectedDistrict).DistrictsID;
            }
            else
            {
                Store.TypeID = clsType.FindTypeEn(SelectedType).TypeID;

                Store.CategoryID = clsCategories.FindCategoryByCategoryNameEn(SelectedCategory).CategoryID;

                if (SelectedDistrict != null)
                    Store.DistrictsID = clsDistrict.FindDistrictsNameEn(SelectedDistrict).DistrictsID;
            }

            //UploadImageToFtp();

            UploadImageToFTP(Store.Photo);

            if (Store.Save())
            {
                IsBusy = true;

                await Task.Delay(10);

                await AppShell.Current.DisplayAlert(AppStrings.Done, AppStrings.Data_Saved_Successfully, AppStrings.Ok);

                await Shell.Current.GoToAsync("..");


                IsBusy = false;

            }
            else
            {
                await AppShell.Current.DisplayAlert(AppStrings.Error, AppStrings.Data_saved_failed, AppStrings.Ok);
                return;


            }

            IsBusy = false;

        }




        public async void TakePhoto()
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

                    Store.Photo = localFilePath;

                }
            }

        }

      
        //public static void UploadImageToFTP()
        //{
        //    string ftpServer = "ftp://win6057.site4now.net", ftpUsername = @"abdullah0-001", ftpPassword = @"Qq-12341234", remoteFolder = "storesplace";

        //    // Get the file name from the photo's path
        //    string fileName = Path.GetFileName(filePath);
        //    // Construct the FTP URL for uploading
        //    string fileUrl = $"{ftpServer}/{remoteFolder}/{fileName}";

        //    using (WebClient client = new WebClient())
        //    {
        //        // Set the FTP credentials
        //        client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

        //        try
        //        {
        //            // Upload the file to the FTP server
        //            client.UploadFile(new Uri(fileUrl), WebRequestMethods.Ftp.UploadFile, filePath);

        //            // Construct the URL to access the uploaded image (public URL)
        //            string publicUrl = $"http://abdullah0-001-site1.mtempurl.com/{remoteFolder}/{fileName}";

        //            // Update the store's photo property with the URL
        //            Console.WriteLine($"Image uploaded successfully. Access it at: {publicUrl}");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception (use your own logging method)
        //            Console.WriteLine("Error uploading file: " + ex.Message);
        //        }
        //    }
        


        private void UploadImageToFtp()
        {

            string ftpServer = "ftp://win6057.site4now.net", ftpUsername = @"abdullah0-001", ftpPassword = @"Qq-12341234", remoteFolder = "storesplace";    

            string fileName = Path.GetFileName(Store.Photo); 
            string fileUrl = $"{ftpServer}/{remoteFolder}/{fileName}"; 

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                try
                {
                     client.UploadFile(new Uri(fileUrl), WebRequestMethods.Ftp.UploadFile, Store.Photo);

                    string publicUrl = $"http://abdullah0-001-site1.mtempurl.com/{remoteFolder}/{fileName}";


                    Store.Photo = publicUrl;
                }
                catch (Exception ex)
                {
                    clsUtil.WriteExceptionInLogFile(ex);
                }
            }


            //string ftpServer = "ftp://win6057.site4now.net", ftpUsername = @"abdullah0-001", ftpPassword = @"Qq-12341234", remoteFolder = "storesplace";

            //string fileName = Path.GetFileName(Store.Photo);
            //string fileUrl = $"{ftpServer}/{remoteFolder}/{fileName}";

            //string localFilePath = Store.Photo;

            //string httpUrl = $"http://win6057.site4now.net/{remoteFolder}/{fileName}";  // Update this to the HTTP URL for access


            //using (WebClient client = new WebClient())
            //{
            //    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            //    try
            //    {
            //        client.UploadFile(new Uri(fileUrl), WebRequestMethods.Ftp.UploadFile, localFilePath);

            //        Store.Photo = httpUrl;
            //    }
            //    catch (Exception ex)
            //    {
            //        clsUtil.WriteExceptionInLogFile(ex);
            //    }
            //}

        }

        public  void UploadImageToFTP(string filePath)

        {     
            
            string ftpServer = "ftp://win6057.site4now.net", ftpUsername = @"abdullah0-001", ftpPassword = @"Qq-12341234", remoteFolder = "storesplace";

        // Get the file name from the photo's path
            string fileName = Path.GetFileName(filePath);
            // Construct the FTP URL for uploading the image
            string fileUrl = $"{ftpServer}/{remoteFolder}/{fileName}";

            using (WebClient client = new WebClient())
            {
                // Set the FTP credentials
                client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                try
                {
                    // Upload the file to the FTP server
                    client.UploadFile(new Uri(fileUrl), WebRequestMethods.Ftp.UploadFile, filePath);

                    // Construct the public URL to access the uploaded image
                    string publicUrl = $"http://abdullah0-001-site1.mtempurl.com/{remoteFolder}/{fileName}";

                    Store.Photo = publicUrl;
                    // Assuming you have a property to store the URL, update it
                    // Example: Store.Photo = publicUrl; (adjust based on your implementation)
                    Console.WriteLine($"Image uploaded successfully. Access it at: {publicUrl}");

                    // Optionally, update the Store.Photo or relevant property to the public URL
                    // Store.Photo = publicUrl; // Update your store with the public URL

                }
                catch (Exception ex)
                {
                    // Log the exception (you can use your custom logging method here)
                    Console.WriteLine("Error uploading file: " + ex.Message);
                    clsUtil.WriteExceptionInLogFile(ex);  // Assuming you use clsUtil for logging
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}