using System.ComponentModel.DataAnnotations;

namespace IntexQueensSlay.Models
{
    public class Customer
    {
        [Key]
        public int customerId {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string resCountry { get; set; }
        public string gender { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        public bool admin { get; set; }
    }
}
