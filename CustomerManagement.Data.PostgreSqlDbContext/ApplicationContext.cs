using System;
using CustomerManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CustomerManagement.Data.PostgreSqlDbContext
{
    public class ApplicationDbContext:DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);
            //modelBuilder.ForNpgsqlUseSequenceHiLo();
            modelBuilder.UseHiLo();
            
            modelBuilder.Entity<Customer>()
                .HasOne(s => s.Company)
                .WithMany(e => e.Customers)
                .HasForeignKey(a => a.CompanyId);
            
            modelBuilder.Entity<Customer>()
                .HasOne(s => s.Title)
                .WithMany(e => e.Customers)
                .HasForeignKey(a => a.TitleId);
        
        }

        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<Title> Titles { get; set; }
    }
}