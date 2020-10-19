using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEj;
using Newtonsoft.Json;
using RestSharp;

namespace DALj
{
    public class ImageAnalysis
    {

        public void GetTags(ImageDetails CurrentImage)
        {
            string apiKey = "acc_b09cc5e006e375c";
            string apiSecret = "bcb54eebdf379e4178c3c17eaae89b9f";
            string image = CurrentImage.ImagePath;

            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/tags");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));
            request.AddFile("image", image);

            IRestResponse response = client.Execute(request);
            Root TagList = JsonConvert.DeserializeObject<Root>(response.Content);
            foreach (var item in TagList.result.tags)
            {
                CurrentImage.Details.Add(item.tag.en, item.confidence);
            }

        }
    }
}




