using PSCS.Domain;

namespace PSCS.OrderingSystem.Models
{
    public class ConfirmViewModel
    {
        public List<OrderLine>? OrderLines { get; set; }

        public int SupplierId { get; set; }
    }
}
