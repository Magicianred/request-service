using RequestService.Core.Interfaces.Repositories;
using System;

namespace RequestService.Core.Services
{
    public class ArchiveService : IArchiveService
    {
        private readonly IRepository _repository;
        public ArchiveService(IRepository repository)
        {
            _repository = repository;
        }

        public void ArchiveOldRequests(int daysSinceJobRequested, int daysSinceJobStatusChanged)
        {
            _repository.ArchiveOldRequests(daysSinceJobRequested, daysSinceJobStatusChanged);
        }
    }
}
