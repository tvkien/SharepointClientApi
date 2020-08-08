using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IListClientApi
    {
        Task CreateDocumentLibraryAsync(CreateDocumentLibraryRequest request);
    }
}