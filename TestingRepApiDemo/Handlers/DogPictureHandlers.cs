using System.Net;
using TestingRepApiDemo.Models;
using TestingRepApiDemo.Models.Dtos;
using TestingRepApiDemo.Models.ViewModels;
using TestingRepApiDemo.Repositories;

namespace TestingRepApiDemo.Handlers
{
    public static class DogPictureHandlers
    {
        public static IResult AddDogPicture(IDogPictureRepository repository, DogPictureDto picture, string tag)
        {
            try
            {
                Tag tagObject = repository.GetOrCreateTag(tag);
                DogPicture dbPicture = new DogPicture()
                {
                    Title = picture.Title,
                    Url = picture.Url,
                    Tag = tagObject,
                };
                repository.StorePicture(dbPicture);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error in AddDogPicture(): {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult ListTags(IDogPictureRepository repository)
        {
            try
            {
                return Results.Json(repository.ListTags());
            } catch(Exception ex)
            {
                Console.WriteLine($"Error in ListTags(): {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        public static IResult GetPicturesForTag(IDogPictureRepository repository, string tag)
        {
            try
            {
                var pictures = repository.ListDogPicturesForTag(tag);
                return Results.Json(pictures.Select(p => new DogPictureViewModel()
                {
                    Title = p.Title,
                    Url = p.Url,
                }));
            } catch(Exception ex)
            {
                Console.WriteLine($"Error in GetPicturesForTag(): {ex}");
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
