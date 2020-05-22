using System.Threading;
using System.Threading.Tasks;
namespace RequestService.Core.Services
{
    public interface IDailyDigestService
    {
        Task GenerateEmailsAsync();
    }
}
