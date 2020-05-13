using RequestService.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RequestService.Core.Services
{
    public static class EmailBuilder
    {

        private  static Dictionary<HelpMyStreet.Utils.Enums.SupportActivities, string> _mappings = new Dictionary<HelpMyStreet.Utils.Enums.SupportActivities, string>() {
            { HelpMyStreet.Utils.Enums.SupportActivities.Shopping, "Shopping" },
            { HelpMyStreet.Utils.Enums.SupportActivities.CollectingPrescriptions, "Prescriptions" },
            { HelpMyStreet.Utils.Enums.SupportActivities.Errands, "Errands" },
            { HelpMyStreet.Utils.Enums.SupportActivities.DogWalking, "Dog Walking" },
            { HelpMyStreet.Utils.Enums.SupportActivities.MealPreparation, "Prepared Meal" },
            { HelpMyStreet.Utils.Enums.SupportActivities.PhoneCalls_Friendly, "Friendly Chat" },
            { HelpMyStreet.Utils.Enums.SupportActivities.PhoneCalls_Anxious, "Supportive Chat" },
            { HelpMyStreet.Utils.Enums.SupportActivities.HomeworkSupport, "Homework" },
            { HelpMyStreet.Utils.Enums.SupportActivities.CheckingIn, "Check In" },
            { HelpMyStreet.Utils.Enums.SupportActivities.FaceMask, "Face Covering" },
            { HelpMyStreet.Utils.Enums.SupportActivities.Other, "Other" }
        };

        public static string BuildHelpRequestedEmail(PersonalDetailsDto requestorDetails, SupportActivityDTO supportActivity, string postcode)
        {
            string onBehalf = requestorDetails.OnBehalfOfAnother ? "Yes" : "No";
            string healthOrWellbeingConcern = requestorDetails.OnBehalfOfAnother ? "Yes" : "No";

            string html = BuildHeader();
            html += BuildTitle("Help Requested");

            html += "<table align='center' border='0' cellpadding='0' cellspacing='0' class='' style='width:600px;' width='600' > " +
                "<tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]--> " +
                "<div style='background:#FFFFFF;background-color:#FFFFFF;Margin:0px auto;max-width:600px;'> <table align='center' border='0' cellpadding='0'" +
                " cellspacing='0' role='presentation' style='background:#FFFFFF;background-color:#FFFFFF;width:100%;'>" +
                " <tbody> <tr> <td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> <!--[if mso | IE]>" +
                " <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td class='' style='vertical-align:top;width:600px;' >" +
                " <![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'> " +
                "<tbody><tr> <td align='left' style='font-size:0px;padding:15px 15px 15px 15px;word-break:break-word;'>" +
                " <div style='font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;color:#000000;'>" +
                $"<div><span style='font-size: 14px;'>" +
                $"Hi. You’re receiving this email because you signed up as a Street Champion at HelpMyStreet.org. A request for help has come in for {postcode}. The details of the request are below.</span></div>" +
                $"<div>&#xA0;</div>" +
                $"<div>" +
                $"<span style='font-size: 14px;'>" +
                $"If you are copied on this message, then you are a “backup” and you don’t need to do anything just now. If you don’t see a response from the Street Champion the message was sent “To” within a day or two though," +
                $" it would be great if you could check with them, to see if they need any assistance." +
                $"</span>" +
                $"</div>" +
                   $"<div>&#xA0;</div>" +
                $"<div>" +
                $"<span style='font-size: 14px;'>If this message has been sent “To” you though, it would be great if you could take the lead on responding to it, " +
                $"making contact with the sender as soon as you can, or arranging for someone else to do so. </span>" +
                $"</span>" +
                $"</div>" +
            $"<div>&#xA0;</div><div style='text-align: left;'>" +
            $"<span style='font-size: 14px;'>Postcode : {postcode} &#xA0;</span><br>" +
            $"<span style='font-size: 14px;'>First Name: {requestorDetails.RequestorFirstName} </span><br>" +
            $"<span style='font-size: 14px;'>Last Name: {requestorDetails.RequestorLastName} </span><br>" +
            $"<span style='font-size: 14px;'>Email Address: {requestorDetails.RequestorEmailAddress} </span><br>" +
            $"<span style='font-size: 14px;'>Phone Number: {requestorDetails.RequestorPhoneNumber} </span><br>" +
            $"<span style='font-size: 14px;'>On Behalf of someone: {onBehalf} </span><br>" +
            $"<span style='font-size: 14px;'>Health or Wellbeing Concern: {healthOrWellbeingConcern} </span><br>" +
            $"<span style='font-size: 14px;'>Further Details: {requestorDetails.FurtherDetails} </span><br>" +
            $"<span style='font-size: 14px;'>Help needed for: </span><br>" +
                 $"<ul>";
            foreach (var activity in supportActivity.SupportActivities)
            {
                html += $"<li style='text-align: left;'><strong><span style='font-size: 14px;'>{_mappings[activity]}</span></strong></li>";
            }

            html += $"</ul>" +
            $"</div>" +
            $"<div><br>" +
            $"<span style='font-size: 14px;'> So that you can co-ordinate your efforts, if this message has gone to more than one Street Champion," +
            $"it would be great if all those copied could keep each other informed of any actions they take relating to it (e.g. making contact with the sender).</span>" +
            $"</div>" +
            $"<div>" +
            $"<span style='font-size: 14px;'><br>" +
            $"Thanks so much!</span></div> " +
            $"</div> " +
                 $"<div>" +
            $"<span style='font-size: 14px;'><br>" +
            $"The HelpMyStreet Team</span></div> " +
            $"</div> " +
                      $"<div>" +
            $"<span style='font-size: 12px;'><br>" +
            $"if you think you have received this email in error or if you want to change your status (e.g. stop receiving emails like this), please let the HelpMyStreet team know by contacting support@helpmystreet.org.</span></div> " +
             $"<div>" +
            $"<span style='font-size: 12px;'>" +
            $"Once the request has been actioned, please delete any copies of this email that you have, for data protection reasons.</span></div> " +
            $"</div> " +
                          
            $"</div> " +
            $"</td> </tr> </tbody></table> </div> " +
            $"<!--[if mso | IE]> </td> </tr> </table> <![endif]--> </td> </tr> </tbody> " +
            $"</table> </div> <!--[if mso | IE]> </td> </tr> </table> <![endif]-->" +
            $" </div> </body></html>";

            return html;
        }

        public static string BuildConfirmationRequestEmail(bool hasStreetChampion)
        {

            string body = "We have passed your request on to local volunteers, who will try to contact you as soon as they can, to let you know if they can help you (or arrange help for you).  If you don’t hear back within a day or two, do try again, or contact us via support@helpmystreet.org and we’ll try to see if there’s been a problem.";

            if (!hasStreetChampion)
            {
                body = "We have received your request and will do all we can to try to find a local volunteer who can help.  If you need to contact us, you can reach us at support@helpmystreet.org.";                
            }

            string html = BuildHeader();
            html += BuildTitle("Request Received");

            html += "<table align='center' border='0' cellpadding='0' cellspacing='0' class='' style='width:600px;' width='600' > " +
                "<tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]--> " +
                "<div style='background:#FFFFFF;background-color:#FFFFFF;Margin:0px auto;max-width:600px;'> <table align='center' border='0' cellpadding='0'" +
                " cellspacing='0' role='presentation' style='background:#FFFFFF;background-color:#FFFFFF;width:100%;'>" +
                " <tbody> <tr> <td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> <!--[if mso | IE]>" +
                " <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td class='' style='vertical-align:top;width:600px;' >" +
                " <![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'> " +
                "<tbody><tr> <td align='left' style='font-size:0px;padding:15px 15px 15px 15px;word-break:break-word;'>" +
                " <div style='font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;color:#000000;'>" +
                $"<div>" +
                $"<span style='font-size: 14px;'>" +
                $"{body}" + 
                $"</span></div>" +
                $"<div>&#xA0;</div>" +    
                "<div>" + 
                $"<span style='font-size: 14px;'><br>" +
                $"Thanks so much!</span></div> " +
                  $"<div>" +
                $"<span style='font-size: 14px;'><br>" +
                $"The HelpMyStreet Team</span></div> " +
                $"</div> " +               
            $"</div> " +
                      $"<div>" +
            $"<span style='font-size: 12px;'><br>" +
            $"If you think you have received this email in error, please let the HelpMyStreet team know by contacting support@helpmystreet.org</span></div> " +
            $"</div> " +
            $"</td> </tr> </tbody></table> </div> " +
            $"<!--[if mso | IE]> </td> </tr> </table> <![endif]--> </td> </tr> </tbody> " +
            $"</table> </div> <!--[if mso | IE]> </td> </tr> </table> <![endif]-->" +
            $" </div> </body></html>";

            return html;
        }


        private static string BuildTitle(string title)
        {
            return " <table align='center' border='0' cellpadding='0' cellspacing='0'" +
                 " class='' style='width:600px;' width='600' > <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'> <![endif]--> " +
                 "<div style='background:#FFFFFF;background-color:#FFFFFF;Margin:0px auto;max-width:600px;'> <table align='center' border='0' cellpadding='0' " +
                 "cellspacing='0' role='presentation' style='background:#FFFFFF;background-color:#FFFFFF;width:100%;'> <tbody> <tr> " +
                 "<td style='direction:ltr;font-size:0px;padding:9px 0px 9px 0px;text-align:center;vertical-align:top;'> <!--[if mso | IE]>" +
                 " <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td class='' style='vertical-align:top;width:600px;' > <![endif]--> " +
                 "<div class='mj-column-per-100 outlook-group-fix' style='font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:top;width:100%;'>" +
                 " <table border='0' cellpadding='0' cellspacing='0' role='presentation' style='vertical-align:top;' width='100%'>" +
                 " <tbody><tr> <td align='left' style='font-size:0px;padding:15px 15px 15px 15px;word-break:break-word;'>" +
                 " <div style='font-family:Ubuntu, Helvetica, Arial, sans-serif;font-size:11px;line-height:1.5;text-align:left;color:#000000;'>" +
                 $" <h1 style='font-family: &apos;Cabin&apos;, sans-serif; text-align: center; line-height: 100%; color: #001489;'><span style='font-size: 36px;'>{title}</span></h1>" +
                 " </div> </td> </tr> </tbody></table> </div>" +
                 " <!--[if mso | IE]> </td> </tr> </table> <![endif]-->" +
                 " </td> </tr> </tbody> </table> </div> <!--[if mso | IE]> </td> </tr> " +
                 "</table> ";
        }
        private static string BuildHeader()
        {
           return "<!DOCTYPE html>" +
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
                " <![endif]--> </td> </tr> </tbody> </table> </div> <!--[if mso | IE]> </td> </tr> </table>";
        }
    }

}
