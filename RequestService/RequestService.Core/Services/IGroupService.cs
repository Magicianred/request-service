using HelpMyStreet.Contracts.GroupService.Request;
using HelpMyStreet.Contracts.GroupService.Response;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IGroupService
    {
        Task<GetNewRequestActionsResponse> GetNewRequestActions(GetNewRequestActionsRequest request, CancellationToken cancellationToken);
        Task<GetUserGroupsResponse> GetUserGroups(int userId, CancellationToken cancellationToken);
        Task<GetGroupMembersResponse> GetGroupMembers(int groupID);
    }

}
