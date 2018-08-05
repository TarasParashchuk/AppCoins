using Admin_App_Coins.HelpFunctions;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Admin_App_Coins.PageActivity
{
    [Activity(Label = "Admin_App_Coins", MainLauncher = false)]
    public class CategoryPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CategoryPages);
            var _GridView = FindViewById<GridView>(Resource.Id.gridView1);

            ActionBar.Title = Intent.GetStringExtra("name");
            var id = Intent.GetIntExtra("_id", 0);

            var uri = new System.Uri("http://185.86.78.146/CategoryMetod/SelectDataCategory.php");
            var list = new GetDataMySQL(uri, "ID_Ruler", id.ToString()).GetDataCategory();
            _GridView.Adapter = new CategoryPage(this, list);
        }
    }
}

