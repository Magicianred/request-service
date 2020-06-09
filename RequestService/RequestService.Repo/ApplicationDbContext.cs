using RequestService.Repo.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.SqlClient;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using HelpMyStreet.PostcodeCoordinates.EF.Extensions;
using HelpMyStreet.PostcodeCoordinates.EF.Entities;
using HelpMyStreet.Utils.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Question = RequestService.Repo.EntityFramework.Entities.Question;
using Job = RequestService.Repo.EntityFramework.Entities.Job;
using RequestService.Repo.Helpers;
namespace RequestService.Repo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            SqlConnection conn = (SqlConnection)Database.GetDbConnection();
  
            if (conn.DataSource.Contains("database.windows.net"))
            {
                conn.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
            }
        }

        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonalDetails> PersonalDetails { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestJobStatus> RequestJobStatus { get; set; }
        public virtual DbSet<SupportActivities> SupportActivities { get; set; }
        public virtual DbQuery<DailyReport> DailyReport { get; set; }
        public virtual DbSet<PostcodeEntity> Postcode { get; set; }
        public virtual DbSet<ActivityQuestions> ActivityQuestions { get; set; }
        public virtual DbSet<Question> Question { get; set; }

        public virtual DbSet<RequestQuestions> RequestQuestions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<DailyReport>().ToQuery(() => DailyReport.FromSql("TwoHourlyReport"));

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "Request");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Details).IsUnicode(false);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.SupportActivityId).HasColumnName("SupportActivityID");

                entity.Property(e => e.VolunteerUserId).HasColumnName("VolunteerUserID");

                entity.Property(e => e.JobStatusId).HasColumnName("JobStatusID");

                entity.HasOne(d => d.NewRequest)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewRequest_NewRequestID");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "RequestPersonal");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Locality)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OtherPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonalDetails>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("PersonalDetails", "RequestPersonal");

                entity.Property(e => e.RequestId)
                    .HasColumnName("RequestID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FurtherDetails)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorEmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorFirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorLastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestorPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Request)
                    .WithOne(p => p.PersonalDetails)
                    .HasForeignKey<PersonalDetails>(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalDetails_RequestID");
            });


            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request", "Request");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.DateRequested)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OtherDetails).IsUnicode(false);
                entity.Property(e => e.OrganisationName).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.PersonIdRecipient).HasColumnName("PersonID_Recipient");

                entity.Property(e => e.PersonIdRequester).HasColumnName("PersonID_Requester");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialCommunicationNeeds).IsUnicode(false);

                entity.HasOne(d => d.PersonIdRecipientNavigation)
                    .WithMany(p => p.RequestPersonIdRecipientNavigation)
                    .HasForeignKey(d => d.PersonIdRecipient)
                    .HasConstraintName("FK_RequestPersonal_Person_PersonID_Recipient");

                entity.HasOne(d => d.PersonIdRequesterNavigation)
                    .WithMany(p => p.RequestPersonIdRequesterNavigation)
                    .HasForeignKey(d => d.PersonIdRequester)
                    .HasConstraintName("FK_RequestPersonal_Person_PersonID_Requester");
            });

            modelBuilder.Entity<RequestJobStatus>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.DateCreated, e.JobStatusId });

                entity.ToTable("RequestJobStatus", "Request");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JobStatusId).HasColumnName("JobStatusID");

                entity.Property(e => e.VolunteerUserId).HasColumnName("VolunteerUserID");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.RequestJobStatus)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_JobID");
            });

            modelBuilder.Entity<SupportActivities>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.ActivityId });

                entity.ToTable("SupportActivities", "Request");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.SupportActivities)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupportActivities_RequestID");
            });
            modelBuilder.Entity<ActivityQuestions>(entity =>
            {
                entity.HasKey(e => new { e.ActivityId, e.QuestionId });

                entity.ToTable("ActivityQuestions", "QuestionSet");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.SetActivityQuestionData();

                entity.Property(e => e.Order)
                .IsRequired()
                .HasDefaultValue(1);

             

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ActivityQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);                
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question", "QuestionSet");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdditionalData).IsUnicode(false);

                entity.SetQuestionData();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
                
              
            });
            modelBuilder.Entity<RequestQuestions>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.QuestionId });

                entity.ToTable("RequestQuestions", "Request");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.RequestQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestQuestions)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });


            modelBuilder.SetupPostcodeCoordinateTables();
            modelBuilder.SetupPostcodeCoordinateDefaultIndexes();
        }
      
    }
}
