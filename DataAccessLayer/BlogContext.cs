using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    //after adding identity, identitydbcontext should be used instead of dbcontext
    public class BlogContext : IdentityDbContext<User>
    {
        //dbcontext options is derived from basecontext
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("your_connection_string", builder =>
        //    {
        //        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //    });
        //    base.OnConfiguring(optionsBuilder);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //     base.OnModelCreating(modelBuilder);



        ////     modelBuilder.Entity<Note>()
        ////.HasOne(b => b.Category)
        ////.WithMany(a => a.Notes)
        ////.IsRequired()
        ////.OnDelete(DeleteBehavior.SetNull);
        //}

        //generetes dbsets which are instances of tables of db

        //identity db context support related user tables 
        //public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
