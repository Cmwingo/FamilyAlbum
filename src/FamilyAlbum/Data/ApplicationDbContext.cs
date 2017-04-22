using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Models;
using System.ComponentModel.DataAnnotations;

namespace FamilyAlbum.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserMessage>()
            .HasKey(t => new { t.ApplicationUserId, t.MessageId });

            builder.Entity<ApplicationUserMessage>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.IncomingMessages)
                .HasForeignKey(pt => pt.ApplicationUserId);

            builder.Entity<ApplicationUserMessage>()
                .HasOne(pt => pt.Message)
                .WithMany(t => t.Recipients)
                .HasForeignKey(pt => pt.MessageId);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Family> Family { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<PhotoAlbum> PhotoAlbum { get; set; }

        public DbSet<Message> Message { get; set; }
    }
}
