using System.ComponentModel.DataAnnotations;

namespace IntexQueensSlay.Models
{
    public partial class LineItems
    {
        [Key]
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Rating { get; set; }
    }
}
