using Admin_App_Coins.BaseClass;
using Admin_App_Coins.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Admin_App_Coins
{
    [Activity(Label = "Admin_App_Coins", MainLauncher = false)]
    class UpdatePeriodActivity : BaseMethodPeriod
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPeriod);

            ActionBar.Title = "Редактировать период";

            var item = JsonConvert.DeserializeObject<Model_Period>(Intent.GetStringExtra("item"));

            Icon_Flag = FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageAddPeriodFlag);
            ImageService.Instance.LoadUrl(item.Icon_flag).Transform(new List<ITransformation>() { new CircleTransformation(2, "#FFB300") }).Into(Icon_Flag);
            Icon_Flag.Click += (s, e) => { SelectImage(0); };

            Icon_Emblem = FindViewById<FFImageLoading.Views.ImageViewAsync>(Resource.Id.ImageAddPeriodEmblem);
            ImageService.Instance.LoadUrl(item.Icon_emblem).Transform(new List<ITransformation>() { new CircleTransformation(2, "#FFB300") }).Into(Icon_Emblem);
            Icon_Emblem.Click += (s, e) => { SelectImage(1); };

            var text_Name = FindViewById<EditText>(Resource.Id.Name_Period);
            var text_Period = FindViewById<EditText>(Resource.Id.Period);
            var ButtonSave = FindViewById<Button>(Resource.Id.SaveAddPeriod);
            var ButtonCancel = FindViewById<Button>(Resource.Id.CancelAddPeriod);

            text_Name.Text = item.Name_Period;
            text_Period.Text = item.Period;

            ButtonSave.Click += (s,e) => {
                GetJson(text_Name.Text, text_Period.Text, item.ID.ToString());
                StartActivity(new Intent(this, typeof(PeriodPageActivity)));
                Finish();
            };

            ButtonCancel.Click += (s, e) => {
                Finish();
            };
        }

        private async void GetJson(string Name_Period, string Period, string Id)
        {
            /*var client = new HttpClient();
            var Icon_flagContent = new ByteArrayContent(Icon_flag);
            var Icon_emblemContent = new ByteArrayContent(Icon_emblem);
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(Name_Period), "Name_Period");
                formData.Add(new StringContent(Period), "Period");
                formData.Add(Icon_flagContent, "Iconf", Guid.NewGuid() + ".png");
                formData.Add(Icon_emblemContent, "Icone", Guid.NewGuid() + ".png");

                var response = await client.PostAsync("http://185.86.78.146/PeriodMetod/CreatePeriod.php", formData);
                var responseString = await response.Content.ReadAsStreamAsync();
            }
            */
            var client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "Name_Period", Name_Period },
                { "Period", Period },
                { "ID", Id }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://185.86.78.146/PeriodMetod/UpdatePeriod.php", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}