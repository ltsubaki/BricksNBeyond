using System.ComponentModel.DataAnnotations;

namespace IntexQueensSlay.Models
{
    public class Product
    {
        [Key]
        public int productId {  get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public int numParts { get; set; }
        public float price { get; set; }
        public string imgLink { get; set; }
        public string primaryColor { get; set; }
        public string secondaryColor { get; set; }
        public string description { get; set; }
        public string category { get; set; }
    }
}
