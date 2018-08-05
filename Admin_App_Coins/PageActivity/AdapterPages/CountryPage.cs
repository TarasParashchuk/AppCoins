using Admin_App_Coins.Model;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Admin_App_Coins.AdapterPages
{
    class CountryPage : BaseAdapter
    {
        private List<Model_Country> data;

        private Context context;

        public CountryPage(Context context, List<Model_Country> data)
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
                view = LayoutInflater.From(context).Inflate(Resource.Layout.MainPage, null, false);

                var layout = view.FindViewById<LinearLayout>(Resource.Id.Layout_country_item);

                var image = view.FindViewById<ImageView>(Resource.Id.ImageCountry);
                //var imageBitmap = new ConvertImage().GetImageBitmapFromUrl(item.Icon);
                //image.SetImageBitmap(imageBitmap);

                var text = view.FindViewById<TextView>(Resource.Id.Text_Country);
                text.Text = item.Name;
            }

            return view;
        }
    }
}