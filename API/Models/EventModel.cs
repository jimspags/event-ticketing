namespace API.Models
{
    public record EventModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
