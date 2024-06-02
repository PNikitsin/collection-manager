namespace Collections.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }

        public int ItemId { get; set; }
    }
}