using System.ComponentModel.DataAnnotations;

namespace mvc.Models.Common
{
    public class Base
    {
        
        public int Id{get; set;}
        [Required]
        
        public string Name{get; set;}
        

    }
}