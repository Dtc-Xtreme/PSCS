using PSCS.Domain;
using System.Reflection;

namespace PSCS.OrderingSystem.Models
{
    public class ProductSearchViewModel
    {
        public IList<Product>? Products { get;set;}
        public string? Search { get; set; }

    }
}
