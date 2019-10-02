using PIMM.Models;

namespace PIMM.ViewModels
{
    public class AmountPerCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}