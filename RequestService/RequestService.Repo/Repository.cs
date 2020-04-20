using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RequestService.Repo
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Repository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateRequest(string postCode)
        {
            Request request = new Request
            {
                PostCode = postCode,
                DateRequested = DateTime.Now,
                IsFulfillable = false,
            };
           _context.Request.Add(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }


        public async Task UpdateFulfillment(int requestId, bool isFulfillable)
        {
            var request = await _context.Request.FirstAsync(x => x.Id == requestId);
            if (request != null)
            {
                request.IsFulfillable = isFulfillable;
                await _context.SaveChangesAsync();
            }
        
        }
    }
}
