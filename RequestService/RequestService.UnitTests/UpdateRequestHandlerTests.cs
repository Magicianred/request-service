using HelpMyStreet.Contracts.CommunicationService.Request;
using HelpMyStreet.Contracts.RequestService.Request;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RequestService.Core.Config;
using RequestService.Core.Dto;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Services;
using RequestService.Handlers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.UnitTests
{
    public class UpdateHandlerRequestTests
    {
        private Mock<IRepository> _repository;
        private Mock<IUserService> _userService;
        private Mock<ICommunicationService> _communicationService;
        private UpdateRequestRequest _request;
        private Mock<IOptionsSnapshot<ApplicationConfig>> _applicationConfig;
        private ApplicationConfig _applicationConfigSettings;
        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository>();
            _repository.Setup(x => x.GetRequestPostCodeAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync("TESTING");
            _userService = new Mock<IUserService>();
            
            _communicationService = new Mock<ICommunicationService>();
            _userService.Setup(x => x.GetChampionsByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetChampionsByPostcodeResponse
            {
                Users = new List<HelpMyStreet.Utils.Models.User>
                {
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 1,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "John",
                            LastName = "Dobbings"
                        }
                    }
                }
            });
            _request = new UpdateRequestRequest
            {
                RequestID = 1,
                FurtherDetails = "Further Details",
                HealthOrWellbeingConcern = true,
                OnBehalfOfAnother = true,
                RequestorEmailAddress = "test@gmail.com",
                RequestorFirstName = "Joe",
                RequestorLastName = "Bloggs",
                RequestorPhoneNumber = "07592124574",
                SupportActivitiesRequired = new SupportActivityRequest
                {
                    SupportActivities = new System.Collections.Generic.List<HelpMyStreet.Utils.Enums.SupportActivities> {
                        HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn,
                        HelpMyStreet.Utils.Enums.SupportActivities.DogWalking
                    }
                }
            };
            _applicationConfigSettings = new ApplicationConfig
            {
                ManualReferEmail = "test",
                ManualReferName = "Test",
            };

            _applicationConfig = new Mock<IOptionsSnapshot<ApplicationConfig>>();
            _applicationConfig.SetupGet(x => x.Value).Returns(_applicationConfigSettings);
        }

        [Test]
        public async Task WhenICall_UpdateRequestHandler_ICallUpdatePersonalDetails_WithCorrectDTO()
        {                       
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object,  _userService.Object, _applicationConfig.Object);
            var response = await handler.Handle(_request, new CancellationToken());

            PersonalDetailsDto expectedDto = new PersonalDetailsDto
            {
                RequestID = _request.RequestID,
                RequestorEmailAddress = _request.RequestorEmailAddress,
                RequestorFirstName = _request.RequestorFirstName,
                RequestorLastName = _request.RequestorLastName,
                RequestorPhoneNumber = _request.RequestorPhoneNumber,
                FurtherDetails = _request.FurtherDetails,
                OnBehalfOfAnother = _request.OnBehalfOfAnother,
                HealthOrWellbeingConcern = _request.HealthOrWellbeingConcern
            };
            _repository.Verify(x => x.UpdatePersonalDetailsAsync(It.Is<PersonalDetailsDto>(arg =>
                arg.RequestID == expectedDto.RequestID &&
                arg.RequestorEmailAddress == expectedDto.RequestorEmailAddress &&
                arg.RequestorFirstName == expectedDto.RequestorFirstName &&
                arg.RequestorLastName == expectedDto.RequestorLastName &&
                arg.RequestorPhoneNumber == expectedDto.RequestorPhoneNumber &&
                arg.FurtherDetails == expectedDto.FurtherDetails &&
                arg.OnBehalfOfAnother == expectedDto.OnBehalfOfAnother &&
                arg.HealthOrWellbeingConcern == expectedDto.HealthOrWellbeingConcern 
            ), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task WhenICall_UpdateRequestHandler_ICallAddSupportActivity_WithCorrectDTO()
        {
            _userService.Setup(x => x.GetChampionCountByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            var response = await handler.Handle(_request, new CancellationToken());

            SupportActivityDTO expectedDto = new SupportActivityDTO
            {
                RequestID = _request.RequestID,
                SupportActivities = _request.SupportActivitiesRequired.SupportActivities
               
            };
            _repository.Verify(x => x.AddSupportActivityAsync(It.Is<SupportActivityDTO>(arg =>
                arg.RequestID == expectedDto.RequestID &&
                arg.SupportActivities == expectedDto.SupportActivities)                
            , new CancellationToken()));
        }

        [Test]
        public async Task WhenICall_UpdateRequestHandler_WithOneChampionMatch_IGetNoCC()
        {
            _userService.Setup(x => x.GetChampionsByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetChampionsByPostcodeResponse
            {
                Users = new List<HelpMyStreet.Utils.Models.User>
                {
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 1,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "John",
                            LastName = "Dobbings"
                        }
                    }
                }
            });
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            await handler.Handle(_request, new CancellationToken());
             _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(r =>  r.Recipients.ToUserIDs.Count == 1 && r.Recipients.CCUserIDs.Count == 0), It.IsAny<CancellationToken>()));            
        }

        [Test]
        public async Task WhenICall_UpdateRequestHandler_WithTwoChampionMatch_IGetOneCC()
        {
            _userService.Setup(x => x.GetChampionsByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetChampionsByPostcodeResponse
            {
                Users = new List<HelpMyStreet.Utils.Models.User>
                {
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 1,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "John",
                            LastName = "Dobbings"
                        }
                    },
                         new HelpMyStreet.Utils.Models.User
                    {
                        ID = 2,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "Mary",
                            LastName = "Pobbings"
                        }
                    }
                }
            });
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            await handler.Handle(_request, new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(r => r.Recipients.ToUserIDs.Count == 1 && r.Recipients.CCUserIDs.Count == 1), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task WhenICall_UpdateRequestHandler_WithThreeChampionMatch_IGetTwoCC()
        {
            _userService.Setup(x => x.GetChampionsByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetChampionsByPostcodeResponse
            {
                Users = new List<HelpMyStreet.Utils.Models.User>
                {
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 1,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "John",
                            LastName = "Dobbings"
                        }
                    },
                         new HelpMyStreet.Utils.Models.User
                    {
                        ID = 2,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "Mary",
                            LastName = "Pobbings"
                        }
                    },
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 3,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "Steve",
                            LastName = "Sherry"
                        }
                    }
                }
            });
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            await handler.Handle(_request, new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(r => r.Recipients.ToUserIDs.Count == 1 && r.Recipients.CCUserIDs.Count == 2), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task WhenICall_UpdateRequestHandler_WithFourChampionMatch_IGetTwoCC()
        {
            _userService.Setup(x => x.GetChampionsByPostcode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetChampionsByPostcodeResponse
            {
                Users = new List<HelpMyStreet.Utils.Models.User>
                {
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 1,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "John",
                            LastName = "Dobbings"
                        }
                    },
                         new HelpMyStreet.Utils.Models.User
                    {
                        ID = 2,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "Mary",
                            LastName = "Pobbings"
                        }
                    },
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 3,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "Steve",
                            LastName = "Sherry"
                        }
                    }
                    ,
                    new HelpMyStreet.Utils.Models.User
                    {
                        ID = 4,
                        UserPersonalDetails = new HelpMyStreet.Utils.Models.UserPersonalDetails
                        {
                            FirstName = "Peter",
                            LastName = "Kay"
                        }
                    }
                }
            });
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            await handler.Handle(_request, new CancellationToken());
            _communicationService.Verify(x => x.SendEmailToUsersAsync(It.Is<SendEmailToUsersRequest>(r => r.Recipients.ToUserIDs.Count == 1 && r.Recipients.CCUserIDs.Count == 2), It.IsAny<CancellationToken>()));
        }

        [Test] 
        public async Task WhenICall_SendEmail_IGetTrueResult_IUpdateRequest()
        {
            _communicationService.Setup(x => x.SendEmailToUsersAsync(It.IsAny<SendEmailToUsersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            await handler.Handle(_request, new CancellationToken());

            _repository.Verify(x => x.UpdateCommunicationSentAsync(_request.RequestID, true, It.IsAny<CancellationToken>()));            
        }

        [Test]
        public async Task WhenICall_SendEmail_IGetFalseResult_IUpdateRequest()
        {
            _communicationService.Setup(x => x.SendEmailToUsersAsync(It.IsAny<SendEmailToUsersRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            UpdateRequestHandler handler = new UpdateRequestHandler(_repository.Object, _communicationService.Object, _userService.Object, _applicationConfig.Object);
            await handler.Handle(_request, new CancellationToken());

            _repository.Verify(x => x.UpdateCommunicationSentAsync(_request.RequestID, false, It.IsAny<CancellationToken>()));
        }



    }
}