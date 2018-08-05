using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using Admin_App_Coins.Model;
using System.Linq;
using Android.App;
using Newtonsoft.Json;
using FFImageLoading;

namespace Admin_App_Coins.AdapterPages
{
    public class ViewHolderPeriod : Java.Lang.Object
    {
        internal RelativeLayout layout;
        internal FFImageLoading.Views.ImageViewAsync icon_flag;
        internal FFImageLoading.Views.ImageViewAsync icon_emblem;
        internal TextView text_year;
        internal TextView text_name_period;
        internal CheckBox checkbox_period;
    }

    class PeriodPage: BaseAdapter<Model_Period>
    {
        private Context context;
        private List<Model_Period> data;
        private ViewHolderPeriod viewHolder;
        public static bool[] CheckedState;
        public static List<int> ArrayIndexItemDelete = new List<int>();

        public bool flagItemPeriodClick;
        public int isCheckBoxVisible = 0;

        public PeriodPage(Context context, List<Model_Period> data)
        {
            this.data = data;
            this.context = context;
            CheckedState = new bool[data.Count];
        }

        public override int Count
        {
            get
            {
                return data.Count;
            }
        }

        public override Model_Period this[int position]
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
                view = LayoutInflater.From(context).Inflate(Resource.Layout.ItemPeriodPages, null, false);

                viewHolder = new ViewHolderPeriod ();

                viewHolder.layout = view.FindViewById<RelativeLayout>(Resource.Id.Layout_period_item);
                viewHolder.layout.Click += ItemPeriodClick;

                viewHolder.icon_flag = view.FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageFlag);
                viewHolder.icon_emblem = view.FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageEmblem);
                viewHolder.text_name_period = view.FindViewById<TextView>(Resource.Id.Text_Name_Period);
                viewHolder.text_year = view.FindViewById<TextView>(Resource.Id.Text_Period_Year);
                viewHolder.checkbox_period = view.FindViewById<CheckBox>(Resource.Id.CheckBoxPeriod);

                view.Tag = viewHolder;
            }
            else viewHolder = (ViewHolderPeriod)view.Tag;

            viewHolder.layout.Id = item.ID;
            ImageService.Instance.LoadUrl(item.Icon_flag).WithCache(FFImageLoading.Cache.CacheType.Memory).Into(viewHolder.icon_flag);
            ImageService.Instance.LoadUrl(item.Icon_emblem).WithCache(FFImageLoading.Cache.CacheType.Memory).Into(viewHolder.icon_emblem);
            viewHolder.text_name_period.Text = item.Name_Period;
            viewHolder.text_year.Text = item.Period;

            if (isCheckBoxVisible == 1)
            {
                viewHolder.checkbox_period.Visibility = ViewStates.Visible;
                var imgViewParams = new RelativeLayout.LayoutParams(viewHolder.icon_emblem.Width, viewHolder.icon_emblem.Height);
                imgViewParams.AddRule(LayoutRules.AlignParentRight);
                imgViewParams.RightMargin = 75;
                viewHolder.icon_emblem.LayoutParameters = imgViewParams;
            }
            else if (isCheckBoxVisible == 2)
            {
                viewHolder.checkbox_period.Visibility = ViewStates.Gone;
                RelativeLayout.LayoutParams imgViewParams = new RelativeLayout.LayoutParams(viewHolder.icon_emblem.Width, viewHolder.icon_emblem.Height);
                imgViewParams.AddRule(LayoutRules.AlignParentRight);
                imgViewParams.RightMargin = 0;
                viewHolder.icon_emblem.LayoutParameters = imgViewParams;

                Array.Clear(CheckedState, 0, CheckedState.Length);
            }
            else viewHolder.checkbox_period.Visibility = ViewStates.Gone;

            viewHolder.checkbox_period.Id = item.ID;
            viewHolder.checkbox_period.Tag = position;
            viewHolder.checkbox_period.SetOnCheckedChangeListener(null);
            viewHolder.checkbox_period.Checked = CheckedState[position];
            viewHolder.checkbox_period.SetOnCheckedChangeListener(new CheckedChangeListener((Activity)context));

            return view;
        }

        private void ItemPeriodClick(object sender, EventArgs args)
        {
            Intent activity;
            var _Activity = (Activity)context;
            var element = (RelativeLayout)sender;
            var item = data.Where(u => u.ID == element.Id).First();

            if (flagItemPeriodClick)
            {
                activity = new Intent(_Activity, typeof(UpdatePeriodActivity));
                activity.PutExtra("item", JsonConvert.SerializeObject(item));
            }
            else
            {
                activity = new Intent(_Activity, typeof(RulesPageActivity));
                activity.PutExtra("_id", item.ID);
                activity.PutExtra("name", item.Name_Period);
            }
            _Activity.StartActivity(activity);
        }

        public class CheckedChangeListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
        {
            private Activity activity;
            

            public CheckedChangeListener(Activity activity)
            {
                this.activity = activity;
            }

            public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
            {
                var position = (int)buttonView.Tag;
                if (isChecked)
                {
                    var indexcheck = buttonView.Id;
                    ArrayIndexItemDelete.Add(indexcheck);
                    CheckedState[position] = true;
                }
                else
                {
                    var indexcheck = buttonView.Id;
                    var index = ArrayIndexItemDelete.IndexOf(indexcheck);
                    ArrayIndexItemDelete.RemoveAt(index);
                    CheckedState[position] = false;
                }
            }
        }
    }
}