using System;
using System.Collections.Generic;
using System.Net.Http;
using Admin_App_Coins.Model;
using Newtonsoft.Json;

namespace Admin_App_Coins.HelpFunctions
{
    class GetDataMySQL
    {
        private string content = string.Empty;
        private string post_name = string.Empty;
        private Uri uri;
        private string id = string.Empty;

        public GetDataMySQL (Uri uri, string post_name, string id)
        {
            this.post_name = post_name;
            this.uri = uri;
            this.id = id;

            GetJson();
        }

        private async void GetJson()
        {
            using (var client = new HttpClient())
            {
                if (id != null && post_name != null)
                {
                    var parameters = new Dictionary<string, string>();
                    parameters[post_name] = id;
                    var response = client.PostAsync(uri, new FormUrlEncodedContent(parameters)).Result;
                    content = await response.Content.ReadAsStringAsync();
                }
                else content = client.GetStringAsync(uri).Result;
            }    
        }

        public List<Model_Period> GetDataPeriod()
        {
            return JsonConvert.DeserializeObject<List<Model_Period>>(content);
        }

        public List<Model_Rules> GetDataRules()
        {
            return JsonConvert.DeserializeObject<List<Model_Rules>>(content);
        }

        public List<Model_Category> GetDataCategory()
        {
            return JsonConvert.DeserializeObject<List<Model_Category>>(content);
        }

        public List<Model_Coins> GetDataCoins()
        {
            return JsonConvert.DeserializeObject<List<Model_Coins>>(content);
        }
    }
}