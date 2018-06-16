using Microsoft.EntityFrameworkCore;
using Skor.EFCoreExtensions.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skor.EFCoreExtensions.Example
{
    public class ExampleDbContext:DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Toilet> Toilets { get; set; }
        public DbSet<ToiletAuthor> ToiletAuthors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(k => k.Id);
            modelBuilder.Entity<Author>().HasMany(m => m.Books)
                .WithOne(o => o.Author).HasForeignKey(f => f.AuthorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Author>().HasMany(f => f.ToiletAuthors)
                .WithOne(o => o.Author).HasForeignKey(f => f.AuthorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>().HasKey(k => k.Id);

            modelBuilder.Entity<Toilet>().HasKey(k => k.Id);
            modelBuilder.Entity<Toilet>().HasMany(m => m.ToiletAuthors)
                .WithOne(o => o.Toilet).HasForeignKey(f => f.ToiletId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ToiletAuthor>().HasKey(k => new { k.AuthorId, k.ToiletId });
        }
    }
}
