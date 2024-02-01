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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasMany(u => u.Trainings)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
            modelBuilder.Entity<Training>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trainings)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Training>()
                .HasMany(t => t.Sets)
                .WithOne(s => s.Training)
                .HasForeignKey(s => s.TrainingId);
            modelBuilder.Entity<Set>()
                .HasOne(s => s.Training)
                .WithMany(t => t.Sets)
                .HasForeignKey(s => s.TrainingId);
            modelBuilder.Entity<Set>()
                .HasOne(s => s.Training)
                .WithMany(t => t.Sets)
                .HasForeignKey(s => s.TrainingId);
            modelBuilder.Entity<Set>()
                .HasOne(s => s.Exercise)
                .WithMany()
                .HasForeignKey(s => s.ExerciseId);
        }




    }
    }

