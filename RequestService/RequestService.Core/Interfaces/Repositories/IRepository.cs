using RequestService.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestService.Core.Interfaces.Repositories
{
    public interface IRepository
    {
        Task<int> CreateRequest(string postCode);
        Task UpdateFulfillment(int requestId, bool isFulfillable);
    }
}
