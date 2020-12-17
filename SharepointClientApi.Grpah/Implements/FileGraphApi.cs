using Microsoft.Graph;
using SharepointClientApi.Grpah.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Implements
{
    public class FileGraphApi : IFileGraphApi
    {
        private readonly IGraphServiceClient graphServiceClient;

        public FileGraphApi(IGraphServiceClient graphServiceClient)
        {
            this.graphServiceClient = graphServiceClient;
        }

        public async Task UploadFileAsync(string siteUrl, string pathToFile)
        {
            var uriSite = new Uri(siteUrl);
            var siteCollection = await graphServiceClient.Sites.GetByPath(uriSite.AbsolutePath, uriSite.Host).Request().GetAsync();
            var drive = graphServiceClient.Sites[siteCollection.Id].Drive.Root;

            using var fileStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            var uploadedItem = await drive
              .ItemWithPath("Engagement Request File/AuvenirApis.docx")
              .Content
              .Request()
              .PutAsync<DriveItem>(fileStream);
        }

        public async Task UploadLargeFileAsync(string siteUrl, string pathToFile)
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
    }
}