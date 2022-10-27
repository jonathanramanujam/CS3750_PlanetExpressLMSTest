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
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "professor25@email.com")};
            int n = _context.Course.Count();

            SQLCourseRepository courseRepository = new SQLCourseRepository(_context);
            Course newCourse = new Course();
            newCourse.CreditHours = 4;
            newCourse.CourseNumber = 1234;
            newCourse.CourseName = "Test Course";
            newCourse.Department = "CD";
            newCourse.StartDate = DateTime.Now;
            newCourse.EndDate = new DateTime(2023, 12, 25);
            DateTime startTime = DateTime.Parse("10/22/2022 07:00:00 AM");
            startTime.ToString("HH:mm");
            newCourse.StartTime = startTime;
            DateTime endTime = DateTime.Parse("10/22/2022 05:00:00 PM");
            endTime.ToString("HH:mm");
            newCourse.EndTime = endTime;
            newCourse.UserID = 1;
            newCourse.CourseLocation = "Nowhere";
            newCourse.Days = "Sun";

            courseRepository.Add(newCourse);

            int m = _context.Course.Count();
            Assert.IsTrue(m == n + 1);
        }
    }
}