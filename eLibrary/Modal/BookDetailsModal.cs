using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eLibrary.Modal
{
    public class BookDetailsModal
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonPropertyName("bookname")]
        public string? BookName { get; set; }

        [JsonPropertyName("authorname")]
        public string? AuthourName { get; set; }
        [JsonPropertyName("imgPath")]
        public string? CoverPageURL { get; set; }
    }
}
