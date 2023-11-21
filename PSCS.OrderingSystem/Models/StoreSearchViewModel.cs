using PSCS.Domain;

namespace PSCS.OrderingSystem.Models
{
    public class StoreSearchViewModel
    {
        public IList<Product>? Products { get; set; }
        public string? Search { get; set; }
    }
}
