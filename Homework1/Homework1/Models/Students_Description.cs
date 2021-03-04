using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class Students_Description
    {
        [Key]
        public int students_description_id { get; set; }

        public int students_id { get; set; }

        public int age { get; set; }

        public string firstname { get; set; }
        
        public string lastname { get; set; }
        
        public string adress { get; set; }
        
        public string country { get; set; }
    }
}