using BookShop_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShop_Repostory.Context
{
    public class IdentityAppContext : IdentityDbContext<AppUser, Role, int>
    {

        public IdentityAppContext(DbContextOptions<IdentityAppContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Errors> ERRORS { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(b =>
            {
                b.ToTable("PERSON");
            });

            builder.Entity<Role>(b => {
                b.ToTable("ROLES");
            });

            builder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.ToTable("USER_CLAIMS");
            });
            builder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.ToTable("ROLE_CLAIMS");
            });
            builder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.ToTable("USER_LOGINS");
            });
            builder.Entity<IdentityUserToken<int>>(b =>
            {
                b.ToTable("USER_TOKENS");
            });
            builder.Entity<IdentityUserRole<int>>(b =>
            {
                b.ToTable("USER_ROLES");
            });


            builder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });
            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);
            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            builder.Entity<BookAuthor>()
               .HasKey(bc => new { bc.BookId, bc.AuthorId });
            builder.Entity<BookAuthor>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(bc => bc.BookId);
            builder.Entity<BookAuthor>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.BookAuthors)
                .HasForeignKey(bc => bc.AuthorId);

            builder.Entity<Book>()
            .Property(b => b.Price).HasColumnType("decimal(6, 2)");
        }
    }
}
