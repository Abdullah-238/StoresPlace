
using StoresPlace.Global;
using StoresPlace;
using System.Globalization;

namespace StoresPlace_Front;

public partial class pgIntroUser : ContentPage
{
    public class Intro
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Image { get; set; }

        public Intro(string title, string subTitle , string image)
        {
            this.Title = title;
            this.SubTitle = subTitle;
            this.Image = image;
        }
    }

    List<Intro> intro = new List<Intro>();
    
    public pgIntroUser()
    {
        InitializeComponent();

        //_LoadIntroDetiles();
    }

   



    void _LoadIntroDetiles()
    {


        //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
        //CultureInfo.CurrentUICulture = new CultureInfo("ar-SA");

        if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
        {


            intro = new List<Intro>
            {
            new Intro("œÊ·«» ”Õ«»Ì" , "Œ·Ì œÊ·«»ﬂ Ê „Õ ÊÌ« Â ›Ì ÃÊ«·ﬂ" , "done_all"),
            new Intro("«·ﬂ· ›Ì Ê«Õœ" ,"⁄‰«Ì… Ê ‰ŸÌ› Ê«œ«—… „Œ“Ê‰ „·«»”ﬂ ›Ì  ÿ»Ìﬁ Ê«Õœ" , "done_all"),
            new Intro("ÃœÊ· „·«»”ﬂ" , " ÕœÌœ  «—ÌŒ Ê Êﬁ  «” ·«„ ﬂ· ﬁÿ⁄…" , "date"),
            new Intro("Ê›—" , " Ê›Ì— „”«Õ…†›Ì†»Ì ﬂ†Ê†Êﬁ ﬂ" , "date")
            };
        }
        else
            intro = new List<Intro>
            {
                    new Intro("Cloud Wardrobe" , "Keep your Wardrobe and its contents in your phone" , "done_all"),
            new Intro("All in one" ,"Care, clean and manage your clothing inventory in one application" , "done_all"),
            new Intro("Scheduling your clothes" , "Specify the date and time of receipt of each item" , "date"),
            new Intro("Save" , "Save space in your home and time" , "date")
            };

        clvIntro.ItemsSource = intro; 
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new pgLogUp());
    }

    private async void btLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new pgLoginPage());
    }

    private void Continue_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }
}