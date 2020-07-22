
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
        public static void SetQuestionData(this EntityTypeBuilder<Question> entity) {
            entity.HasData(new Question {
                Id = (int)Questions.SupportRequesting,
                Name = "Please tell us more about the help or support you're requesting",
                QuestionType = (int)QuestionType.MultiLineText,
                AdditionalData = GetAdditionalData(Questions.SupportRequesting)
            });
            entity.HasData(new Question {
                Id = (int)Questions.FaceMask_SpecificRequirements,
                Name = "Please tell us about any specific requirements (e.g. size, colour, style etc.)",
                QuestionType = (int)QuestionType.MultiLineText,
                AdditionalData = GetAdditionalData(Questions.FaceMask_SpecificRequirements)
            });
            entity.HasData(new Question {
                Id = (int)Questions.FaceMask_Amount,
                Name = "How many face coverings do you need?",
                QuestionType = (int)QuestionType.Number,
                AdditionalData = GetAdditionalData(Questions.FaceMask_Amount)
            });
            entity.HasData(new Question {
                Id = (int)Questions.FaceMask_Recipient,
                Name = "Who will be using the face coverings?",
                QuestionType = (int)QuestionType.Radio,
                AdditionalData = GetAdditionalData(Questions.FaceMask_Recipient)
            });
            entity.HasData(new Question { 
             Id = (int)Questions.FaceMask_Cost,
                Name = "Are you able to pay the cost of materials for your face covering (usually £2 - £3 each)?",
                QuestionType = (int)QuestionType.Radio,
                AdditionalData = GetAdditionalData(Questions.FaceMask_Cost)
            });

            entity.HasData(new Question { 
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
            var activites = Enum.GetValues(typeof(SupportActivities)).Cast<SupportActivities>();
            var requestFormVariants = Enum.GetValues(typeof(RequestHelpFormVariant)).Cast<RequestHelpFormVariant>();

            foreach (var activity in activites)
            {
                foreach (var form in requestFormVariants)
                {
                    if (activity == SupportActivities.FaceMask)
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.FaceMask_SpecificRequirements, Order = 2, RequestFormVariantId = (int)form , Required= false });
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.FaceMask_Amount, Order = 1, RequestFormVariantId = (int)form, Required = true });
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.FaceMask_Recipient, Order = 3, RequestFormVariantId = (int)form, Required = false });

                        if (form != RequestHelpFormVariant.FtLOS)
                        {
                            entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.FaceMask_Cost, Order = 4, RequestFormVariantId = (int)form, Required = false });
                        }
                        else
                        {
                            entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.FtlosDonationInformation, Order = 4, RequestFormVariantId = (int)form, Required = false });
                        }

                        if (form == RequestHelpFormVariant.DIY)
                        {
                            entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.WillYouCompleteYourself, Order = 5, RequestFormVariantId = (int)form, Required = true });
                        }
                        continue;

                    }
                    entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.SupportRequesting, Order = 1, RequestFormVariantId = (int)form , Required = false });
                    entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.IsHealthCritical, Order = 2, RequestFormVariantId = (int)form, Required = true });
                    if (form == RequestHelpFormVariant.DIY)
                    {
                        entity.HasData(new ActivityQuestions { ActivityId = (int)activity, QuestionId = (int)Questions.WillYouCompleteYourself, Order = 3, RequestFormVariantId = (int)form, Required = true });
                    }
                }
            }
        }
    }
}
