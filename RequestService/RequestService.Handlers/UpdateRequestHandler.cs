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
using HelpMyStreet.Contracts.RequestService.Request;
using HelpMyStreet.Contracts.CommunicationService.Request;

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

           var supportDetails = await UpdateSupportActivitiesAsync(request.RequestID, request.SupportActivitiesRequired.SupportActivities, cancellationToken);

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
            
            if(champions.Users.Count > 1)
            {
                Random random = new Random();
                var randomElementIndex = random.Next(0, (ChampionIds.Count - 1));
                toUserId = ChampionIds.ElementAt(randomElementIndex);
                ChampionIds.RemoveAt(randomElementIndex);

                if (champions.Users.Count > 3)
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
                    ToUserIDs = new List<int> { toUserId },
                    CCUserIDs = ccList,
                },
                Subject = "Help Requested",
                BodyText = $"Help Requested \r\n Hi {selectedChampion.UserPersonalDetails.FirstName} {selectedChampion.UserPersonalDetails.LastName}, \r\n " +
                $"{requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName} has requested some help with the following: \r\n" +
                $"{supportActivities.SupportActivities.Select(x => x.ToString() + "\r\n")}" +
                $"Here Are some details to get in touch with {requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName}" +
                $"Email Address: {requestorDetails.RequestorEmailAddress} \r\n" +
                $"Phone Number: {requestorDetails.RequestorPhoneNumber} \r\n" +
                $"Further Details: {requestorDetails.FurtherDetails} \r\n" +
                $"On Behalf of Someone: {requestorDetails.OnBehalfOfAnother} \r\n" +
                $"Health Or Wellbeing Concern: {requestorDetails.HealthOrWellbeingConcern} \r\n" +
                $"Thank you \r\n" +
                $"HelpMyStreet \r\n",
                BodyHTML =  BuildHtmlTemplate(requestorDetails, supportActivities, selectedChampion.UserPersonalDetails.FirstName, selectedChampion.UserPersonalDetails.LastName)
            };
            
           return await _communicationService.SendEmailToUsersAsync(emailRequest, cancellationToken);
        }
        
        private string BuildHtmlTemplate(PersonalDetailsDto requestorDetails, SupportActivityDTO supportActivity, string championFirstname, string championLastname)
        {
            string onBehalf = requestorDetails.OnBehalfOfAnother ? "Yes" : "No";
            string healthOrWellbeingConcern = requestorDetails.OnBehalfOfAnother ? "Yes" : "No";

            string html = "<!DOCTYPE html>" +
                "<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>" +
                "<head> <title> </title>" +
                " <!--[if !mso]><!-- --> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <!--<![endif]--> " +
                "<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'> <meta name='viewport' content='width=device-width, initial-scale=1'> <style type='text/css'> " +
                "#outlook a { padding:0; } .ReadMsgBody { width:100%; } .ExternalClass { width:100%; } .ExternalClass * { line-height:100%; }" +
                " body { margin:0;padding:0;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%; } " +
                "table, td { border-collapse:collapse;mso-table-lspace:0pt;mso-table-rspace:0pt; } " +
                "img { border:0;height:auto;line-height:100%; outline:none;text-decoration:none;-ms-interpolation-mode:bicubic; } p { display:block;margin:13px 0; } </style>" +
                " <!--[if !mso]><!--> <style type='text/css'> @media only screen and (max-width:480px) { @-ms-viewport { width:320px; } @viewport { width:320px; } } </style>" +
                " <!--<![endif]--> <!--[if mso]> <xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml>" +
                " <![endif]--> <!--[if lte mso 11]> <style type='text/css'> .outlook-group-fix { width:100% !important; } </style> <![endif]--> <!--[if !mso]><!--> " +
                "<link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>" +
                "<link href='https://fonts.googleapis.com/css?family=Cabin:400,700' rel='stylesheet' type='text/css'> <style type='text/css'> " +
                "@import url(https://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700);@import url(https://fonts.googleapis.com/css?family=Cabin:400,700); </style> <!--<![endif]-->" +
                " <style type='text/css'> @media only screen and (min-width:480px) { .mj-column-per-100 { width:100% !important; max-width: 100%; } } </style> " +
                "<style type='text/css'> @media only screen and (max-width:480px) { table.full-width-mobile { width: 100% !important; } td.full-width-mobile { width: auto !important; } }" +
                " </style> <style type='text/css'>.hide_on_mobile { display: none !important;} @media only screen and (min-width: 480px) { .hide_on_mobile { display: block !important;} }" +
                " .hide_section_on_mobile { display: none !important;} @media only screen and (min-width: 480px) { .hide_section_on_mobile { display: table !important;} }" +
                " .hide_on_desktop { display: block !important;} @media only screen and (min-width: 480px) { .hide_on_desktop { display: none !important;} } " +
                ".hide_section_on_desktop { display: table !important;} @media only screen and (min-width: 480px) { .hide_section_on_desktop { display: none !important;} } " +
                "[owa] .mj-column-per-100 { width: 100%!important; } [owa] .mj-column-per-50 { width: 50%!important; }" +
                " [owa] .mj-column-per-33 { width: 33.333333333333336%!important; } p { margin: 0px; } @media only print and (min-width:480px)" +
                " { .mj-column-per-100 { width:100%!important; } .mj-column-per-40 { width:40%!important; } .mj-column-per-60 { width:60%!important; }" +
                " .mj-column-per-50 { width: 50%!important; } mj-column-per-33 { width: 33.333333333333336%!important; } }</style> </head> <body style='background-color:#FFFFFF;'>" +
                " <div style='background-color:#FFFFFF;'> <!--[if mso | IE]> <table align='center' border='0' cellpadding='0' cellspacing='0' class='' style='width:600px;' width='600' >" +
                " <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]-->" +
                " <div style='background:#ffff;background-color:#ffff;Margin:0px auto;max-width:600px;'> " +
                "<table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' style='background:#ffff;background-color:#ffff;width:100%;'>" +
                " <tbody> <tr> <td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> <!--[if mso | IE]> " +
                "<table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td class='' style='vertical-align:top;width:600px;' > <![endif]--> " +
                "<div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'> " +
                "<tbody><tr> <td align='center' style='font-size:0px;padding:0px 0px 0px 0px;word-break:break-word;'> " +
                "<table border='0' cellpadding='0' cellspacing='0' role='presentation' style='border-collapse:collapse;border-spacing:0px;'> <tbody> <tr> <td style='width:600px;'>" +
                " <img height='auto' src='https://www.helpmystreet.org/img/logos/HelpMyStreet_logo.jpg' alt='logo'" +
                " style='border:0;display:block;outline:none;text-decoration:none;height:auto;width:100%;font-size:13px;' width='600'>" +
                " </td> </tr> </tbody> </table> </td> </tr> </tbody></table> </div> <!--[if mso | IE]> </td> </tr> </table>" +
                " <![endif]--> </td> </tr> </tbody> </table> </div> <!--[if mso | IE]> </td> </tr> </table> <table align='center' border='0' cellpadding='0' cellspacing='0'" +
                " class='' style='width:600px;' width='600' > <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]--> " +
                "<div style='background:#FFFFFF;background-color:#FFFFFF;Margin:0px auto;max-width:600px;'> <table align='center' border='0' cellpadding='0' " +
                "cellspacing='0' role='presentation' style='background:#FFFFFF;background-color:#FFFFFF;width:100%;'> <tbody> <tr> " +
                "<td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> <!--[if mso | IE]>" +
                " <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td class='' style='vertical-align:top;width:600px;' > <![endif]--> " +
                "<div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'>" +
                " <tbody><tr> <td align='left' style='font-size:0px;padding:15px 15px 15px 15px;word-break:break-word;'>" +
                " <div style='font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;color:#000000;'>" +
                " <h1 style='font-family: &apos;Cabin&apos;, sans-serif; text-align: center; line-height: 100%; color: #001489;'><span style='font-size: 36px;'>Help Requested</span></h1>" +
                " </div> </td> </tr> </tbody></table> </div>" +
                " <!--[if mso | IE]> </td> </tr> </table> <![endif]-->" +
                " </td> </tr> </tbody> </table> </div> <!--[if mso | IE]> </td> </tr> " +
                "</table> <table align='center' border='0' cellpadding='0' cellspacing='0' class='' style='width:600px;' width='600' > " +
                "<tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]--> " +
                "<div style='background:#FFFFFF;background-color:#FFFFFF;Margin:0px auto;max-width:600px;'> <table align='center' border='0' cellpadding='0'" +
                " cellspacing='0' role='presentation' style='background:#FFFFFF;background-color:#FFFFFF;width:100%;'>" +
                " <tbody> <tr> <td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> <!--[if mso | IE]>" +
                " <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td class='' style='vertical-align:top;width:600px;' >" +
                " <![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'> " +
                "<tbody><tr> <td align='left' style='font-size:0px;padding:15px 15px 15px 15px;word-break:break-word;'>" +
                " <div style='font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;color:#000000;'>" +
                $"<div><span style='font-size: 14px;'>Hi {championFirstname} {championLastname},</span></div>" +
                $"<div>&#xA0;</div><div><span style='font-size: 14px;'>{requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName} has requested some help with the following:&#xA0;</span>" +
                $"</div><ul>";
                foreach (var activity in supportActivity.SupportActivities) {
                    html += $"<li style='text-align: left;'><strong><span style='font-size: 14px;'>{activity}</span></strong></li>";    
                }
                html += $"</ul><div><strong>" +
                $"<span style='font-size: 14px;'><br></span></strong><span style='font-size: 14px;'>" +
                $"Here are some details to get in touch with {requestorDetails.RequestorFirstName} {requestorDetails.RequestorLastName} :</span></div>" +
                $"<div>&#xA0;</div><div style='text-align: left;'><span style='font-size: 14px;'>" +
                $"Email Address : {requestorDetails.RequestorEmailAddress} &#xA0;</span><br><span style='font-size: 14px;'>" +
                $"Phone Number: {requestorDetails.RequestorPhoneNumber} &#xA0;</span><br><span style='font-size: 14px;'>" +
                $"Further Details: &#xA0;{requestorDetails.FurtherDetails}</span></div><div style='text-align: left;'><span style='font-size: 14px;'>" +      
                $"On Behalf of Someone: { onBehalf }</span></div><div style='text-align: left;'><span style='font-size: 14px;'>" +
                $"Health Or Wellbeing Concern: {healthOrWellbeingConcern}</span></div><div><br><span style='font-size: 14px;'>" +
                $"Thank you</span></div><div><span style='font-size: 14px;'><br>" +
                $"HelpMyStreet</span></div> </div> </td> </tr> </tbody></table> </div> " +
                $"<!--[if mso | IE]> </td> </tr> </table> <![endif]--> </td> </tr> </tbody> " +
                $"</table> </div> <!--[if mso | IE]> </td> </tr> </table> <![endif]-->" +
                $" </div> </body></html>";

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
                HealthOrWellbeingConcern = request.HealthOrWellbeingConcern

            };

            await _repository.UpdatePersonalDetailsAsync(dto, cancellationToken);

            return dto;
        }
        private async Task<SupportActivityDTO> UpdateSupportActivitiesAsync(int requestId,  List<HelpMyStreet.Utils.Enums.SupportActivities> activities, CancellationToken cancellationToken)
        {
            SupportActivityDTO activies = new SupportActivityDTO
            {
                RequestID = requestId,
                SupportActivities = activities
            };

            await _repository.AddSupportActivityAsync(activies, cancellationToken);
            return activies;
        }
    }
}
