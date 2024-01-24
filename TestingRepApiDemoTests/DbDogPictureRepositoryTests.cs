using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingRepApiDemo.Data;
using TestingRepApiDemo.Models;
using TestingRepApiDemo.Repositories;

namespace TestingRepApiDemoTests
{
    [TestClass]
    public class DbDogPictureRepositoryTests
    {
        [TestMethod]
        public void GetOrCreateTag_CreatesTagIfNoneExists()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetOrCreateTag_CreatesTagIfNoneExists")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IDogPictureRepository repository = new DbDogPictureRepository(context);

            // Act
            Tag result = repository.GetOrCreateTag("test-tag");

            // Assert
            Assert.AreEqual("test-tag", result.Name);
            Assert.AreEqual(1, context.Tags.Count());
            Assert.AreEqual("test-tag", context.Tags.Single().Name);
        }

        [TestMethod]
        public void GetOrCreateTag_FetchesTagIfExists()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("GetOrCreateTag_FetchesTagIfExists")
                .Options;

            ApplicationContext context = new ApplicationContext(options);
            IDogPictureRepository repository = new DbDogPictureRepository(context);

            context.Tags.Add(new Tag()
            {
                Name = "test-tag",
            });
            context.SaveChanges();

            // Act
            Tag result = repository.GetOrCreateTag("test-tag");

            // Assert
            Assert.AreEqual("test-tag", result.Name);
            Assert.AreEqual(1, context.Tags.Count());
        }
    }
}
