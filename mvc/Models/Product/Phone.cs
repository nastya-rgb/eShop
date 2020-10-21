using System;
using System.Collections.Generic;
using System.ComponentModel;
using mvc.Models.Common;

namespace mvc.Models.Product
{
    public class Phone:Base
    {
        public int Year{get;set;}
        public int Price {get;set;}
        public int Count {get;set;}
         public Color Color {get;set;}
         public int BrandId{get;set;}
          public Brand Brand {get;set;}
          public int OSId{get;set;}
          public OS OS {get;set;}
          public Company Company {get;set;}
          public int CompanyId{get;set;}
          public int? UserId{get;set;}

          public Users User{get;set;}


    }
    public class Brand:Base{
        public ICollection<Phone> Phones{get;set;}

    }
    public class OS:Base{
         public ICollection<Phone> Phones{get;set;}
        
    }
    public enum Color{
        [Description("Красный")]
        Red=1,
        [Description("Зеленый")]
        Green=2,
        [Description("Голубой")]
        Blue=3,
        [Description("Золотой")]
        Gold=4,
        [Description("Серебрянный")]
        Silver=5,
        [Description("Черный")]
        Black=6
    }
}