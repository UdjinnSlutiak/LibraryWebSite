using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_4.Models;

namespace Lab_4.Models
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookStudent>()
                .HasKey(t => new { t.BookId, t.StudentId });

            modelBuilder.Entity<BookStudent>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookStudents)
                .HasForeignKey(bs => bs.BookId);

            modelBuilder.Entity<BookStudent>()
                .HasOne(bs => bs.Student)
                .WithMany(s => s.BooksStudent)
                .HasForeignKey(bs => bs.StudentId);
        }

        public DbSet<Lab_4.Models.AddBookViewModel> AddBookViewModel { get; set; }

        public DbSet<Lab_4.Models.AddStudentViewModel> AddStudentViewModel { get; set; }

        public DbSet<Lab_4.Models.AddAdministratorViewModel> AddAdministratorViewModel { get; set; }

        //public DbSet<Lab_4.Models.AddBookViewModel> AddBookViewModel { get; set; }
        //public DbSet<Lab_4.Models.AddStudentViewModel> AddStudentViewModel { get; set; }
    }
}
