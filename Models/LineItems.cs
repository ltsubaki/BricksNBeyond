using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntexQueensSlay.Models
{
    public partial class LineItems
    {

        [Key]
        public int TransactionId { get; set; }
        public int ProductId { get; set; }

        // Other properties
        public int? Quantity { get; set; }
        public int? Rating { get; set; }
    }
}


