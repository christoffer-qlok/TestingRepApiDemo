using System.Text.Json.Serialization;

namespace TestingRepApiDemo.Models.Dtos
{
    public class DogPictureDto
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
