namespace Collections.Web.ViewModels
{
    public class CreateItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CollectionId { get; set; }
    }
}