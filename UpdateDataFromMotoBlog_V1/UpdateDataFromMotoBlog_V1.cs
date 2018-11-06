using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Repository;

namespace UpdateDataFromMotoBlog_V1
{
    public static class UpdateDataFromMotoBlog_V1
    {
        [FunctionName("UpdateDataFromMotoBlog_V1")]
        public static void Run([TimerTrigger("*/1 * * * * *")]TimerInfo myTimer, TraceWriter log)
        {

            log.Info($"C# Timer trigger GetDataFromBlog function executed at: {DateTime.Now}");

            try
            {
                HttpClient bloggerClient = new HttpClient();

                bloggerClient.BaseAddress = new Uri("https://www.googleapis.com/blogger/v3/blogs/347878390938562057/posts");

                bloggerClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage bloggerResponse = bloggerClient.GetAsync("?maxResults=500&key=AIzaSyCRFYtA9LXbUwZh5vspUGLg2ahPd8Xdx2Q").Result;

                log.Info($"Google API response code: {bloggerResponse.StatusCode.ToString()}");

                if (bloggerResponse.IsSuccessStatusCode)
                {
                    var blogContent = JsonConvert.DeserializeObject<Blog>(bloggerResponse.Content.ReadAsStringAsync().Result);


                    //DB settings
                    var EndpointUrl = ConfigurationManager.AppSettings["MototouringdbUrl"];
                    var PrimaryKey = ConfigurationManager.AppSettings["MototouringdbPrimaryKey"];
                    var dbName = "MototouringDB";
                    var dbCollactionName = "MototouringDBCollection";


                    using (var unitOfWork = new UnitOfWork(EndpointUrl, dbName, dbCollactionName, PrimaryKey))
                    {
                        foreach (var blogPost in blogContent.Items)
                        {
                            unitOfWork.BlogRepository.Add(blogPost);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                log.Info(ex.StackTrace);
            }


            log.Info($"End of GetDataFromBlog function");
        }
    }
}
