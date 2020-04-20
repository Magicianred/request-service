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
    }
}
