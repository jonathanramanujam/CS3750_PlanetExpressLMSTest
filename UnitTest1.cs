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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Moq;

namespace CS3750_PlanetExpressLMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void canAddCourseTest()
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

        [TestMethod]
        public void canCreateAssignmentTest()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "professor25@email.com") };
            int n = _context.Assignment.Count();

            SQLAssignmentRepository assignmentRepo = new SQLAssignmentRepository(_context);
            Assignment assAdd = new Assignment();
            assAdd.Name = "Test Assignment";
            assAdd.SubmissionType = "FILE";
            assAdd.PointsPossible = 10;
            assAdd.OpenDateTime = new DateTime(2022, 10, 21, 23, 59, 00);
            assAdd.CloseDateTime = new DateTime(2022, 12, 21, 23, 59, 00);
            assAdd.CourseID = _context.Course.FirstOrDefault(c => c.UserID == login.user.ID).ID;
            assAdd.Description = "This assignment fake AF";
            assignmentRepo.Add(assAdd);


            //if there is no test course to delete, this will fail, just run createCourse before it
            int m = _context.Assignment.Count();
            Assert.IsTrue(m == n + 1);
        }

        [TestMethod]
        public void deleteAssignmentTest()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            Mock<IWebHostEnvironment> _environment = new Mock<IWebHostEnvironment>();

            _environment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            //Set up login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "professor25@email.com") };
            int n = _context.Assignment.Count();

            SQLSubmissionRepository subRepo = new SQLSubmissionRepository(_context, _environment.Object);
            SQLAssignmentRepository assRepo = new SQLAssignmentRepository(_context, subRepo);

            //There are so many test assignments this shouldn't be a problem, but maybe eventually..
            if (_context.Assignment.FirstOrDefault(a => a.Name == "Test Assignment") == null)
            {
                canCreateAssignmentTest();
                n = _context.Assignment.Count();
            }

            Assignment assToDelete = _context.Assignment.FirstOrDefault(a => a.Name == "Test Assignment");
            assRepo.Delete(assToDelete.ID);

            //if there is no test course to delete, this will fail, just run createCourse before it
            int m = _context.Assignment.Count();
            Assert.IsTrue(m == n - 1);

        }

        [TestMethod]
        public void canDeleteCoursesTest()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "professor25@email.com") };
            int n = _context.Course.Count();

            SQLAssignmentRepository assignmentRepository = new SQLAssignmentRepository(_context);
            SQLEnrollmentRepository enrollmentRepository = new SQLEnrollmentRepository(_context);

            SQLCourseRepository courseRepository = new SQLCourseRepository(_context, assignmentRepository, enrollmentRepository);


            if(_context.Course.FirstOrDefault(c => c.CourseName == "Test Course") == null)
            {
                canAddCourseTest();
                n = _context.Course.Count();
            }
            Course courseToDelete = _context.Course.FirstOrDefault(c => c.CourseName == "Test Course");
            courseRepository.Delete(courseToDelete.ID);

            //if there is no test course to delete, this will fail, just run createCourse before it
            int m = _context.Course.Count();
            Assert.IsTrue(m == n - 1);
        }

        [TestMethod]
        public void registerForCourseTest()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com") };
            int n = _context.Enrollment.Count();

            SQLEnrollmentRepository enrollRepo = new SQLEnrollmentRepository(_context);
            Enrollment newEnrollment = new Enrollment();

            //add a course the test user is not enrolled in
            newEnrollment.CourseID = _context.Enrollment.FirstOrDefault(e => e.UserID != login.user.ID).CourseID;
            newEnrollment.UserID = login.user.ID;
            enrollRepo.Add(newEnrollment);

            //will fail if the user is in every course, perhaps run deregisterforcoursetest?
            int m = _context.Enrollment.Count();
            Assert.IsTrue(m == n + 1);
        }

        [TestMethod]
        public void deregisterForCourseTest()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com") };
            int n = _context.Enrollment.Count();

            SQLEnrollmentRepository enrollRepo = new SQLEnrollmentRepository(_context);
            Enrollment dropEnroll = new Enrollment();
            if(_context.Enrollment.FirstOrDefault(e => e.UserID == login.user.ID) != null)
            {
                dropEnroll = _context.Enrollment.FirstOrDefault(e => e.UserID == login.user.ID);
                enrollRepo.Delete(dropEnroll.ID);
            }

            //will fail if the user is not registered for any courses
            //perhaps run the registerForCourseTest?
            int m = _context.Enrollment.Count();
            Assert.IsTrue(m == n - 1);
        }

        [TestMethod]
        public void canCreatePayment()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            SQLUserRepository userRepo = new SQLUserRepository(_context);

            // Get the user
            User user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com");

            // Retrieve the old names
            string oldFirstName = user.FirstName;
            string oldLastName = user.LastName;

            // Alternate between Real Scholar and Fake Student, and make sure it changes.
            if (oldFirstName == "Real" || oldLastName == "Scholar")
            {
                user.FirstName = "Fake";
                user.LastName = "Student";

                userRepo.Update(user);

                user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com");

                Assert.AreEqual("Fake", user.FirstName);
                Assert.AreEqual("Student", user.LastName);
            }
            else // if name is fake student or anything else
            {
                user.FirstName = "Real";
                user.LastName = "Scholar";

                userRepo.Update(user);

                user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com");

                Assert.AreEqual("Real", user.FirstName);
                Assert.AreEqual("Scholar", user.LastName);
            }
        }

        [TestMethod]
        public void canEditProfileNameTest()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "studentstudent2@mail.com") };
            int n = _context.Payment.Count();

            SQLPaymentRepository paymentRepo = new SQLPaymentRepository(_context);
            Payment payAdd = new Payment();
            payAdd.FirstName = "Test";
            payAdd.LastName = "Test";
            payAdd.CardNumber = "4242424242424242";
            payAdd.Cvv = "123";
            payAdd.ExpDate = new DateTime(2023, 12, 21, 23, 59, 00);
            payAdd.ID = login.user.ID;

            paymentRepo.Add(payAdd);
            //if there is no test course to delete, this will fail, just run createCourse before it
            int m = _context.Payment.Count();
            Assert.IsTrue(m == n + 1);
        }

        [TestMethod]
        public void canSubmitAssignment()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null) { user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com") };
            int n = _context.Submission.Count();

            //Mock environment (thank you Brooks)
            Mock<IWebHostEnvironment> _environment = new Mock<IWebHostEnvironment>();
            _environment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            

            SQLSubmissionRepository subRepo = new SQLSubmissionRepository(_context, _environment.Object);
            Submission newsub = new Submission();

            //gotta get a course the student is in, then an assignment in that course
            int courseID = Convert.ToInt32(_context.Enrollment.FirstOrDefault(e => e.UserID == login.user.ID).CourseID);
            int assID = Convert.ToInt32(_context.Assignment.FirstOrDefault(a => a.CourseID == courseID).ID);

            newsub.SubmissionTime = DateTime.Now;
            newsub.AssignmentID = assID;
            newsub.UserID = login.user.ID;

            //chose this because it's already here. Don't really wanna go all out on some file upload code if i dont have to...
                //.txt files work for both file upload and txt box submissions...
            newsub.Path = "wwwroot\\submissions\\Text_StuDent.txt"; 
            newsub.Grade = null;

            //add the assignment
            subRepo.Add(newsub);

            //check that the submission went in
            int m = _context.Submission.Count();
            Assert.IsTrue(m == n + 1);
        }

        [TestMethod]
        public void canEditAddresses()
        {
            DbContextOptions<CS3750_PlanetExpressLMSContext> options = new DbContextOptions<CS3750_PlanetExpressLMSContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMS_Planet;Persist Security Info=True;User ID=LMS_Planet;Password=Planetexpress!!");
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            SQLUserRepository userRepo = new SQLUserRepository(_context);

            // Get the user
            User user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com");

            // Retrieve the old names
            string addy1 = user.Address1;
            string addy2 = user.Address2;

            // Alternate from Walmart & McD's to Temple Square & Tabernacle, and make sure it changes.
            if (addy1 == "1632 N 2000 W" || addy2 == "1959 Wall Ave")
            {
                user.Address1 = "2145 Washington Blvd";
                user.Address2 = "North West Temple Street";

                userRepo.Update(user);

                user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com");

                Assert.AreEqual("2145 Washington Blvd", user.Address1);
                Assert.AreEqual("North West Temple Street", user.Address2);
            }
            else // if addy is Walmart, Mcds or anything else
            {
                user.Address1 = "1632 N 2000 W";
                user.Address2 = "1959 Wall Ave";

                userRepo.Update(user);

                user = _context.User.FirstOrDefault(u => u.Email == "fakestudent@mail.com");

                Assert.AreEqual("1632 N 2000 W", user.FirstName);
                Assert.AreEqual("1959 Wall Ave", user.LastName);
            }
        }
    }
}
