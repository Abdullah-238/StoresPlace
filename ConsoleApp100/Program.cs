// See https://aka.ms/new-console-template for more information




using StoresPlace_APIs.Stores;
using StoresPlace_Business;
using StoresPlace_DataAccess;




var storeDetailsDTOs = clsStore.GetAllStoresInDetailsByPersonIDEn(1003);


foreach(var storeDetails in storeDetailsDTOs)
{
    Console.WriteLine(storeDetails.Name);
}