using Sharepoint.Business.Requests;
using System.Threading.Tasks;

namespace Sharepoint.Business.Interfaces
{
    public interface ISiteCollectionManager
    {
        Task<string> CreateSiteCollectionAsync(SiteCollectionRequest request);

        Task<string> CreateSubSiteAsync(CreateSubSiteRequest request);

        Task<bool> DeleteSiteCollectionAsync(string siteUrl);
    }
}