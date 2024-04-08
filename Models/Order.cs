using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntexQueensSlay.Models
{
    public class Order
    {
        [Key]
        public int transactionId {  get; set; }
        [ForeignKey("customerId")]
        public int customerId { get; set; }
        public string date { get; set; }
        public string weekDay { get; set; }
        public string entryMode {  get; set; }
        public float subtotal { get; set; }
        public string transactionType { get; set; }
        public string transCountry { get; set; }
        public string shippingAddress { get; set; }
        public string time {  get; set; }
        public string bank {  get; set; }
        public string cardType { get; set; }
        public bool fraud { get; set; }
    }
}
