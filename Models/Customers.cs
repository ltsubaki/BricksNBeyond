﻿using System.ComponentModel.DataAnnotations;

namespace IntexQueensSlay.Models
{
    public partial class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? BirthDate { get; set; }

        public string? ResCountry { get; set; }

        public string? Gender { get; set; }

        public int? Age { get; set; }

        //public int? Id { get; set; }
    }
}
