using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class Book
    {
        [Key]
        public int id { get; set; }
        
        public decimal price { get; set; }
        
        public string isbn { get; set; }
    }
}