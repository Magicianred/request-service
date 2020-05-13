using RequestService.Repo.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.SqlClient;
using Microsoft.Azure.Services.AppAuthentication;

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

        public virtual DbSet<PersonalDetails> PersonalDetails { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<SupportActivities> SupportActivities { get; set; }

        public virtual DbQuery<DailyReport> DailyReport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<DailyReport>().ToQuery(() => DailyReport.FromSql("TwoHourlyReport"));

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

                entity.Property(e => e.DateRequested)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
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
        }
    }
}
