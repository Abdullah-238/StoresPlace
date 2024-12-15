using StoresPlace.Global;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Sqlite.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoresPlace_Front.Stores
{

    public class StoreViewModel : INotifyPropertyChanged
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
        List<StoreDetailsDTO> _stores { get; set; }
        public List<StoreDetailsDTO> Stores
        {
            get => _stores;
            set
            {
                _stores = value;
                OnPropertyChanged();
            }
        }
        public ICommand SelectStore {  get; set; }

        public ICommand MoveToSavedStoreCommand { get; set; }

        string _CategoryName;

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
                    OnDistrictSelected();
                }
            }
        }

        List<StoreDetailsDTO> _mySavedStore { get; set; }
        public List<StoreDetailsDTO> MySavedStores
        {
            get => _mySavedStore;
            set
            {
                _mySavedStore = value;
                OnPropertyChanged();
            }
        }


        public void LoadSavedStores()
        {
            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                MySavedStores = clsSavedStore.GetAllSavedStoreByPersonIDAr(clsGlobal.CurrentUser.PersonID);
            }
            else
                MySavedStores = clsSavedStore.GetAllSavedStoreByPersonIDEn(clsGlobal.CurrentUser.PersonID);

        }


        public StoreViewModel()
        {
            SelectStore = new Command<int>(OnStoreSelected);

            MoveToSavedStoreCommand = new Command<int>(MoveToSavedStore);
        }

        public void _Load(string CategoryName)
        {
            _CategoryName = CategoryName;

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                if (!clsRegionArDataLite.isRegionsArSaved())
                {
                    Regions = clsRegion.GetAllRegionsByNameAr();

                    clsRegionArDataLite.SaveRegionsArAsync(Regions);
                }

                Regions = clsRegionArDataLite.LoadRegionsArAsync();

                Stores = clsStore.GetAllStoreByCategoryNameAr(CategoryName,clsGlobal.TypeID);
            }
            else
            {
                if (!clsRegionEnDataLite.isRegionsEnSaved())
                {
                    Regions = clsRegion.GetAllRegionsByNameEn();

                    clsRegionEnDataLite.SaveRegionsEnAsync(Regions);
                }

                Regions = clsRegionEnDataLite.LoadRegionsEnAsync();

                Stores = clsStore.GetAllStoreByCategoryNameEn(CategoryName,clsGlobal.TypeID);
            }
        }



        private async void OnStoreSelected(int StoreID)
        {
            IsBusy = true;

            await Task.Delay(10);

            if (StoreID != null)
            {
                await AppShell.Current.GoToAsync($"StorePage?storeID={StoreID}");
            }

            IsBusy = false;
        }


        async void MoveToSavedStore(int StoreID)
        {
            IsBusy = true;

            if (StoreID != null)
            {
                await AppShell.Current.GoToAsync($"SavedStorePage?storeID={StoreID}");
            }
            IsBusy = false;

        }



        public void OnRegionSelected()
        {
            string selectedRegion = _selectedRegion;

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Cities = clsCity.GetAllCitiesByRegionNameAr(selectedRegion);

                Stores = clsStore.GetAllStoreByCategoryNameArAndRegionName(_CategoryName, selectedRegion, clsGlobal.TypeID);
            }
            else
            {
                Cities = clsCity.GetAllCitiesByRegionNameEn(selectedRegion);

                Stores = clsStore.GetAllStoreByCategoryNameEnAndRegionName(_CategoryName, selectedRegion, clsGlobal.TypeID);
            }

        }

        public void OnCitySelected()
        {
            string selectedCity = _selectedCity;

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Districts = clsDistrict.GetAllDistrictByCityNameAr(selectedCity); ;

                Stores = clsStore.GetAllStoreByCategoryNameArAndCityNameAr(_CategoryName, selectedCity, clsGlobal.TypeID);
            }
            else
            {
                Districts = clsDistrict.GetAllDistrictByCityNameEn(selectedCity); ;

                Stores = clsStore.GetAllStoreByCategoryNameEnAndCityNameEn(_CategoryName, selectedCity, clsGlobal.TypeID);
            }
        }

        public void OnDistrictSelected( )
        {
            string selectedCity = _selectedDistrict;

            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
            {
                Stores = clsStore.GetAllStoreByCategoryNameArAndDistrictsNameAr(_CategoryName, selectedCity, clsGlobal.TypeID);
            }
            else
            {
                Stores = clsStore.GetAllStoreByCategoryNameEnAndDistrictsNameEn(_CategoryName, selectedCity, clsGlobal.TypeID);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
