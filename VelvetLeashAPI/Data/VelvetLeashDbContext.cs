using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.Data
{
    public class VelvetLeashDbContext : IdentityDbContext<User>
    {
        public VelvetLeashDbContext(DbContextOptions<VelvetLeashDbContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetSitter> PetSitters { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PetSitterService> PetSitterServices { get; set; }
        public DbSet<BookingMessage> BookingMessages { get; set; }
        public DbSet<UserNotificationSettings> UserNotificationSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User entity configuration
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasMaxLength(10);
                entity.Property(e => e.HowDidYouHear).HasMaxLength(500);
            });

            // Pet entity configuration
            builder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SpecialInstructions).HasMaxLength(1000);
                entity.Property(e => e.MedicalConditions).HasMaxLength(500);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Pets)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PetSitter entity configuration
            builder.Entity<PetSitter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.About).HasMaxLength(1000);
                entity.Property(e => e.Skills).HasMaxLength(500);
                entity.Property(e => e.HomeDetails).HasMaxLength(500);
                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasMaxLength(10);
                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DailyRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.OvernightRate).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.User)
                    .WithOne(u => u.PetSitter)
                    .HasForeignKey<PetSitter>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Booking entity configuration
            builder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SpecialInstructions).HasMaxLength(1000);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PetSitter)
                    .WithMany(ps => ps.Bookings)
                    .HasForeignKey(e => e.PetSitterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Pet)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(e => e.PetId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Review entity configuration
            builder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.HasOne(e => e.Reviewer)
                    .WithMany(u => u.ReviewsGiven)
                    .HasForeignKey(e => e.ReviewerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Reviewee)
                    .WithMany(u => u.ReviewsReceived)
                    .HasForeignKey(e => e.RevieweeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Booking)
                    .WithMany()
                    .HasForeignKey(e => e.BookingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PetSitterService entity configuration
            builder.Entity<PetSitterService>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(e => e.PetSitter)
                    .WithMany(ps => ps.Services)
                    .HasForeignKey(e => e.PetSitterId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // BookingMessage entity configuration
            builder.Entity<BookingMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);

                entity.HasOne(e => e.Booking)
                    .WithMany(b => b.Messages)
                    .HasForeignKey(e => e.BookingId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Sender)
                    .WithMany()
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // UserNotificationSettings entity configuration
            builder.Entity<UserNotificationSettings>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.User)
                    .WithOne(u => u.NotificationSettings)
                    .HasForeignKey<UserNotificationSettings>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed default notification settings for existing users
            // This would be handled in migrations or startup code
        }
    }
}