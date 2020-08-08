using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IClientTeamHanlder
    {
        Task Handle(ClientTeamRequest request);
    }
}