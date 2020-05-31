using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository;
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

namespace DiplomFreelance.Models
{
    public static class BusinessLogic
    {
        public static Executor GetExecutorByUserId(string executorUserId)
        {
            var exec = new ExecutorRepository();
            return exec.GetByQuery($"Select * from Executor where Executor.ID_User = '{executorUserId}'").SingleOrDefault();
        }

        public static ICollection<Executor> GetExecutors(string query)
        {
            var executorRepository = new ExecutorRepository();
            return executorRepository.GetByQuery(query).ToList();
        }

        public static void LoadImg(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, ImageFormat.Jpeg);
                    byte[] imageBytes = m.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);

                    var client = new RestClient("https://api.imgur.com/3/image");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", "Client-ID 77c105146c35d79");
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("image", base64String);
                    IRestResponse response = client.Execute(request);
                }
            }
        }
    }
}