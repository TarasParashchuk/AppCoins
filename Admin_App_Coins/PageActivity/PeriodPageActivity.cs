using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Admin_App_Coins.AdapterPages;
using Admin_App_Coins.HelpFunctions;
using Admin_App_Coins.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Admin_App_Coins
{
    [Activity(Label = "Admin_App_Coins", MainLauncher = true)]
    public class PeriodPageActivity : Activity
    {
        private Toolbar editToolbar;
        private PeriodPage PeriodPageAdapter;
        private bool flag = true;
        ListView _ListView;
        List<Model_Period> list;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PeriodPage);

            editToolbar = FindViewById<Toolbar>(Resource.Id.toolbar_edit_toolbar);
            editToolbar.Title = "Редактирование";
            editToolbar.InflateMenu(Resource.Menu.edit_toolbar);
            editToolbar.MenuItemClick += SelectEditItem;

            ActionBar.Title = "Периоды";
            _ListView = FindViewById<ListView>(Resource.Id._listView);

            var uri = new Uri("http://185.86.78.146/PeriodMetod/SelectDataPeriod.php");
            list = new GetDataMySQL(uri, null, null).GetDataPeriod();
            PeriodPageAdapter = new PeriodPage(this, list);
            _ListView.Adapter = PeriodPageAdapter;
            _ListView.ChoiceMode = ChoiceMode.Multiple;
        }

        private void SelectEditItem(object sender, Toolbar.MenuItemClickEventArgs e)
        {
            var id = e.Item.ItemId;
            if (id == Resource.Id.menu_edit)
            {
                if (flag)
                {
                    PeriodPageAdapter.isCheckBoxVisible = 1;
                    PeriodPageAdapter.NotifyDataSetChanged();
                    flag = false;
                }
                else
                {
                    PeriodPageAdapter.isCheckBoxVisible = 2;
                    PeriodPageAdapter.NotifyDataSetChanged();
                    flag = true;
                }
            }
            else if (id == Resource.Id.menu_add)
            {
                var activity = new Intent(this, typeof(AddPeriodActivity));
                StartActivity(activity);
            }
            else if (id == Resource.Id.menu_delete)
            {
                GetJson();

                var count = 0;
                for (var i = 0; i < PeriodPage.CheckedState.Length; i++)
                {
                    if (PeriodPage.CheckedState[i])
                    {
                        list.RemoveAt(i - count);
                        PeriodPage.CheckedState[i] = false;
                        count++;
                    }
                }

                //Array.Clear(PeriodPage.CheckedState, 0, PeriodPage.CheckedState.Length);

                PeriodPageAdapter.NotifyDataSetChanged();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_toolbar, menu);
            var Switch = (Switch)menu.FindItem(Resource.Id.switch_edit).ActionView;
            Switch.Checked = PeriodPageAdapter.flagItemPeriodClick;
            Switch.CheckedChange += _switch_CheckedChange;

            if (PeriodPageAdapter.flagItemPeriodClick)
                editToolbar.Visibility = ViewStates.Visible;
            else editToolbar.Visibility = ViewStates.Gone;

            return base.OnCreateOptionsMenu(menu);
        }

        private void _switch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            PeriodPageAdapter.flagItemPeriodClick = e.IsChecked;
            if (e.IsChecked)
                editToolbar.Visibility = ViewStates.Visible;
            else editToolbar.Visibility = ViewStates.Gone;
        }

        private /*async*/ void GetJson()
        {
            var str = JsonConvert.SerializeObject(PeriodPage.ArrayIndexItemDelete);
            var client = new HttpClient();
            var response = /*await*/ client.PostAsync("http://185.86.78.146/PeriodMetod/DeletePeriod.php", new StringContent(str, Encoding.UTF8, "application/json")).Result;
            var responseString = /*await*/ response.Content.ReadAsStreamAsync().Result;
            //var contents = response.Content.ReadAsStringAsync().Result;
        }
    }
}

