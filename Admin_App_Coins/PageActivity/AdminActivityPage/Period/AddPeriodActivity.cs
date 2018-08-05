using Admin_App_Coins.BaseClass;
using Admin_App_Coins.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Admin_App_Coins
{
    [Activity(Label = "Admin_App_Coins", MainLauncher = false)]
    public class AddPeriodActivity : BaseMethodPeriod
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPeriod);

            ActionBar.Title = "Создать новый период";

            Icon_Flag = FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageAddPeriodFlag);
            Icon_Flag.Click += (s, e) => { SelectImage(0); };

            Icon_Emblem = FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageAddPeriodEmblem);
            Icon_Emblem.Click += (s, e) => { SelectImage(1); };

            var text_Name = FindViewById<EditText>(Resource.Id.Name_Period);
            var text_Period = FindViewById<EditText>(Resource.Id.Period);

            var ButtonSave = FindViewById<Button>(Resource.Id.SaveAddPeriod);
            ButtonSave.Click += (s, e) =>
            {
                var progressDialog = ProgressDialog.Show(this, null, "Ожидайте обновления...", true);
                var isSendData = false;
                new Thread(new ThreadStart(delegate
                {
                    isSendData = GetJson(text_Name.Text, text_Period.Text, byte_icon_flag, byte_icon_emblem).Result;
                    RunOnUiThread(() => progressDialog.Hide());
                    RunOnUiThread(() => {
                        StartActivity(new Intent(this, typeof(PeriodPageActivity)));
                        Finish();
                    });
                })).Start();
                /*if (isSendData)
                {
                    
                }*/
            };

            var ButtonCancel = FindViewById<Button>(Resource.Id.CancelAddPeriod);
            ButtonCancel.Click += (s, e) =>
            {
                Finish();
            };
        }

        private async Task<bool> GetJson(string Name_Period, string Period, byte[] Icon_flag, byte[] Icon_emblem)
        {
            var new_item_period = new Model_Period();

            new_item_period.ID = 0;
            new_item_period.Name_Period = Name_Period;
            new_item_period.Period = Period;

            if (Icon_flag != null)
                new_item_period.Icon_flag = Convert.ToBase64String(Icon_flag);
            else new_item_period.Icon_flag = string.Empty;

            if (Icon_emblem != null)
                new_item_period.Icon_emblem = Convert.ToBase64String(Icon_emblem);
            else new_item_period.Icon_emblem = string.Empty;

            var str = JsonConvert.SerializeObject(new_item_period);
            var client = new HttpClient();
            var response = await client.PostAsync("http://185.86.78.146/PeriodMetod/CreatePeriod.php", new StringContent(str, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            return Convert.ToBoolean(responseString);
        }
    }
}