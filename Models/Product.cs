using System.ComponentModel.DataAnnotations;

namespace IntexQueensSlay.Models
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public int? Year { get; set; }

        public int? NumParts { get; set; }

        public double? Price { get; set; }

        public string? ImgLink { get; set; }

        public string? PrimaryColor { get; set; }

        public string? SecondaryColor { get; set; }

        public string? Description { get; set; }

        public string? Category1 { get; set; }

        public string? Category2 { get; set; }

        public string? Category3 { get; set; }
    }

}
