using Fitness.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.Guid))
                        .HasDefaultValueSql("gen_random_uuid()")
                        .IsRequired();
                }
            }

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Guid)
                    .IsUnique();
            });

            // Calendar
            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasIndex(e => e.Day)
                    .IsUnique(); 

                entity.HasIndex(e => e.Guid)
                    .IsUnique();
            });

            // Exercise
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.Property(e => e.Repetitions)
                    .HasDefaultValue(0);

                entity.Property(e => e.Approaches)
                    .HasDefaultValue(0);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Exercises)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Calendar)
                    .WithMany(c => c.Exercises)
                    .HasForeignKey(e => e.CalendarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MeasurementUnit)
                    .WithMany()
                    .HasForeignKey(e => e.MeasurementUnitId);

                entity.HasOne(e => e.Status)
                    .WithMany()
                    .HasForeignKey(e => e.StatusId);

                entity.HasIndex(e => e.Guid)
                    .IsUnique();
            });

            // MeasurementUnit
            modelBuilder.Entity<MeasurementUnit>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.HasIndex(e => e.Guid)
                    .IsUnique();

                // Seed data
                entity.HasData(
                    new MeasurementUnit { Id = 1, Guid = Guid.NewGuid(), Name = "repetitions" },
                    new MeasurementUnit { Id = 2, Guid = Guid.NewGuid(), Name = "kg" },
                    new MeasurementUnit { Id = 3, Guid = Guid.NewGuid(), Name = "minutes" },
                    new MeasurementUnit { Id = 4, Guid = Guid.NewGuid(), Name = "km" }
                );
            });

            // Status
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.HasIndex(e => e.Guid)
                    .IsUnique();

                // Seed data 
                entity.HasData(
                    new Status { Id = 1, Guid = Guid.NewGuid(), Name = "Not started" },
                    new Status { Id = 2, Guid = Guid.NewGuid(), Name = "In progress" },
                    new Status { Id = 3, Guid = Guid.NewGuid(), Name = "Completed" },
                    new Status { Id = 4, Guid = Guid.NewGuid(), Name = "Skipped" }
                );
            });
        }
    }
}
