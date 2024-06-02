using Collections.Web.ViewModels.Collection;
using Collections.Web.ViewModels.Item;

namespace Collections.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<ItemViewModel> LatestItems { get; set; }
        public IEnumerable<CollectionViewModel> LargestCollections { get; set; }
    }
}