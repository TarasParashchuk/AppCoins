using Admin_App_Coins.HelpFunctions;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Admin_App_Coins.PageActivity
{
    [Activity(Label = "Admin_App_Coins", MainLauncher = false)]
    public class CoinsPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PageCoins);
            var _ListView = FindViewById<ListView>(Resource.Id.listView);

            ActionBar.Title = Intent.GetStringExtra("name");
            var id = Intent.GetIntExtra("_id", 0);

            var uri = new System.Uri("http://185.86.78.146/CoinMetod/SelectDataCoin.php");
            var list = new GetDataMySQL(uri, "ID_Category", id.ToString()).GetDataCoins();
            _ListView.Adapter = new CoinsPage(this, list);
        }
    }
}

