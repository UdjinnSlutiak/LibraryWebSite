using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab_4.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Lab_4.Controllers
{
    public class HomeController : Controller
    {
        public string id;
        

        private readonly LibraryDbContext dbContext;
        public HomeController(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetUser()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            User user = dbContext.Users.Where(u => u.UserId == id).FirstOrDefault();
            return user;
        }

        [HttpGet]
        public IActionResult Index(GetViewModel model)
        {
            model.User = GetUser();
            return View(model);
        }

        [HttpGet]
        public IActionResult Books(GetViewModel model)
        {
            model.User = GetUser();
            model.Books = dbContext.Books.AsEnumerable<Book>();
            return View(model);
        }

        [HttpGet]
        public IActionResult Students(GetViewModel model)
        {
            model.User = GetUser();
            model.Students = dbContext.Students.AsEnumerable<Student>();
            return View(model);
        }

        [HttpGet]
        public IActionResult Administrators(GetViewModel model)
        {
            model.User = GetUser();
            model.Administrators = dbContext.Administrators.AsEnumerable<Administrator>();
            return View(model);
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            if (HttpContext.Session.Keys.Contains("id"))
                return RedirectToAction("Index");
            else return View();
        }

        [HttpPost]
        public IActionResult Authorization(AuthorizationViewModel model)
        {
            var user = dbContext.Users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
            if (user != null)
            {
                string id = user.UserId.ToString();
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("id", id);
                return RedirectToAction("Index");
            }
            else return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Authorization");
        }

        [HttpGet]
        public IActionResult AddBookG(AddBookViewModel model)
        {
            model.User = GetUser();
            return View("AddBook", model);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            Book book = new Book
            {
                Name = model.Name,
                Author = model.Author,
                PageCount = model.PageCount,
                TotalCount = model.TotalCount
            };
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
            return RedirectToAction("AddBookG");
        }

        [HttpGet]
        public IActionResult AddStudentG(AddStudentViewModel model)
        {
            model.User = GetUser();
            return View("AddStudent", model);
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentViewModel model)
        {
            Student student = new Student()
            {
                FirstName = model.FirstName,
                LastName = model.SecondName,
                FatherName = model.FatherName,
                Group = model.Group,
                Faculty = model.Faculty,
                Login = model.Login,
                Password = model.Password,
                Type = Models.User.UserType.Student
            };
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
            return RedirectToAction("AddStudentG");
        }

        [HttpGet]
        public IActionResult AddAdministratorG(AddAdministratorViewModel model)
        {
            model.User = GetUser();
            return View("AddAdministrator", model);
        }

        [HttpPost]
        public IActionResult AddAdministrator(AddAdministratorViewModel model)
        {
            Administrator administrator = new Administrator
            {
                FirstName = model.FirstName,
                LastName = model.SecondName,
                FatherName = model.FatherName,
                Position = model.Position,
                Login = model.Login,
                Password = model.Password,
                Type = Models.User.UserType.Administrator
            };
            dbContext.Administrators.Add(administrator);
            dbContext.SaveChanges();
            return RedirectToAction("AddAdministratorG");
        }

        [HttpGet]
        public IActionResult Summary(int studentId)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.UserId == studentId);
            //List<Book> TakenBooks = dbContext.Students.Where(s => s.Id == studentId).Include(b => b.Books).FirstOrDefault();
            List<Book> bookStudents = student.BooksStudent.Where(bs => bs.StudentId == studentId).Select(bs => bs.Book).ToList();
            var booooks = dbContext.Books.Include(b => b.BookStudents).ThenInclude(bs => bs.Student).ToList();
            //var courses = db.Courses.Include(c => c.StudentCourses).ThenInclude(sc => sc.Student).ToList();
            List<Book> books = dbContext.Books.ToList();


            var vm = new AddBtSViewModel
            {
                Student = student,
                TakenBooks = booooks.Where(b => b.BookStudents.Select(bs => bs.StudentId).Contains(studentId))
            };
            vm.AvailableBooks = books.Where(b => b.IsAvailable == true).Except(vm.TakenBooks).ToList();
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult TakeBook(int studentId, int bookId)
        {
            var student = dbContext.Students.FirstOrDefault(s => s.UserId == studentId);
            var book = dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            student.TakeBook(book);
            dbContext.Students.Update(student);
            dbContext.Books.Update(book);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Summary), new { studentId = studentId });
        }

        [HttpPost]
        public RedirectToActionResult ReturnBook(int studentId, int bookId)
        {
            /*var student = dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            var book = dbContext.Books.FirstOrDefault(b => b.Id == bookId);
            student.ReturnBook(book);*/
            Book book = dbContext.Books.Include(bs => bs.BookStudents).FirstOrDefault(b => b.Id == bookId);
            Student student = dbContext.Students.FirstOrDefault(s => s.UserId == studentId);
            student.ReturnBook(book);
            dbContext.Students.Update(student);
            dbContext.Books.Update(book);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Summary), new { studentId = studentId });
        }

        [HttpGet]
        public IActionResult EditBook(int Id)
        {
            Book book = dbContext.Books.Find(Id);
            return View(book);
        }

        [HttpPost]
        public IActionResult EditBook(Book book)
        {
            dbContext.Update(book);
            dbContext.SaveChanges();
            return RedirectToAction("Books");
        }

        [HttpGet]
        public IActionResult DeleteBook(int Id)
        {
            Book book = dbContext.Books.Find(Id);
            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteBook(Book book)
        {
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
            return RedirectToAction("Books");
        }

        [HttpGet]
        public IActionResult BookInfo(int Id)
        {
            Book book = dbContext.Books.Find(Id);
            return View(book);
        }

        [HttpPost]
        public IActionResult BookInfo() => RedirectToAction("Books");

        [HttpGet]
        public IActionResult EditStudent(int Id)
        {
            Student student = dbContext.Students.Find(Id);
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            dbContext.Update(student);
            dbContext.SaveChanges();
            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult DeleteStudent(int Id)
        {
            Student student = dbContext.Students.Find(Id);
            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteStudent(Student student)
        {
            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult StudentInfo(int Id)
        {
            Student student = dbContext.Students.Find(Id);
            return View(student);
        }

        [HttpPost]
        public IActionResult StudentInfo() => RedirectToAction("Students");

        [HttpGet]
        public IActionResult EditAdministrator(int Id)
        {
            Administrator Administrator = dbContext.Administrators.Find(Id);
            return View(Administrator);
        }

        [HttpPost]
        public IActionResult EditAdministrator(Administrator Administrator)
        {
            dbContext.Update(Administrator);
            dbContext.SaveChanges();
            return RedirectToAction("Administrators");
        }

        [HttpGet]
        public IActionResult DeleteAdministrator(int Id)
        {
            Administrator Administrator = dbContext.Administrators.Find(Id);
            return View(Administrator);
        }

        [HttpPost]
        public IActionResult DeleteAdministrator(Administrator Administrator)
        {
            dbContext.Administrators.Remove(Administrator);
            dbContext.SaveChanges();
            return RedirectToAction("Administrators");
        }

        [HttpGet]
        public IActionResult AdministratorInfo(int Id)
        {
            Administrator Administrator = dbContext.Administrators.Find(Id);
            return View(Administrator);
        }

        [HttpPost]
        public IActionResult AdministratorInfo() => RedirectToAction("Administrators");
    }
}
