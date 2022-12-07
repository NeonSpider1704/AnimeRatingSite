
using AnimeRatingSite.Controllers;
using AnimeRatingSite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AnimeRatingSiteTests
{
    
    [TestClass]
    public class Animes1ControllerTests
    {
        [TestMethod]
        public void IndexLoadsView() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new ApplicationDbContext(options);
            var controller = new Animes1Controller(context);

            var results = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", results.ViewName);
        }
    }
}