using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalPasswordManager.Repository.Models;

namespace PersonalPasswordManager.Repository;

public partial class PasswordManagerDbContext : DbContext
{
    //private readonly IConfiguration _configuration;
    //public PasswordManagerDbContext(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Password> Passwords { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Password>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Password__3214EC074197EDF4");

            entity.Property(e => e.App).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.EncryptedPassword).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
