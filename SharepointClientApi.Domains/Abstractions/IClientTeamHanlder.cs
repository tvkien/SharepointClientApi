using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IClientTeamHanlder
    {
        Task HandleAsync(ClientTeamRequest request);

        Task ExternalSharingAsync(ClientTeamRequest request);
    }
}