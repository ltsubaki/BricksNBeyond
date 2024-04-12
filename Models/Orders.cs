using System.ComponentModel.DataAnnotations;

namespace IntexQueensSlay.Models
{
    public partial class Orders
    {
        [Key]
        public int TransactionId { get; set; }

        public int? CustomerId { get; set; }

        public string? Date { get; set; }

        public string? WeekDay { get; set; }

        public int? Time { get; set; }

        public string? EntryMode { get; set; }

        public int? Subtotal { get; set; }

        public string? TransactionType { get; set; }

        public string? Trans_Country { get; set; }

        public string? ShippingAddress { get; set; }

        public string? Bank { get; set; }

        public string? CardType { get; set; }

        public int? Fraud { get; set; }
    }

}
