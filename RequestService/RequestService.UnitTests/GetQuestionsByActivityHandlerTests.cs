using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.RequestService.Response;
using HelpMyStreet.Utils.Models;
using Moq;
using NUnit.Framework;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class GetQuestionsByActivityHandlerTests
    {
        private Mock<IRepository> _repository;
        private GetQuestionsByActivityHandler _classUnderTest;
        private GetQuestionsByActivitiesRequest _request;
        private List<ActivityQuestionDTO> _response;
        private List<Question> _questions;

        [SetUp]
        public void Setup()
        {
            SetupRepository();
            _classUnderTest = new GetQuestionsByActivityHandler(_repository.Object);
            _request = new GetQuestionsByActivitiesRequest()
            {

                ActivitesRequest = new ActivitesRequest
                {
                    Activities = new List<HelpMyStreet.Utils.Enums.SupportActivities>
                    {
                        HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn
                    }
                },
                RequestHelpFormVariantRequest = new RequestHelpFormVariantRequest()
                {
                    RequestHelpFormVariant = HelpMyStreet.Utils.Enums.RequestHelpFormVariant.Default
                },
                RequestHelpFormStageRequest = new RequestHelpFormStageRequest()
                {
                    RequestHelpFormStage = HelpMyStreet.Utils.Enums.RequestHelpFormStage.Request
                }
            };
            _questions = new List<Question>();

            _response = new List<ActivityQuestionDTO>
            {
                new ActivityQuestionDTO
                {
                    Activity = HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn,
                    Questions = new List<Question>{ new Question { Id = 1 } }
                },
                new ActivityQuestionDTO
                {
                    Activity = HelpMyStreet.Utils.Enums.SupportActivities.CollectingPrescriptions,
                    Questions = new List<Question> { new Question { Id = 2 } }
                },
                new ActivityQuestionDTO
                {
                    Activity = HelpMyStreet.Utils.Enums.SupportActivities.FaceMask,
                    Questions = new List<Question>{ new Question { Id = 1 } }
                },
                new ActivityQuestionDTO
                {
                    Activity = HelpMyStreet.Utils.Enums.SupportActivities.MealPreparation,
                    Questions = new List<Question> { new Question { Id = 2 } }
                },
            };
        }

        private void SetupRepository()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.GetQuestionsForActivity(It.IsAny<HelpMyStreet.Utils.Enums.SupportActivities>(), It.IsAny<HelpMyStreet.Utils.Enums.RequestHelpFormVariant>(), It.IsAny<HelpMyStreet.Utils.Enums.RequestHelpFormStage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(()=> _questions);
        }

        [Test]
        public async Task WhenIGetActivites_IMapCorrectly_FromRepsository()
        {

            var response = await _classUnderTest.Handle(_request, new CancellationToken());

            Assert.AreEqual(_request.ActivitesRequest.Activities.Count, response.SupportActivityQuestions.Count);
            foreach(var activity in _request.ActivitesRequest.Activities)
            {
                Assert.IsTrue(response.SupportActivityQuestions.Where(x => x.Key == activity).Count() == 1);
            }

            _repository.Verify(X => X.GetQuestionsForActivity(_request.ActivitesRequest.Activities.First(), _request.RequestHelpFormVariantRequest.RequestHelpFormVariant, _request.RequestHelpFormStageRequest.RequestHelpFormStage, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenIGetActivites_ThatAreNotSetupInDB_IGetNoQuestionsBack()
        {
            _request.ActivitesRequest.Activities = new List<HelpMyStreet.Utils.Enums.SupportActivities>();
            _response.Add(new ActivityQuestionDTO { Activity = HelpMyStreet.Utils.Enums.SupportActivities.Other, Questions = new List<HelpMyStreet.Utils.Models.Question>() });
            
            _request.ActivitesRequest.Activities.Add(HelpMyStreet.Utils.Enums.SupportActivities.Other);
            var response = await _classUnderTest.Handle(_request, new CancellationToken());
            
            var otherActivity = response.SupportActivityQuestions.Where(x => x.Key == HelpMyStreet.Utils.Enums.SupportActivities.Other).FirstOrDefault();
            Assert.NotNull(otherActivity);
            Assert.AreEqual(0, otherActivity.Value.Count());
        }

    }
}