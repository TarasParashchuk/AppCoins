using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Admin_App_Coins.Model;
using System;
using System.Linq;
using Android.App;
using Admin_App_Coins.HelpFunctions;

namespace Admin_App_Coins
{
    public class ViewHolderCoins : Java.Lang.Object
    {
        internal CustomImageView image_coins;
        internal TextView text_price;
        internal TextView text_year;
        internal TextView text_name;
        internal CustomButton count_button;
    }

    class CoinsPage: BaseAdapter<Model_Coins>
    {
        private List<Model_Coins> data;
        private ViewHolderCoins viewHolder;
        private Context context;

        public CoinsPage(Context context, List<Model_Coins> data)
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

        public override Model_Coins this[int position]
        {
            get
            {
                return data[position];
            }
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
                view = LayoutInflater.From(context).Inflate(Resource.Layout.ItemListCoins, null, false);

                viewHolder = new ViewHolderCoins();

                viewHolder.image_coins = view.FindViewById<CustomImageView>(Resource.Id.ImageCoins);
                viewHolder.image_coins.Click += Image_CoinsClick;
                viewHolder.text_price = view.FindViewById<TextView>(Resource.Id.Text_Price);
                viewHolder.text_year = view.FindViewById<TextView>(Resource.Id.Text_Year);
                viewHolder.text_name = view.FindViewById<TextView>(Resource.Id.Text_Name);
                viewHolder.count_button = view.FindViewById<CustomButton>(Resource.Id.ButtonCount);
                viewHolder.count_button.Click += Count_CoinsClick;

                view.Tag = viewHolder;
            }
            else viewHolder = (ViewHolderCoins)view.Tag;

            //var imageBitmap = new ConvertImage().GetImageBitmapFromUrl(item.Icon);
            //viewHolder.image_coins.SetImageBitmap(imageBitmap);
            viewHolder.image_coins.Id_Item = item.Id_Coins;

            if (item.Information != "Нет")
                viewHolder.image_coins.SetBackgroundResource(Resource.Drawable.ImageBorderCoinsRed);
            else viewHolder.image_coins.SetBackgroundResource(Resource.Drawable.ImageBorderCoinsYellow);

            viewHolder.text_price.Text = "Цена: " + item.Price + " грн.";
            viewHolder.text_year.Text = item.Year.ToString();
            viewHolder.text_name.Text = item.Name;

            viewHolder.count_button.Id_Item = item.Id_Coins;
            viewHolder.count_button.Text = item.Count.ToString();

            return view;
        }

        private void Image_CoinsClick(object sender, EventArgs args)
        {
            /*var image = (CustomImageView)sender;
            var item = data.Where(u => u.Id_Coins == image.Id_Item).First();

            var Activity = (Activity)context;
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();

            var newFragment = DetailsCoinView.NewInstance(null, item);
            newFragment.Show(ft, "dialog");*/
        }

        private void Count_CoinsClick(object sender, EventArgs args)
        {
            /*var button = (CustomButton)sender;
            item = data.Where(u => u.Id_Coins == button.Id_Item).First();

            var Activity = (Activity)context;
            FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();

            var newFragment = CountChangeCoinView.NewInstance(null, item.Name + " " + item.Year, item.Count);
            newFragment.Show(ft, "dialog");
            newFragment.DialogClosed += (s, e) =>
            {
                item.Count = e.ReturnValue;
                PeriodPageActivity.database.SaveItem(item);
                button.Text = item.Count.ToString();
            };*/
        }
    }
}