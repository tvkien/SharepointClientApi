using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IFileClientApi
    {
        Task UploadFileAsync(UploadFileRequest request);

        Task CreateFolderAsync(CreateFolderRequest request);
    }
}
