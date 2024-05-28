namespace Collections.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<Collections.Domain.Entities.Item> LatestItems { get; set; }
        public List<Collections.Domain.Entities.Collection> LargestCollections { get; set; }
    }
}