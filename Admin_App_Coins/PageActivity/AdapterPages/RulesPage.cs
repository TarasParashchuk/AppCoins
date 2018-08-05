using Admin_App_Coins.HelpFunctions;
using Admin_App_Coins.Model;
using Admin_App_Coins.PageActivity;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Admin_App_Coins.AdapterPages
{
    class RulesPage : BaseAdapter
    {
        private List<Model_Rules> data;

        private Context context;

        public RulesPage(Context context, List<Model_Rules> data)
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
                view = LayoutInflater.From(context).Inflate(Resource.Layout.ItemRulerPages, null, false);

                var layout = view.FindViewById<CustomLinearLayout>(Resource.Id.Layout_ruler_item);
                layout.Id_Item = item.Id_Ruler;
                layout.Click += ItemRulerClick;

                var image = view.FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageRuler);
                ImageService.Instance.LoadUrl(item.Icon).WithCache(FFImageLoading.Cache.CacheType.Disk).Transform(new List<ITransformation>() { new CircleTransformation(5, "#FFB300") }).Into(image);

                var text = view.FindViewById<TextView>(Resource.Id.Text_Name_Ruler);
                text.Text = item.Ruler;

                var text_year = view.FindViewById<TextView>(Resource.Id.Text_Ruler_Year);
                text_year.Text = item.Ruler_Year;
            }

            return view;
        }

        private void ItemRulerClick(object sender, EventArgs args)
        {
            var element = (CustomLinearLayout)sender;
            var item = data.Where(u => u.Id_Ruler == element.Id_Item).First();

            var _Activity = (Activity)context;

            var activity = new Intent(_Activity, typeof(CategoryPageActivity));
            activity.PutExtra("_id", item.Id_Ruler);
            activity.PutExtra("name", item.Ruler + item.Ruler_Year);
            _Activity.StartActivity(activity);
        }
    }
}