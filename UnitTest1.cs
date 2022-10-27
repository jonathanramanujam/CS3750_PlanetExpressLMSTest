using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS3750_PlanetExpressLMS.Models;
using CS3750_PlanetExpressLMS.Data;
using CS3750_PlanetExpressLMS.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CS3750_PlanetExpressLMS;

namespace CS3750_PlanetExpressLMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=PlanetExpress!!", null);
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "professor25@email.com")};
            //int n = _context.Course.Count();

            // Make sure a user is logged in
            //login.OnGet();
            //login.user = _context.User.FirstOrDefault(u => u.Email == "professor25@email.com");
            /*login.user.Email = "faketeacher@mail.com";
            login.user.Password = "password";*/
            login.OnPostAsync();

            CoursesModel courseMod = new CoursesModel(null, null);
            courseMod.OnGetAsync();
            courseMod.course.CourseNumber = 1234;
            courseMod.course.CourseName = "Test Course";
            courseMod.course.CourseLocation = "Nowhere";
            courseMod.course.StartDate = DateTime.Now;
            courseMod.course.EndDate = new DateTime(2023, 12, 25);
            courseMod.course.Days = "0000001"; 

            courseMod.OnPostAsync();

            int m = _context.Course.Count();
            //Assert.IsTrue(m == n + 1);
        }
    }
}