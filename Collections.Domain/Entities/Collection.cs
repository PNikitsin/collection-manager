namespace Collections.Domain.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CollectionPicture { get; set; }

        public string ApplicationUserId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Item> Items { get; set; }
    }
}