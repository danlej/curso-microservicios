namespace GetAdults.Models
{
    public class Adult
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? BirthYear { get; set; }
        public string? ImageUrl { get; set; }
    }
}