using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Interfaces
{
    public interface IFileGraphApi
    {
        Task UploadFileAsync(string siteUrl, string pathToFile);

        Task UploadLargeFileAsync(string siteUrl, string pathToFile);
    }
}