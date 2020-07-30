
using HelpMyStreet.Utils.Enums;
using HelpMyStreet.Utils.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using RequestService.Repo.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Question = RequestService.Repo.EntityFramework.Entities.Question;
using SupportActivities = HelpMyStreet.Utils.Enums.SupportActivities;

namespace RequestService.Repo.Helpers
{
    public static class QuestionExtenstions
    {
        public static void SetQuestionData(this EntityTypeBuilder<Question> entity)
        {
            entity.HasData(new Question
            {
                Id = (int)Questions.SupportRequesting,
                Name = "Please tell us more about the help or support you're requesting",
                QuestionType = (int)QuestionType.MultiLineText,
                AdditionalData = GetAdditionalData(Questions.SupportRequesting)
            });
            entity.HasData(new Question
            {
                Id = (int)Questions.FaceMask_SpecificRequirements,
                Name = "Please tell us about any specific requirements (e.g. size, colour, style etc.)",
                QuestionType = (int)QuestionType.MultiLineText,
                AdditionalData = GetAdditionalData(Questions.FaceMask_SpecificRequirements)
            });
            entity.HasData(new Question
            {
                Id = (int)Questions.FaceMask_Amount,
                Name = "How many face coverings do you need?",
                QuestionType = (int)QuestionType.Number,
                AdditionalData = GetAdditionalData(Questions.FaceMask_Amount)
            });
            entity.HasData(new Question
            {
                Id = (int)Questions.FaceMask_Recipient,
                Name = "Who will be using the face coverings?",
                QuestionType = (int)QuestionType.Radio,
                AdditionalData = GetAdditionalData(Questions.FaceMask_Recipient)
            });
            entity.HasData(new Question
            {
                Id = (int)Questions.FaceMask_Cost,
                Name = "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?",
                QuestionType = (int)QuestionType.Radio,
                AdditionalData = GetAdditionalData(Questions.FaceMask_Cost)
            });

            entity.HasData(new Question
            {
                Id = (int)Questions.IsHealthCritical,
                Name = "Is this request critical to someone's health or wellbeing?",
                QuestionType = (int)QuestionType.Radio,
                AdditionalData = GetAdditionalData(Questions.IsHealthCritical)
            });
            entity.HasData(new Question
            {
                Id = (int)Questions.WillYouCompleteYourself,
                Name = "Will you complete this request yourself?",
                QuestionType = (int)QuestionType.Radio,
                AdditionalData = GetAdditionalData(Questions.WillYouCompleteYourself)
            });

            entity.HasData(new Question
            {
                Id = (int)Questions.FtlosDonationInformation,
                Name = "Please donate to the For the Love of Scrubs GoFundMe <a href=\"https://www.gofundme.com/f/for-the-love-of-scrubs-face-coverings\" target=\"_blank\">here</a> to help pay for materials and to help us continue our good work. Recommended donation £3 - £4 per face covering.",
                QuestionType = (int)QuestionType.LabelOnly,
                AdditionalData = string.Empty
            });

            entity.HasData(new Question
            {
                Id = (int)Questions.CommunicationNeeds,
                Name = "Are there any communication needs that volunteers need to know about before they contact you or the person who needs help?",
                QuestionType = (int)QuestionType.MultiLineText,
                AdditionalData = string.Empty
            });
            entity.HasData(new Question
            {
                Id = (int)Questions.AnythingElseToTellUs,
                Name = "Is there anything else you would like to tell us about the request?",
                QuestionType = (int)QuestionType.MultiLineText,
                AdditionalData = string.Empty
            });
        }
        private static string GetAdditionalData(Questions question)
        {
            List<AdditonalQuestionData> additionalData = new List<AdditonalQuestionData>();
            switch (question)
            {
                case Questions.FaceMask_Recipient:
                    additionalData = new List<AdditonalQuestionData>
                    {
                        new AdditonalQuestionData
                        {
                            Key = "keyworkers",
                            Value = "Key workers"
                        },
                        new AdditonalQuestionData
                        {
                            Key = "somonekeyworkers",
                            Value = "Someone helping key workers stay safe in their role (e.g. care home residents, visitors etc.)"
                        },
                        new AdditonalQuestionData
                        {
                            Key = "memberspublic",
                            Value = "Members of the public"
                        },
                    };
                    break;
                case Questions.FaceMask_Cost:
                    additionalData = new List<AdditonalQuestionData>
                    {
                        new AdditonalQuestionData
                        {
                            Key = "Yes",
                            Value = "Yes"
                        },
                        new AdditonalQuestionData
                        {
                            Key = "No",
                            Value = "No"
                        },
                        new AdditonalQuestionData
                        {
                            Key = "Contribution",
                            Value = "I can make a contribution"
                        },
                    };
                    break;
                case Questions.IsHealthCritical:
                    additionalData = new List<AdditonalQuestionData>
                    {
                        new AdditonalQuestionData
                        {
                            Key = "true",
                            Value = "Yes"
                        },
                        new AdditonalQuestionData
                        {
                            Key = "false",
                            Value = "No"
                        }
                    };
                    break;
                case Questions.WillYouCompleteYourself:
                    additionalData = new List<AdditonalQuestionData>
                    {
                        new AdditonalQuestionData
                        {
                            Key = "true",
                            Value = "Yes"
                        },
                        new AdditonalQuestionData
                        {
                            Key = "false",
                            Value = "No, please make it visible to other volunteers"
                        }
                    };
                    break;
            }

            return JsonConvert.SerializeObject(additionalData);
        }


