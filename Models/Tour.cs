// Models/Tour.cs
using System.Collections.Generic;

namespace LoginRegistrationMVC.Models
{
    public class Tour
    {
        public string TourName { get; set; }
        public string Place { get; set; }
        public List<string> Photos { get; set; } // Store the photo URLs
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ContactNumber { get; set; }
        public double Rating { get; set; }
    }
}