using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eLibrary.Modal
{
    public class BookDetailsModal
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? BookName { get; set; }
        public string? AuthourName { get; set; }
        public string? CoverPageURL { get; set; }
        public string? ISBN_Number { get; set; }


    }
}
