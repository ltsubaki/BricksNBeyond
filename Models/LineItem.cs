using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntexQueensSlay.Models
{
    public class LineItem
    {
        [Key]
        [Column(Order = 1)] // This sets the order of composite keys (optional)
        public int transactionId { get; set; }

        [Key]
        [Column(Order = 2)] // This sets the order of composite keys (optional)
        public int productId { get; set; }
        public int quantity { get; set; }
        public int rating { get; set; }
    }
}
