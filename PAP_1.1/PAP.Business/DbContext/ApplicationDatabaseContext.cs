using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAP.DataBase;
using PAP.DataBase.Auth;
using System;

namespace PAP.Business.DbContext
{
        
    public class ApplicationDatabaseContext : IdentityDbContext<User, UserRole, Guid>
    {
        public ApplicationDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AccountNotification> AccountNotifications { get; set; }
        public virtual DbSet<AccountOnEvent> AccountOnEvent { get; set; }
        public virtual DbSet<AccountPublish> AccountPublish { get; set; }
        public virtual DbSet<AccountRelationship> AccountRelationship { get; set; }
        public virtual DbSet<ContentPublishAccount> ContentPublishAccount { get; set; }
        public virtual DbSet<ContentPublishEvent> ContentPublishEvent { get; set; }
        public virtual DbSet<Event> Event { get; set; }   
        public virtual DbSet<FeedBackContentAccount> FeedBackContentAccount { get; set; }
        public virtual DbSet<FeedBackContentEvent> FeedBackContentEvent { get; set; }
        public virtual DbSet<PhotoContentPublishAccount> PhotoContentPublishAccount { get; set; }
        public virtual DbSet<PhotoContentPublishEvent> PhotoContentPublishEvent { get; set; }
        public virtual DbSet<PublishEvent> PublishEvent { get; set; }
        public virtual DbSet<VideoContentPublishAccount> VideoContentPublishAccount { get; set; }
        public virtual DbSet<VideoContentPublishEvent> VideoContentPublishEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(P => P.PhotoUrl)
                .HasDefaultValue("DefaultUserPhoto.png");

            modelBuilder.Entity<AccountPublish>()
                .HasOne(AP => AP.ContentPublishAccounts)
                .WithOne(I => I.AccountPublish)
                .HasForeignKey<ContentPublishAccount>(C => C.AccountPublishId);


            modelBuilder.Entity<PublishEvent>()
                .HasOne(PE => PE.ContentPublishEvent)
                .WithOne(I => I.PublishEvent)
                .HasForeignKey<ContentPublishEvent>(C => C.PublishEventId);       

            modelBuilder.Entity<Event>()
                .Property(E => E.IsEnabled)
                .HasDefaultValue(1);

            modelBuilder.Entity<Event>()
                .Property(E => E.Stars)
                .HasDefaultValue(3);

            modelBuilder.Entity<FeedBackContentAccount>()
                .Property(F => F.Stars)
                .HasDefaultValue(3);

            modelBuilder.Entity<FeedBackContentEvent>()
              .Property(F => F.Stars)
              .HasDefaultValue(3);

            modelBuilder.Entity<AccountRelationship>()
                .HasIndex(RA => new { RA.SenderAccountId, RA.ReceiverAccountId })
                .IsUnique();


            modelBuilder.Entity<AccountRelationship>()
                .HasOne(AR => AR.ReceiverAccount)
                .WithMany(x => x.ReceiverAccountRelationships)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AccountRelationship>()
                .HasOne(AR => AR.SenderAccount)
                .WithMany(x => x.SenderAccountRelationships)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AccountNotification>()
              .HasIndex(AN => new { AN.SenderNotificationAccountId, AN.ReceiverNotificationAccountId })
              .IsUnique();

            modelBuilder.Entity<AccountNotification>()
                .HasOne(AN => AN.ReceiverNotificationAccount)
                .WithMany(x => x.ReceiverNotificationAccounts)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AccountNotification>()
                .HasOne(AN => AN.SenderNotificationAccount)
                .WithMany(x => x.SenderNotificationAccounts )
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FeedBackContentAccount>()
              .HasOne(AN => AN.AccountPublish)
              .WithMany(x => x.FeedBackContentAccounts)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FeedBackContentEvent>()
             .HasOne(AN => AN.PublishEvent)
             .WithMany(x => x.FeedBackContentEvents)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
