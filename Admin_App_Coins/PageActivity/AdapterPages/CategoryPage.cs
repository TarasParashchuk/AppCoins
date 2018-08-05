using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Admin_App_Coins.Model;
using System.Collections.Generic;
using Android.App;
using System.Linq;
using Admin_App_Coins.PageActivity;

namespace Admin_App_Coins
{
    class CategoryPage : BaseAdapter
    {
        private List<Model_Category> data;

        private Context context;

        public CategoryPage(Context context, List<Model_Category> data)
        {
            this.data = data;
            this.context = context;
        }

        public override int Count
        {
            get
            {
                return data.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            var item = data[position];

            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.ItemCategoryPages, null, false);

                var layout = view.FindViewById<CustomLinearLayout>(Resource.Id.Layout_category_item);
                layout.Id_Item = item.Id_Category;
                layout.Click += ItemCategoryClick;

                var image = view.FindViewById<ImageView>(Resource.Id.ImageCategory);
                //var imageBitmap = new ConvertImage().GetImageBitmapFromUrl(item.Icon);
                //image.SetImageBitmap(imageBitmap);

                var text = view.FindViewById<TextView>(Resource.Id.Text_Name_Category);
                text.Text = item.Str_Denomination;

                var Button_CountCategory = view.FindViewById<Button>(Resource.Id.ButtonCountCategory);
                Button_CountCategory.Text = item.Count.ToString();
            }

            return view;
        }

        private void ItemCategoryClick(object sender, EventArgs args)
        {
            var Layout = (CustomLinearLayout)sender;
            var item = data.Where(u => u.Id_Category == Layout.Id_Item).First();

            var Activity = (Activity)context;

            var activity = new Intent(Activity, typeof(CoinsPageActivity));
            activity.PutExtra ("_id", item.Id_Category);
            activity.PutExtra("name", item.Str_Denomination);
            Activity.StartActivity(activity);
        }
    }
}