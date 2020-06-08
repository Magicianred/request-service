using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using System.Linq;

namespace RequestService.Handlers
{
    public class GetQuestionsByActivityHandler : IRequestHandler<GetQuestionsByActivitiesRequest, GetQuestionsByActivtiesResponse>
    {
        private readonly IRepository _repository;                
        public GetQuestionsByActivityHandler(
            IRepository repository)
        {
            _repository = repository;                        
        }

        public async Task<GetQuestionsByActivtiesResponse> Handle(GetQuestionsByActivitiesRequest request, CancellationToken cancellationToken)
        {            
            var questions = await _repository.GetActivityQuestions(request.ActivitesRequest.Activities, cancellationToken);

            GetQuestionsByActivtiesResponse response = new GetQuestionsByActivtiesResponse();
            response.SupportActivityQuestions = new Dictionary<HelpMyStreet.Utils.Enums.SupportActivities, List<Question>>();

            questions.ForEach(x =>
            {
                if (x.Questions.Count() == 0)
                {
                    response.SupportActivityQuestions.Add(x.Activity, new List<Question>());
                }
                else
                {
                    response.SupportActivityQuestions.Add(x.Activity, x.Questions);
                }
            });

            return response;
        }
    }
}
