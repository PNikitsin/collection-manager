namespace Collections.Domain.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CollectionPicture { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Author { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Item> Items { get; set; }
    }
}