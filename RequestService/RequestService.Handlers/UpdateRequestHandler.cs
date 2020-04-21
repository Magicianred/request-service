using RequestService.Core.Domains.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Core.Dto;
using RequestService.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System;
using HelpMyStreet.Utils.Enums;

namespace RequestService.Handlers
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequestRequest>
    {
        private readonly IRepository _repository;
        private readonly ICommunicationService _communicationService;
        private readonly IUserService _userService;
        public UpdateRequestHandler(IRepository repository, ICommunicationService communicationService, IUserService userService)
        {
            _repository = repository;
            _communicationService = communicationService;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
           var personalDetails =  await UpdatePersonalDetailsAsync(request, cancellationToken);

           var supportDetails = await UpdateSupportActivitiesAsync(request.RequestID, request.SupportActivtiesRequired.SupportActivities, cancellationToken);

           bool commsSent = await SendEmailAsync(request.RequestID, personalDetails, supportDetails, cancellationToken);

           await _repository.UpdateCommunicationSentAsync(request.RequestID, commsSent, cancellationToken);

           return await Unit.Task;
        }

        private async Task<bool> SendEmailAsync(int requestId, PersonalDetailsDto requestorDetails, SupportActivityDTO supportActivities, CancellationToken cancellationToken )
        {
            
            string postCode = await _repository.GetRequestPostCodeAsync(requestId, cancellationToken);

            var champions = await _userService.GetChampionsByPostcode(postCode, cancellationToken);         

            if(champions == null || champions.Users.Count == 0)
            {
                throw new Exception("No Champions found using the supplied request postcode");
            }

            List<int> ChampionIds = champions.Users.Select(x => x.ID).ToList();
            List<int> ccList = new List<int>();

            // intially set the first person to be the userId before we apply logic
            int toUserId = ChampionIds.First();
            
            if(ChampionIds.Count >= 1)
            {
                Random random = new Random();
                var randomElementIndex = random.Next(0, (ChampionIds.Count - 1));
                toUserId = ChampionIds.ElementAt(randomElementIndex);
                ChampionIds.RemoveAt(randomElementIndex);

                if (ChampionIds.Count > 3)
                {
                    var randomCCElementIndex = random.Next(0, (ChampionIds.Count - 1));
                    ccList.Add(ChampionIds.ElementAt(randomCCElementIndex));
                    ChampionIds.RemoveAt(randomCCElementIndex);

                    randomCCElementIndex = random.Next(0, (ChampionIds.Count - 1));
                    ccList.Add(ChampionIds.ElementAt(randomCCElementIndex));
                    ChampionIds.RemoveAt(randomCCElementIndex);
                }
                else
                {
                    ccList = ChampionIds.Select(x => x).ToList();
                }                            
            }

            var selectedChampion = champions.Users.First(x => x.ID == toUserId);
            SendEmailToUsersRequest emailRequest = new SendEmailToUsersRequest
            {
                Recipients = new Recipients
                {
                    ToUserID = new List<int> { toUserId },
                    CcUserIDs = ccList,
                },
                Subject = "Help Requested",
                BodyText = "",
                BodyHTML =  BuildHtmlTemplate(requestorDetails.RequestorFirstName, requestorDetails.RequestorLastName, requestorDetails.RequestorEmailAddress,
                requestorDetails.RequestorPhoneNumber, requestorDetails.FurtherDetails, supportActivities.SupportActivities, selectedChampion.UserPersonalDetails.FirstName, selectedChampion.UserPersonalDetails.LastName)
            };
            
           return await _communicationService.SendEmailToUsersAsync(emailRequest, cancellationToken);
        }


        private string BuildHtmlTemplate(string RequestorFirstName, string RequestLastName, string RequestorEmailAddress, string RequestorPhoneNumber, string FurtherDetails,
            List<SupportActivities> activties, string ChampionFirstName, string ChampionLastName)
        {
            string html =  $"<!DOCTYPE html>" +
                "<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>" +
                "<head>" +
                    " <title> </title>" +
                    " <!--[if !mso]><!-- -->" +
                    " <meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                    " <!--<![endif]--> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'> <meta name='viewport' content='width=device-width, initial-scale=1'> " +
                    "<style type='text/css'>" +
                    " #outlook a { padding:0; }" +
                    " .ReadMsgBody { width:100%; }" +
                    " .ExternalClass { width:100%; } " +
                    ".ExternalClass * { line - height:100%; } " +
                    "body { margin:0;padding:0;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%; }" +
                    "table, td { border - collapse:collapse;mso-table-lspace:0pt;mso-table-rspace:0pt; }" +
                    "img { border:0;height:auto;line-height:100%; outline:none;text-decoration:none;-ms-interpolation-mode:bicubic; } " +
                    "p { display:block;margin:13px 0; } </style> <!--[if !mso]><!-->" +
                    "<style type='text/css'>" +
                    " @media only screen and (max-width:480px) { @-ms-viewport { width:320px; } @viewport { width:320px; } }" +
                    " </style> " +
                    "<!--<![endif]--> " +
                    "<!--[if mso]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml>" +
                    " <![endif]--> " +
                    "<!--[if lte mso 11]> <style type='text/css'> .outlook-group-fix { width:100% !important; } </style> <![endif]--> " +
                    "<!--[if !mso]><!--> <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>" +
                    "<link href='https://fonts.googleapis.com/css?family=Cabin:400,700' rel='stylesheet' type='text/css'>" +
                    " <style type='text/css'> @import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);" +
                    "@import url(https://fonts.googleapis.com/css?family=Cabin:400,700); " +
                    "</style> <!--<![endif]--> <style type='text/css'> @media only screen and (min-width:480px) " +
                    "{ .mj - column - per - 100 { width:100% !important; max-width: 100%; } } " +
                    "</style>" +
                    " <style type='text/css'> </style> " +
                    "<style type='text/css'>" +
                    ".hide_on_mobile { display: none !important;} " +
                    "@media only screen and (min-width: 480px) { .hide_on_mobile { display: block !important;} } " +
                    ".hide_section_on_mobile { display: none !important;} @media only screen and (min-width: 480px) { .hide_section_on_mobile { display: table !important;} } " +
                    ".hide_on_desktop { display: block !important;} @media only screen and (min-width: 480px) { .hide_on_desktop { display: none !important;} }" +
                    " .hide_section_on_desktop { display: table !important;} @media only screen and (min-width: 480px)" +
                    " { .hide_section_on_desktop { display: none !important;} } [owa] .mj-column-per-100 { width: 100%!important; } " +
                    "[owa] .mj-column-per-50 { width: 50%!important; } [owa] .mj-column-per-33 { width: 33.333333333333336%!important; }" +
                    " p { margin: 0px; } @media only print and (min-width:480px) { .mj - column - per - 100 { width:100%!important; } " +
                    ".mj-column-per-40 { width:40%!important; } .mj-column-per-60 { width:60%!important; } .mj-column-per-50 { width: 50%!important; } mj-column-per-33 { width: 33.333333333333336%!important; } }" +
                    "</style>" +
                " </head>" +
                " <body style='background-color:#FFFFFF;'>" +
                    " <div style='background-color:#FFFFFF;'> " +
                    "<!--[if mso | IE]> <table align='center' border='0' cellpadding='0' cellspacing='0' class='' style='width:600px;' width='600' >" +
                    " <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]--> <div style='Margin:0px auto;max-width:600px;'> " +
                    "<table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' style='width:100%;'>" +
                    " <tbody>" +
                    " <tr> " +
                    "<td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> " +
                    "<!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> " +
                    "<tr> <td class='' style='vertical-align:top;width:600px;' > " +
                    "<![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                    " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'> <tbody><tr>" +
                    " <td align='left' style='font-size:0px;padding:15px 15px 15px 15px;word-break:break-word;'> <div style='font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;color:#000000;'>" +
                    //Actual Content Starts HERE
                    " <h1 style='font-family: &apos;Cabin&apos;, sans-serif; text-align: center; line-height: 100%;'><span style='font-size: 48px;'><strong><span style='color: #001489;'>Help Requested</span></strong></span></h1>" +
                    $"<div>&#xA0;</div><p><span style='font-size: 14px; color: #000000;'><strong>Hi {ChampionFirstName} {ChampionFirstName},<br><br>" +
                    $"{RequestorFirstName}&#xA0; {RequestLastName} has requested some help with the following:&#xA0;</strong></span></p>" +
                    $"<ol>";
                     foreach(var activity in activties)
                      {
                        html += $"<li><span style='font-size: 14px; color: #000000;'> <strong>{activity}</strong></span></li>";
                      }                                      
                    html += $"</ol>" +
                    $"<div>&#xA0;</div><div><span style='font-size: 14px; color: #000000;'>" +
                    $"" +
                    $"<strong>Here are some details to get in touch with {RequestorFirstName} &#xA0;{RequestLastName}<br><br>&#xA0; " +
                    $"Email Address : {RequestorEmailAddress}<br>&#xA0; " +
                    $"Phone Number:&#xA0; {RequestorPhoneNumber}<br>&#xA0; " +
                    $"Further Details:&#xA0; {FurtherDetails}<br><br><br>" +
                    $"Thank you,<br>&#xA0;" +
                    $"HelpMyStreet<br><br><br><br>" +
                    $"</strong" +
                    $"></span></div><div>&#xA0;</div><div>&#xA0;</div> </div> </td> </tr>" +
                    $" </tbody></table> </div> <!--[if mso | IE]> </td> </tr> </table> <![endif]--> </td> </tr> </tbody>" +
                    $" </table> </div> <!--[if mso | IE]> </td> </tr> </table> <![endif]--> </div> " +
                $"</body>" +
                $"</html>";
            return html;

        }
        private async Task<PersonalDetailsDto> UpdatePersonalDetailsAsync(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
            PersonalDetailsDto dto = new PersonalDetailsDto
            {
                RequestID = request.RequestID,
                RequestorEmailAddress = request.RequestorEmailAddress,
                RequestorFirstName = request.RequestorFirstName,
                RequestorLastName = request.RequestorLastName,
                RequestorPhoneNumber = request.RequestorPhoneNumber,
                FurtherDetails = request.FurtherDetails,
                OnBehalfOfAnother = request.OnBehalfOfAnother,
            };

            await _repository.UpdatePersonalDetailsAsync(dto, cancellationToken);

            return dto;
        }
        private async Task<SupportActivityDTO> UpdateSupportActivitiesAsync(int requestId,  List<HelpMyStreet.Utils.Enums.SupportActivities> activities, CancellationToken cancellationToken)
        {
            SupportActivityDTO activies = new SupportActivityDTO
            {
                RequestId = requestId,
                SupportActivities = activities
            };

            await _repository.AddSupportActivityAsync(activies, cancellationToken);
            return activies;
        }
    }
}
