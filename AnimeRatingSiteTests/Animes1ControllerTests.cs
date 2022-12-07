
using AnimeRatingSite.Controllers;
using AnimeRatingSite.Data;
using AnimeRatingSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnimeRatingSiteTests
{
    [TestClass]
    public class Animes1ControllerTests
    {
        private ApplicationDbContext context;
        Animes1Controller controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            context = new ApplicationDbContext(options);
            var genre = new Genre { GenreId = 42, Name = "A Genre" };
            context.Add(genre);

            for(var i = 50; i < 61; i++)
            {
                var anime = new Anime { AnimeId = i, Title = "Title: " + i.ToString(), Rating = 7.5, Description = "Desc for " + i.ToString(), Image = "", GenreId = 42, Genre = genre };
                context.Add(anime);
            }
            context.SaveChanges();

            controller = new Animes1Controller(context);
        }

        //Index
        #region "Index"
        [TestMethod]
        public void IndexLoadsView() 
        {
            //Arrange - in TestInitialize
            //Act
            var results = (ViewResult)controller.Index().Result;
            //Assert
            Assert.AreEqual("Index", results.ViewName);
        }

        [TestMethod]
        public void IndexLoadsAnimes1()
        {
            //Arrange - in TestInitialize
            //Act
            var result = (ViewResult)controller.Index().Result;
            List<Anime> model = (List<Anime>)result.Model;
            //Assert
            CollectionAssert.AreEqual(context.Anime.OrderBy(a => a.Title).ToList(), model);
        }
        #endregion


    }
}