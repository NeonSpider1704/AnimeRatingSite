
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

        //Details
        #region "Details"
        [TestMethod]
        public void DetailsNoIdLoads404()
        {
            //Arrange - in TestInitialize
            //Act
            var results = (ViewResult)controller.Details(null).Result;
            //Assert
            Assert.AreEqual("404", results.ViewName);
        }
        
        [TestMethod]
        public void DetailsNoAnimesTableLoads404()
        {
            //Arrange
            context.Anime = null;
            //Act
            var result = (ViewResult)controller.Details(null).Result;
            //Assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Details(30).Result;
            //Assert
            Assert.AreEqual("404", result.ViewName);
        }
        
        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            //Act
            var result = (ViewResult)controller.Details(80).Result;
            //Assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsAnime()
        {
            //Act
            var result = (ViewResult)controller.Details(80).Result;
            //Assert
            Assert.AreEqual(context.Anime.Find(80), result.Model);
        }
        #endregion

        //Create
        #region "Create"
        [TestMethod]
        public void Create()
        {
            //Act
            var results = (ViewResult)controller.Create();
            //Assert
            Assert.AreEqual("Create",results.ViewName);
        }
        /**
        public void CreateImageValid()
        {
            //Act
            var results = (ViewResult)controller.Create();
            //Assert
            Assert.AreEqual("Create", results.ViewName);
        }
        **/
        #endregion

        //Edit
        #region "Edit"
        [TestMethod]
        public void EditNoIdLoads404()
        {
            //Arrange - in TestInitialize
            //Act
            var results = (ViewResult)controller.Edit(null).Result;
            //Assert
            Assert.AreEqual("404", results.ViewName);
        }

        [TestMethod]
        public void EditNoAnimesTableLoads404()
        {
            //Arrange
            context.Anime = null;
            //Act
            var result = (ViewResult)controller.Edit(null).Result;
            //Assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Edit(30).Result;
            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            //Act
            var result = (ViewResult)controller.Edit(80).Result;
            //Assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsAnime()
        {
            //Act
            var result = (ViewResult)controller.Edit(80).Result;
            //Assert
            Assert.AreEqual(context.Anime.Find(80), result.Model);
        }
        #endregion

        //Delete
        #region "Edit"
        [TestMethod]
        public void DeleteNoIdLoads404()
        {
            //Arrange - in TestInitialize
            //Act
            var results = (ViewResult)controller.Delete(null).Result;
            //Assert
            Assert.AreEqual("404", results.ViewName);
        }

        [TestMethod]
        public void DeleteNoAnimesTableLoads404()
        {
            //Arrange
            context.Anime = null;
            //Act
            var result = (ViewResult)controller.Delete(null).Result;
            //Assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Delete(30).Result;
            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            //Act
            var result = (ViewResult)controller.Delete(80).Result;
            //Assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsAnime()
        {
            //Act
            var result = (ViewResult)controller.Delete(80).Result;
            //Assert
            Assert.AreEqual(context.Anime.Find(80), result.Model);
        }
        #endregion
    }
}