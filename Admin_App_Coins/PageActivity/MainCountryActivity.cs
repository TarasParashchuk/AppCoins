using Android.App;

using Android.OS;

namespace Admin_App_Coins
{
    [Activity(Label = "MainCountryActivity", MainLauncher = false)]
    public class MainCountryActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainPage);


            // Create your application here
        }
    }
}