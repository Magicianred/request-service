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
            if(request.ActivitesRequest.Activities.Count!=1)
            {
                throw new System.Exception("Expecting only one activity");
            }
            
            var selectedActivity = request.ActivitesRequest.Activities.First();

            List<Question> questions = await _repository.GetQuestionsForActivity(selectedActivity, request.RequestHelpFormVariantRequest.RequestHelpFormVariant, request.RequestHelpFormStageRequest.RequestHelpFormStage, cancellationToken);

            GetQuestionsByActivtiesResponse response = new GetQuestionsByActivtiesResponse();

            Dictionary<HelpMyStreet.Utils.Enums.SupportActivities, List<Question>> dict = new Dictionary<HelpMyStreet.Utils.Enums.SupportActivities, List<Question>>
            {
                { selectedActivity, questions }
            };

            response.SupportActivityQuestions = dict;

            return response;
        }
    }
}
