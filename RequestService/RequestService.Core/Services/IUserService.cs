using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using RequestService.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public interface IUserService
    {
        Task<int> GetChampionCountByPostcode(string postcode, CancellationToken cancellationToken);
        Task<GetChampionsByPostcodeResponse> GetChampionsByPostcode(string postcode, CancellationToken cancellationToken);
        Task<GetUserByIDResponse> GetUser(int userID, CancellationToken cancellationToken);
        Task<GetHelpersByPostcodeAndTaskTypeResponse> GetHelpersByPostcodeAndTaskType(string postcode, List<SupportActivities> activities, CancellationToken cancellationToken);

    }

}
