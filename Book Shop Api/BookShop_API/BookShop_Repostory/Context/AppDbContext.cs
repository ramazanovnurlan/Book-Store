using BookShop_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShop_API.BookShop_Repostory.Context
{
    //public class AppDbContext: DbContext
    //{
    //    public AppDbContext()
    //    {
    //    }

    //    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    //    {
    //    }

    //    public virtual DbSet<Author> Authors { get; set; }
    //    public virtual DbSet<Book> Books { get; set; }
    //    public virtual DbSet<Category> Categories { get; set; }
    //    public virtual DbSet<BookAuthor> BookAuthors { get; set; }
    //    public virtual DbSet<BookCategory> BookCategories { get; set; }
    //    public virtual DbSet<Language> Languages { get; set; }

    //    protected override void OnModelCreating(ModelBuilder modelmodelBuilder)
    //    {
    //        base.OnModelCreating(modelmodelBuilder);

    //        modelmodelBuilder.Entity<BookCategory>()
    //            .HasKey(bc => new { bc.BookId, bc.CategoryId });
    //        modelmodelBuilder.Entity<BookCategory>()
    //            .HasOne(bc => bc.Book)
    //            .WithMany(b => b.BookCategories)
    //            .HasForeignKey(bc => bc.BookId);
    //        modelmodelBuilder.Entity<BookCategory>()
    //            .HasOne(bc => bc.Category)
    //            .WithMany(c => c.BookCategories)
    //            .HasForeignKey(bc => bc.CategoryId);

    //        modelmodelBuilder.Entity<BookAuthor>()
    //           .HasKey(bc => new { bc.BookId, bc.AuthorId });
    //        modelmodelBuilder.Entity<BookAuthor>()
    //            .HasOne(bc => bc.Book)
    //            .WithMany(b => b.BookAuthors)
    //            .HasForeignKey(bc => bc.BookId);
    //        modelmodelBuilder.Entity<BookAuthor>()
    //            .HasOne(bc => bc.Author)
    //            .WithMany(c => c.BookAuthors)
    //            .HasForeignKey(bc => bc.AuthorId);

    //        modelmodelBuilder.Entity<Book>()
    //        .Property(b => b.Price).HasColumnType("decimal(6, 2)");

    //        modelmodelBuilder.Entity<AppUser>(b =>
    //        {
    //            b.ToTable("PERSON");
    //        });

    //        modelmodelBuilder.Entity<Role>(b => {
    //            b.ToTable("ROLES");
    //        });

    //        modelmodelBuilder.Entity<IdentityUserClaim<int>>(b =>
    //        {
    //            b.ToTable("USER_CLAIMS");
    //        });
    //        modelmodelBuilder.Entity<IdentityRoleClaim<int>>(b =>
    //        {
    //            b.ToTable("ROLE_CLAIMS");
    //        });
    //        modelmodelBuilder.Entity<IdentityUserLogin<int>>(b =>
    //        {
    //            b.ToTable("USER_LOGINS");
    //        });

    //        modelmodelBuilder.Entity<IdentityUserToken<int>>(b =>
    //        {
    //            b.ToTable("USER_TOKENS");
    //        });
    //        modelmodelBuilder.Entity<IdentityUserRole<int>>(b =>
    //        {
    //            b.ToTable("USER_ROLES");
    //        });
    //    }
    //}
}
