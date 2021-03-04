using System.Threading.Tasks;

namespace Sharepoint.Business.Interfaces
{
    public interface ITokenManager
    {
        Task<string> GetAccessTokenSPOAsync(string siteUrl);
    }
}