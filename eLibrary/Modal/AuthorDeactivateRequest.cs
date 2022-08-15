using System.Text.Json.Serialization;

namespace eLibrary.Modal
{
    public class AuthorDeactivateRequest
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
