using PSCS.Domain;
using System.Reflection;

namespace PSCS.OrderingSystem.Models
{
    public class ZoneSearchViewModel
    {
        public IList<Zone>? Zones { get;set;}
        public string? Search { get; set; }

    }
}
