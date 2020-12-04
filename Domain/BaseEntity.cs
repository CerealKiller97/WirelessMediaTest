using System.Text.Json.Serialization;
using NodaTime;

namespace Models
{
    public abstract class BaseEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("created_at")]
        public Instant CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public Instant? UpdatedAt { get; set; }
    }
}