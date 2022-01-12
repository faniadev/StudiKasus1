using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
            .HasMany(p=>p.Payments)
            .WithOne(p=>p.Enrollment!)
            .HasForeignKey(p=>p.EnrollmentID);

            modelBuilder.Entity<Payment>()
            .HasOne(p=>p.Enrollment)
            .WithMany(p=>p.Payments)
            .HasForeignKey(p=>p.EnrollmentID);
        }

    }
}
