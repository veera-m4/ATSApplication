using byteforzaFinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace byteforzaFinalProject.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You don't actually ever need to call this
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=VEERA;Database=312Online_Shopping1;Trusted_Connection =true;TrustServerCertificate=true;MultipleActiveResultSets=true ";
                optionsBuilder.UseMySql(
                connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
        public DbSet<Candidate> candidates { get; set; }
        public DbSet<CandidateProcess> candidatesProcess { get; set; }
        public DbSet<Interview> interviews { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Feedback> feedbacks { set; get; }
    }
}
