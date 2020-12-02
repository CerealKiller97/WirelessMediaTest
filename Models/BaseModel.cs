using NodaTime;

namespace Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public Instant CreatedAt { get; set; }
        public Instant? UpdatedAt { get; set; }
    }
}