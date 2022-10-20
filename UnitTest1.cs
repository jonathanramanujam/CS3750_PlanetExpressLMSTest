using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS3750_PlanetExpressLMS.Models;
using CS3750_PlanetExpressLMS.Data;
using CS3750_PlanetExpressLMS.Pages;

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
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Your connection string;", null);
            var _context = new CS3750_PlanetExpressLMSContext((DbContextOptions<CS3750_PlanetExpressLMSContext>)builder.Options);

            //setup: login
            LoginModel login = new LoginModel(null);
            login.user = _context.User.Where(u => u.Email == "faketeacher@mail.com");
            login.OnGet();
            login.user.Email = "faketeacher@mail.com";
            login.user.Password = "Password123";
            login.OnPostAsync();

            CoursesModel courseMod = new CoursesModel(null, null);
            courseMod.OnGetAsync();

            courseMod.
        }
    }
}