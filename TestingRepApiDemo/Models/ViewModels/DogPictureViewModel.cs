using System.Text.Json.Serialization;

namespace TestingRepApiDemo.Models.ViewModels
{
    public class DogPictureViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
