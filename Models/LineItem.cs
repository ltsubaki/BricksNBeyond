using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntexQueensSlay.Models
{
    public partial class LineItem
    {
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Rating { get; set; }

        // Navigation properties for the related entities
        public virtual Order Transaction { get; set; }

        public virtual Product Product { get; set; }
    }
}

