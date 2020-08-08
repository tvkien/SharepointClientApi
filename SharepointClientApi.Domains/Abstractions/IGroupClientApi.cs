using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IGroupClientApi
    {
        Task AddGroupAsync(AddGroupRequest request);
    }
}