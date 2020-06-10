using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public static class ImageService
    {
        public static string LoadImg(Image image)
        {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, ImageFormat.Png);
                    byte[] imageBytes = m.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);

                    var client = new RestClient("https://api.imgur.com/3/image");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", "Client-ID 77c105146c35d79");
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("image", base64String);
                    IRestResponse response = client.Execute(request);
                    var imgURL = JObject.Parse(response.Content).SelectToken("data.link").ToObject<String>();

                    return imgURL;
                }
            
        }
    }
}