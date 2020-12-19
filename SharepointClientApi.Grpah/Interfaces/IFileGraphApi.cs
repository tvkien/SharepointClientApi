using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Interfaces
{
    public interface IFileGraphApi
    {
        Task CreateFolderAsync(string siteUrl, string folderName);

        Task UploadFileAsync(string siteUrl, string pathToFile);

        Task UploadLargeFileAsync(string siteUrl, string pathToFile);
    }
}