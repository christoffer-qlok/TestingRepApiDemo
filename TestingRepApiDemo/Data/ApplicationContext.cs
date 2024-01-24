using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using TestingRepApiDemo.Models;

namespace TestingRepApiDemo.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<DogPicture> DogPictures { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    }
}
