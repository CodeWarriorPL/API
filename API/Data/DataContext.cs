using API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<UserMeasurement> UserMeasurements { get; set; }

        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Trainings)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete issues

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserMeasurements)
                .WithOne(um => um.User)
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Training>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trainings)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Training>()
                .HasOne(t => t.TrainingPlan)
                .WithMany(tp => tp.Trainings)
                .HasForeignKey(t => t.TrainingPlanId)
                .OnDelete(DeleteBehavior.Cascade); // Fixes the foreign key issue

            modelBuilder.Entity<Training>()
                .HasMany(t => t.Sets)
                .WithOne(s => s.Training)
                .HasForeignKey(s => s.TrainingId)
                .OnDelete(DeleteBehavior.Cascade); // Allowed if it does not cause cycles

            modelBuilder.Entity<Set>()
                .HasOne(s => s.Exercise)
                .WithMany()
                .HasForeignKey(s => s.ExerciseId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

