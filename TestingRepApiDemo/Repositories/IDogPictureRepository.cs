using Microsoft.EntityFrameworkCore;
using TestingRepApiDemo.Data;
using TestingRepApiDemo.Models;

namespace TestingRepApiDemo.Repositories
{
    public interface IDogPictureRepository
    {
        // Pictures
        void StorePicture(DogPicture picture);
        DogPicture? GetPicture(int pictureId);
        List<DogPicture> ListDogPictures();
        List<DogPicture> ListDogPicturesForTag(string tag);

        // Tags
        List<string> ListTags();
        void AddTag(Tag tag);
        Tag GetOrCreateTag(string name);
    }

    public class DbDogPictureRepository : IDogPictureRepository
    {
        private readonly ApplicationContext _context;

        public DbDogPictureRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public Tag GetOrCreateTag(string name)
        {
            Tag? tag = _context.Tags.Where(t => t.Name == name).SingleOrDefault();
            if(tag != null)
            {
                return tag;
            }
            tag = new Tag() { Name = name };
            AddTag(tag);
            return tag;
        }

        public DogPicture? GetPicture(int pictureId)
        {
            DogPicture? picture = _context.DogPictures.Where(d => d.Id == pictureId).SingleOrDefault();
            return picture;
        }

        public List<DogPicture> ListDogPictures()
        {
            return _context.DogPictures.ToList();
        }

        public List<DogPicture> ListDogPicturesForTag(string tag)
        {
            Tag? tagObject = _context.Tags
                .Where(t => t.Name == tag)
                .Include(t => t.DogPictures)
                .SingleOrDefault();

            if(tagObject == null)
            {
                return new List<DogPicture>();
            }

            return tagObject.DogPictures.ToList();
        }

        public List<string> ListTags()
        {
            return _context.Tags.Select(t => t.Name).ToList();
        }

        public void StorePicture(DogPicture picture)
        {
            _context.DogPictures.Add(picture);
            _context.SaveChanges();
        }
    }
}
