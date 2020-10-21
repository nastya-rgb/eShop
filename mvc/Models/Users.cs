using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using mvc.Models.Common;
using mvc.Models.ConpInfo;

namespace mvc.Models
{
    public class Users : Base
    {
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [RegularExpression("[0-9]{12}", ErrorMessage = "Введите имя из 12 чисел")]
        [MinLength(12), MaxLength(12)]
        [Required]

        public string IIN { get; set; }
        [Required]
        [MaxLength(10)]
        public string Login { get; set; }
        [Required]
        [MinLength(5), MaxLength(10)]
        public string Password { get; set; }
        public StatusWork StatusWork { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public string Mobile {get;set;}
        public string AboutMe {get;set;}
        public string Avatar{get;set;}
    }

    public enum StatusWork
    {
        [Description("Работает")]
        Work = 1,
        [Description("Отпуск")]
        Holiday = 2,
        [Description("Болеет")]
        Ill = 3,
        [Description("Выходной")]
        Weekend = 4
    }
}