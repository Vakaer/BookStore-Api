using BookStore.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Persistence.Persistence.Repositories
{
    public class BookStoreDbContext :DbContext
    {
        public BookStoreDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<BookEntity> tbl_books { get; set; }
        public DbSet<AuthorEntity> tbl_Author{ get; set; }
        public DbSet<GenresEntity> tbl_Genres { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GenresEntity>(entity =>
            {
                entity
               .HasMany(e => e.Books)
               .WithOne(b => b.GenresEntity);

            });
            modelBuilder.Entity<AuthorEntity>(entity =>
            {
                entity
                .HasMany(e => e.Books)
                .WithOne(a => a.AuthorEntity);
            });


        }

    }

}
