
using StoresPlace.Global;
using StoresPlace_Front.Strings;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using StoresPlace_Front.Global;
namespace StoresPlace_Front.AppSettings;

public partial class pgProfile : ContentPage
{
    clsPerson customer = clsGlobal.CurrentUser;

    public pgProfile()
	{
		InitializeComponent();

        this.BindingContext = customer;
	}

   

    private async void btCreate_Clicked(object sender, EventArgs e)
    {
        
        if (string.IsNullOrEmpty(enEmail.Text) || string.IsNullOrEmpty(enName.Text) || string.IsNullOrEmpty(enPhone.Text))
        {
            await DisplayAlert(null, AppStrings.Please_fill_all_fields_before_continue, AppStrings.Ok);

            return;
        }

        if ( clsPerson.IsPersonExistsByEmail(enEmail.Text.Trim()) && customer.Email != enEmail.Text)
        {
            await DisplayAlert(AppStrings.Email, AppStrings.Please_enter_another_email_this_email_is_Exist, AppStrings.Ok);
            return;
        }

        if ( clsPerson.IsPersonExistsByPhone(enPhone.Text.Trim()) && customer.Phone != enPhone.Text)
        {
            await DisplayAlert(AppStrings.Phone, AppStrings.Please_enter_another_phone_this_phone_is_Exist, AppStrings.Ok);
            return;
        }

        if (enPhone.Text.Trim().Length < 9 || !enPhone.Text.StartsWith("5"))
        {
            await DisplayAlert(AppStrings.Phone, AppStrings.Please_Enter_Valid_Phone, AppStrings.Ok);
            return;
        }



        if (customer != null)
        {
            customer.Phone = enPhone.Text.Trim();
            customer.Email = enEmail.Text.Trim();
            customer.Name = enName.Text.Trim();

            Preferences.Default.Remove(clsAppConstants.Email);
            Preferences.Default.Remove(clsAppConstants.Password);

            Preferences.Default.Set(clsAppConstants.Email, clsUtil.Encrypt(customer.Email));
            Preferences.Default.Set(clsAppConstants.Password, clsUtil.Encrypt(customer.Password));

            if (!string.IsNullOrEmpty(enPassword.Text))
            {
                if (enPassword.Text.Length < 8)
                {
                    await DisplayAlert(AppStrings.Password, AppStrings.please_enter_a_valid_password, AppStrings.Ok);
                    return;
                }
                else
                    customer.Password = clsUtil.ComputeHash(enPassword.Text.Trim());
            }
            else
                customer.Password = clsGlobal.CurrentUser.Password;
        }

        if (customer.Save())
		{
            clsGlobal.CurrentUser = customer;

            MyTool.MyToast(AppStrings.Data_Saved_Successfully);

            //await DisplayAlert(AppStrings.Done, AppStrings.Data_Saved_Successfully, AppStrings.Ok);


            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //ToastDuration duration = ToastDuration.Long;
            //double fontSize = 14;


            //var toast = Toast.Make("message", duration,fontSize);

            //await toast.Show(cancellationTokenSource.Token);
        }
		else
		{
            await DisplayAlert(AppStrings.Error, AppStrings.Data_saved_failed, AppStrings.Ok);

            MyTool.MyToast(AppStrings.Data_saved_failed);

        }

    }

    private async void  ContentPage_Loaded(object sender, EventArgs e)
    {
       //if (clsGlobal.CurrentUser != null)
       // {
       //     enPhone.Text =            customer.Phone;
       //     enEmail.Text =            customer.Email;
       //     enName.Text =             customer.Namee;
       //     //imgCustomerImage.Source = customer.Photo ;
       // }

        if (clsGlobal.CurrentUser == null)
        {
            btCreate.IsEnabled = false;

            this.IsVisible = false;

            await DisplayAlert(AppStrings.Sign_in, AppStrings.Please_sign_in_to_Complete_process, AppStrings.Ok);

            await Shell.Current.GoToAsync("..");

            return;
        }

    }


}