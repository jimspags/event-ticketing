using System.ComponentModel.DataAnnotations;

namespace API.Database.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        [MinLength(3), MaxLength(100)]
        public string Title { get; set; }
        [MinLength(3), MaxLength(200)]
        public string Description { get; set; }
        public double Price { get; set; }
        [MinLength(3), MaxLength(200)]
        public string Location { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [MinLength(3), MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        [MinLength(3), MaxLength(100)]
        public string? ModifiedBy { get; set; }
    }
}
