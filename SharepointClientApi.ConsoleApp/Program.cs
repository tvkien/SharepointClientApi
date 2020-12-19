using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
//using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharepointClientApi.Grpah;
using SharepointClientApi.Grpah.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SharepointClientApi.ConsoleApp
{
    class Program
    {
        private static HttpClient httpClient = new HttpClient();
        private static IGraphServiceClient graphServiceClient = null;

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start");
            var site = @"https://titanpod2.sharepoint.com/sites/AuvenirDev__EnvironmentPrefix__-03669893-fb4b-48a7-ab8e-772388635432/23c5dd6f-1467-4de0-9ee1-18e80fc32acb";
            //var uriSite = new Uri(site);

            //var token = await GetAccessTokenKienAsync();

            try
            {
                var serviceProvider = BuildServiceProvider();
                var fileGraphApi = serviceProvider.GetRequiredService<IFileGraphApi>();
                await fileGraphApi.CreateFolderAsync(site, "New Folder Name Test");

                //await fileGraphApi.UploadLargeFileAsync(site, @"D:\QLBH 2020 - Kien.xls");

                //graphServiceClient = new GraphServiceClient(
                //               new DelegateAuthenticationProvider(
                //                   async (requestMessage) =>
                //                   {
                //                       requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //                   }));

                //await UploadLargeFileAsync(site, @"D:\QLBH 2020 - Kien.xls");
            }
            catch (Exception ex)
            {

            }

            //var drive = graphServiceClient.Sites[siteCollection.Id].Drive.Root;

            //var pathToFile = @"C:\Users\kien.truong\Desktop\AuvenirApis.docx";
            //using (var fileStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read))
            //{
            //    var uploadedItem = await drive
            //      .ItemWithPath("Engagement Request File/AuvenirApis.docx")
            //      .Content
            //      .Request()
            //      .PutAsync<DriveItem>(fileStream);
            //}


            Console.WriteLine("Done............");

            Console.ReadKey();
        }

        private static async Task<string> GetAccessTokenKienAsync()
        {
            try
            {
                var requestData = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("client_id", "192cdaaa-9063-40c5-99a9-b29c154b1716"),
                    new KeyValuePair<string, string>("client_secret", "IEa_.ec_r8_VyFMdG55615.5.qy3l..NJF"),
                    new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "admin@titanpod2.onmicrosoft.com"),
                    new KeyValuePair<string, string>("password", "TitanCorpVn@1234")
                };

                var httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://login.microsoftonline.com/31d2192e-fd57-4e9d-9fe4-6524828399c5/oauth2/v2.0/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(requestData)
                };

                var response = await httpClient.SendAsync(httpRequestMessage);
                var dataResponse = await response.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(dataResponse);
                return jsonData.SelectToken("access_token")?.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static async Task<string> GetAccessTokenAsync()
        {
            try
            {
                var requestData = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("client_id", "e11bb7e9-009a-40e8-9f29-fd922df7d352"),
                    new KeyValuePair<string, string>("client_secret", "t-3u63iwz3ekcG3pk.jbSdE6~2_YyrY_4b"),
                    new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "ca_sc_caauvdevspocan@dmchosting.ca"),
                    new KeyValuePair<string, string>("password", "1cXw9k4rgg4sbx!#")
                };

                var httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://login.microsoftonline.com/1ddae48a-90e5-468a-9ac8-244d5b76edf5/oauth2/v2.0/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(requestData)
                };

                var response = await httpClient.SendAsync(httpRequestMessage);
                var dataResponse = await response.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<JObject>(dataResponse);
                return jsonData.SelectToken("access_token")?.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task UploadLargeFileAsync(string siteUrl, string pathToFile)
        {
            var uriSite = new Uri(siteUrl);
            var siteCollection = await graphServiceClient.Sites.GetByPath(uriSite.AbsolutePath, uriSite.Host).Request().GetAsync();
            var drive = graphServiceClient.Sites[siteCollection.Id].Drive.Root;

            using var fileStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            var uploadProps = new DriveItemUploadableProperties
            {
                ODataType = null,
                AdditionalData = new Dictionary<string, object>
                {
                    { "@microsoft.graph.conflictBehavior", "replace" }
                }
            };

            var uploadSession = await drive
              .ItemWithPath("Engagement Request File/AuvenirApis.docx")
              .CreateUploadSession(uploadProps)
              .Request()
              .PostAsync();
            int maxSliceSize = 320 * 1024;
            var fileUploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, fileStream, maxSliceSize);
            IProgress<long> progress = new Progress<long>(progress => {
                Console.WriteLine($"Uploaded {progress} bytes of {fileStream.Length} bytes");
            });

            try
            {
                var uploadResult = await fileUploadTask.UploadAsync(progress);

                if (uploadResult.UploadSucceeded)
                {
                    Console.WriteLine($"Upload complete, item ID: {uploadResult.ItemResponse.Id}");
                }
                else
                {
                    Console.WriteLine("Upload failed");
                }
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error uploading: {ex.ToString()}");
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            var services = new ServiceCollection();
            services.AddAzureInfrastructure(configuration);
            return services.BuildServiceProvider();
        }
    }
}
