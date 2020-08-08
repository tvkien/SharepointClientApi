using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface ISiteCreation
    {
        Task<string> CreateCommunicationSiteAsync(CommunicationSiteRequest request);

        Task<string> CreateTeamSiteNoGroupAsync(TeamSiteNoGroupRequest request);

        Task<string> CreateTeamSiteCollectionAsync(TeamSiteCollectionRequest request);
    }
}