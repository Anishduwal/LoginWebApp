using Login.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> User => Set<User>();
        public DbSet<RefreshToken> LoginTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //    entity.HasIndex(x => x.Email).IsUnique();
            //    entity.Property(x => x.Email).IsRequired();
            //    entity.Property(x => x.PasswordHash).IsRequired();
            //});

            // REFRESH TOKEN TABLE
            //modelBuilder.Entity<RefreshToken>(entity =>
            //{
            //    entity.ToTable("RefreshTokens");

            //    entity.HasKey(x => x.Id);

            //    entity.Property(x => x.Token)
            //          .IsRequired()
            //          .HasMaxLength(500);

            //    entity.Property(x => x.ExpireDate)
            //          .IsRequired();

            //    entity.Property(x => x.IsRevoked)
            //          .IsRequired();

            //    entity.HasOne<User>()
            //          .WithMany()
            //          .HasForeignKey(x => x.UserId)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});
        }
    }
}
