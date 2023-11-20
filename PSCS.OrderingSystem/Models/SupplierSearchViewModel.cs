using PSCS.Domain;
using System.Reflection;

namespace PSCS.OrderingSystem.Models
{
    public class SupplierSearchViewModel
    {
        public IList<Supplier>? Suppliers { get;set;}
        public string? Search { get; set; }

    }
}
