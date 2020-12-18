using System.Threading.Tasks;

namespace SharepointClientApi.Grpah.Interfaces
{
    public interface ITokenManager
    {
        Task<string> AcquireTokenAsync();

        Task<string> AcquireTokenByUsernamePasswordAsync();
    }
}