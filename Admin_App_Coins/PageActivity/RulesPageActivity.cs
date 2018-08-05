using Android.App;
using Android.OS;
using Android.Widget;
using Admin_App_Coins.HelpFunctions;
using Admin_App_Coins.AdapterPages;
using Android.Views;

namespace Admin_App_Coins
{
    [Activity(Label = "Admin_App_Coins", MainLauncher = false)]
    public class RulesPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RulerPages);
            var _GridView = FindViewById<GridView>(Resource.Id.gridView2);

            var editToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_edit_toolbar);
            editToolbar.Title = "Редактирование";
            editToolbar.InflateMenu(Resource.Menu.edit_toolbar);

            ActionBar.Title = Intent.GetStringExtra("name");
            var id = Intent.GetIntExtra("_id", 0);

            var uri = new System.Uri("http://185.86.78.146/RulerMetod/SelectDataRuler.php");
            var list = new GetDataMySQL(uri, "ID_Period", id.ToString()).GetDataRules();
           _GridView.Adapter = new RulesPage(this, list);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}

