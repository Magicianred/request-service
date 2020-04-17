using RequestService.Core.Domains.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Handlers
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequestRequest>
    {
        public Task<Unit> Handle(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
