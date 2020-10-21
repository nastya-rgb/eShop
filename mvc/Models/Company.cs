using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using mvc.Models.Common;

namespace mvc.Models
{
    public class Company:Base
    {
        [Required]
        [MinLength(12), MaxLength(12)]
         public string XIN{get;set;}
         [Required]
         public string Director {get;set;}
         public string Address{get; set;}
         public string Phone{get; set;}
         public string Email{get;set;}
         public string RedistrDocPath{get;set;}

        public ICollection<Users> Employees{get;set;}

    }
}