﻿using CaffStore.REST.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using MySql.Data.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaffStore.REST.Dal
{
    public partial class DataDrivenDbContext : DbContext
    {
        public DataDrivenDbContext()
        {
        }

        public DataDrivenDbContext(DbContextOptions<DataDrivenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Caff> Caffs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;database=caffstore;user=root;password=rootpassword";
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Caff>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.CaffFile).HasColumnType("longblob");
                entity.Property(e => e.UploaderId).IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Text).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.CaffId).IsRequired();
            });

        }
    }
}
