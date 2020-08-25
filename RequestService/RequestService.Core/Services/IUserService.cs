using HelpMyStreet.Contracts.UserService.Response;
using HelpMyStreet.Utils.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IUserService
    {
        Task<int> GetChampionCountByPostcode(string postcode, CancellationToken cancellationToken);
        Task<GetChampionsByPostcodeResponse> GetChampionsByPostcode(string postcode, CancellationToken cancellationToken);
        Task<GetUserByIDResponse> GetUser(int userID, CancellationToken cancellationToken);
        Task<GetUsersResponse> GetUsers(CancellationToken cancellationToken);
        Task<GetVolunteersByPostcodeAndActivityResponse> GetHelpersByPostcodeAndTaskType(string postcode, List<SupportActivities> activities, CancellationToken cancellationToken);

    }

}
