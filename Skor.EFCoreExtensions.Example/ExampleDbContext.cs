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
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(k => k.Id);
            modelBuilder.Entity<Author>().HasMany(m => m.Books)
                .WithOne(o => o.Author).HasForeignKey(f => f.AuthorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>().HasKey(k => k.Id);

            base.OnModelCreating(modelBuilder);

        }
    }
}
