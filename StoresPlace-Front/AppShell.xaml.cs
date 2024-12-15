using StoresPlace_Front.AppSettings;
using StoresPlace_Front.Categories;
using StoresPlace_Front.GlobalPages;
using StoresPlace_Front.Settings;
using StoresPlace_Front.Settings.MyStores;
using StoresPlace_Front.Stores;
using StoresPlace_Front.StoresForSell;

namespace StoresPlace_Front
{
    public partial class AppShell : Shell
    {
       public  AppShell()
        {
            InitializeComponent();

            _RegisterPage();

            Connectivity.ConnectivityChanged += clsGlobal.CheckInitialConnectivity;

        }

       

        void _RegisterPage()
        {

            //Routing.RegisterRoute("MainPage", typeof(MainPage));

            Routing.RegisterRoute("MainPage/Categories", typeof(pgCategories));

            Routing.RegisterRoute("MainPage/Categories/Stores", typeof(pgStores));

            Routing.RegisterRoute("MainPage/Categories/Stores/StorePage", typeof(pgStorePage));


            Routing.RegisterRoute("StoresForSell", typeof(pgStoresForSell));

            Routing.RegisterRoute("StoresForSell/StorePageForSell", typeof(pgStorePage));



            //Routing.RegisterRoute("Settings/PreviousOrder", typeof(pgPreviousOrder));

            //Routing.RegisterRoute("Settings/Review", typeof(pgGetAllReviews));

            Routing.RegisterRoute("Settings/Profile", typeof(pgProfile));

            Routing.RegisterRoute("Settings/MyStores", typeof(pgMyStores));

            Routing.RegisterRoute("Settings/MyStores/MyStorePage", typeof(pgMyStorePage));

            Routing.RegisterRoute("Settings/AddNewStore", typeof(pgAddNewStore));

            Routing.RegisterRoute("Settings/AppSettings", typeof(pgAppSettings));

            Routing.RegisterRoute("Settings/MySavedStores", typeof(pgMySavedStores));

            Routing.RegisterRoute("Settings/MySavedStores/SavedStorePage", typeof(pgStorePage));


            //Routing.RegisterRoute("Settings/Complain/AddNewComplain", typeof(pgComplain));

            //Routing.RegisterRoute("Settings/Complain/AddNewComplain/SentComplainSuccessfully", typeof(pgSentComplainSuccuess));

            //Routing.RegisterRoute("Settings/Address", typeof(pgAddress));


            Routing.RegisterRoute("NoInternetConnection", typeof(pgInternetConnecation));

            Routing.RegisterRoute("UpdatePage", typeof(pgUpdatePage));



            //Routing.RegisterRoute("pgDoulby/pgItemsPage", typeof(pgItemsPage));

            //Routing.RegisterRoute("pgDoulby/pgItemsPage/pgScheduleItems", typeof(pgScheduleItems));


        }
    }
}
