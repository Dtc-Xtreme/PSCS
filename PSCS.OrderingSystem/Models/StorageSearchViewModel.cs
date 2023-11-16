using PSCS.Domain;
using System.Reflection;

namespace PSCS.OrderingSystem.Models
{
    public class StorageSearchViewModel
    {
        public IList<Storage>? Storages { get;set;}
        public string? Search { get; set; }

    }
}