        public static void SetActivityQuestionData(this EntityTypeBuilder<ActivityQuestions> entity)
        {
            var requestFormVariants = Enum.GetValues(typeof(RequestHelpFormVariant)).Cast<RequestHelpFormVariant>();

            foreach (var form in requestFormVariants)
            {
                foreach (var activity in GetSupportActivitiesForRequestFormVariant(form))
                {
                    if (activity == SupportActivities.FaceMask)
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.FaceMask_SpecificRequirements, Location = "pos2", Order = 2, RequestFormVariantId = (int)form, Required = false, PlaceholderText = "Don’t forget to tell us how many of each size you need. If you have very specific style requirements it may take longer to find a volunteer to help with your request. Please don’t include personal information such as name or address in this box, we’ll ask for that later.", Subtext = "Size guide:<br />&nbsp;- Men’s (Small / Medium / Large)<br />&nbsp;- Ladies’ (Small / Medium / Large)<br />&nbsp;- Children’s (One Size - under 12)" });
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.FaceMask_Amount, Location = "pos3", Order = 1, RequestFormVariantId = (int)form, Required = true, Subtext = "Remember they’re washable and reusable, so only request what you need between washes." });
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.FaceMask_Recipient, Location = "pos3", Order = 3, RequestFormVariantId = (int)form, Required = false });

                        if (form != RequestHelpFormVariant.FtLOS)
                        {
                            entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.FaceMask_Cost, Location = "pos3", Order = 4, RequestFormVariantId = (int)form, Required = false, Subtext = "Volunteers are providing their time and skills free of charge." });
                        }
                        else
                        {
                            entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.FtlosDonationInformation, Location = "pos3", Order = 4, RequestFormVariantId = (int)form, Required = false });
                        }
                    }
                    else
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.SupportRequesting, Location = "pos1", Order = 1, RequestFormVariantId = (int)form, Required = false, PlaceholderText = "Please don’t include any sensitive details that aren’t needed in order for us to help you" });
                        
                        string placeholderText = activity == SupportActivities.CommunityConnector ? "Is there a specific issue you would like to discuss with the Community Connector, e.g. dealing with a bereavement (please don’t include personal details here)" : "For example, if it’s a request for some shopping and you know what you want, you could give us the list.";
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Detail, QuestionId = (int)Questions.AnythingElseToTellUs, Location = "details2", Order = 2, RequestFormVariantId = (int)form, Required = false, PlaceholderText = placeholderText });
                    }

                    if (form != RequestHelpFormVariant.HLP_CommunityConnector && activity != SupportActivities.FaceMask)
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.IsHealthCritical, Location = "pos3", Order = 2, RequestFormVariantId = (int)form, Required = true });
                    }

                    entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.SupportRequesting, Order = 1, RequestFormVariantId = (int)form , Required = false });
                    
                    if (form != RequestHelpFormVariant.HLP_CommunityConnector)
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.IsHealthCritical, Order = 2, RequestFormVariantId = (int)form, Required = true });
                    }

                    if (form == RequestHelpFormVariant.DIY)
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Request, QuestionId = (int)Questions.WillYouCompleteYourself, Location = "pos3", Order = 3, RequestFormVariantId = (int)form, Required = true });
                    }

                    entity.HasData(new ActivityQuestions { ActivityId = (int)activity, RequestFormStageId = (int)RequestHelpFormStage.Detail, QuestionId = (int)Questions.CommunicationNeeds, Location = "details2", Order = 1, RequestFormVariantId = (int)form, Required = false, PlaceholderText = "For example, do you have any specific language requirement or hearing issues that we should know about?" });
                }
            }
        }

        private static IEnumerable<SupportActivities> GetSupportActivitiesForRequestFormVariant(RequestHelpFormVariant form)
        {
            IEnumerable<SupportActivities> activites;
            IEnumerable<SupportActivities> genericSupportActivities = Enum.GetValues(typeof(SupportActivities)).Cast<SupportActivities>()
                .Where(sa => sa != SupportActivities.WellbeingPackage && sa != SupportActivities.CommunityConnector);

            switch (form)
            {
                case RequestHelpFormVariant.FtLOS:
                    activites = new List<SupportActivities>() { SupportActivities.FaceMask };
                    break;

                case RequestHelpFormVariant.HLP_CommunityConnector:
                    activites = new List<SupportActivities>() { SupportActivities.CommunityConnector };
                    break;

                case RequestHelpFormVariant.VitalsForVeterans:
                    activites = new List<SupportActivities>(genericSupportActivities);
                    ((List<SupportActivities>)activites).Add(SupportActivities.WellbeingPackage);
                    break;

                default: 
                    activites = genericSupportActivities; 
                    break;
            };

            return activites;
        }
    }
}
