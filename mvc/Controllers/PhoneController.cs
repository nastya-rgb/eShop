using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Models;
using mvc.Models.Common;
using mvc.Models.Product;

namespace mvc.Controllers
{
    public class PhoneController : Controller
    {
        private ShopContext _db;
        public PhoneController(ShopContext context)
        {
            _db = context;
        }
        [HttpGet]
        public IActionResult GetBrands()
        {
            var brands = _db.Brand.ToList();
            return Json(brands);
        }
        public IActionResult GetOS()
        {
            var os = _db.OS.ToList();
            return Json(os);
        }
        public class FilterPhone
        {
            public int Page { get; set; }
            public string Name { get; set; }
            public int Brand { get; set; }
            public int Year { get; set; }
            public int OS { get; set; }
            public int PriceFrom { get; set; }
            public int PriceTo { get; set; }
            public string Sort { get; set; }

        }
        [HttpGet]
        public IActionResult GetChartData()
        {
            var script = @"SELECT b.Name as Brand, Count(p.Id) as Count
            from Phones as p
            inner join Brand as b on b.Id = p.BrandId
            GROUP by b.Name
            order by b.Name";
            var lstChart =_db.PhoneChart.FromSqlRaw(script).ToList();
            var count =_db.Phones.Count();
            return Json( new{   lstChart, count});

          }


        [HttpGet]
        public IActionResult GetPhones([FromQuery] FilterPhone filter)
        {
            /* Thread.Sleep(2000);*/
            var compId = int.Parse(User.FindFirst(x => x.Type == "CompanyId").Value);
            var lstPhones = _db.Phones.Include(x => x.Brand).Include(x => x.OS)
                        .Where(x => x.CompanyId == compId);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                lstPhones = lstPhones.Where(x => x.Name.Contains(filter.Name));
            }
            if (filter.Brand > 0)
            {
                lstPhones = lstPhones.Where(x => x.BrandId == filter.Brand);
            }
            if (filter.OS > 0)
            {
                lstPhones = lstPhones.Where(x => x.OSId == filter.OS);
            }
            if (filter.Year > 0)
            {
                lstPhones = lstPhones.Where(x => x.Year == filter.Year);
            }
            if (filter.PriceFrom > 0)
            {
                lstPhones = lstPhones.Where(x => x.Price == filter.PriceFrom);
            }
            if (filter.PriceFrom > 0)
            {
                lstPhones = lstPhones.Where(x => x.Price == filter.PriceTo);
            }


            var count = lstPhones.Count();
            var totalPage = (int)Math.Ceiling(count / 10.0);
            if (string.IsNullOrEmpty(filter.Sort))
            {
                lstPhones = lstPhones.OrderBy(x => x.Id).Skip(10 * (filter.Page - 1)).Take(10);
            }
            else
            {
                switch (filter.Sort)
                {
                    case "brand":
                        lstPhones = lstPhones.OrderBy(x => x.Brand.Name);
                        break;
                    case "name":
                        lstPhones = lstPhones.OrderBy(x => x.Name);
                        break;
                    case "year":
                        lstPhones = lstPhones.OrderBy(x => x.Year);
                        break;
                    case "price":
                        lstPhones = lstPhones.OrderBy(x => x.Price);
                        break;
                    case "count":
                        lstPhones = lstPhones.OrderBy(x => x.Count);
                        break;
                    case "branddesc":
                        lstPhones = lstPhones.OrderByDescending(x => x.Brand.Name);
                        break;
                    case "namedesc":
                        lstPhones = lstPhones.OrderByDescending(x => x.Name);
                        break;
                    case "yeardesc":
                        lstPhones = lstPhones.OrderByDescending(x => x.Year);
                        break;
                    case "pricedesc":
                        lstPhones = lstPhones.OrderByDescending(x => x.Price);
                        break;
                    case "countdesc":
                        lstPhones = lstPhones.OrderByDescending(x => x.Count);
                        break;
                }
            }
            lstPhones = lstPhones.Skip(10 * (filter.Page - 1)).Take(10);



            var phones = lstPhones.Select(x => new
            {
                Brand = x.Brand.Name,
                Seria = x.Name,
                x.Year,
                OS = x.OS.Name,
                Color = x.Color.GetDescription(),
                x.Price,
                x.Count
            });
            var data = new { count, totalPage, phones };
            return Json(data);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
            var compId = int.Parse(User.FindFirst(x => x.Type == "CompanyId").Value);
            List<Phone> phones = new List<Phone>();
            using (var book = new XLWorkbook(file.OpenReadStream()))
            {
                var rows = book.Worksheet(1).RangeUsed().RowsUsed().Skip(1).ToList();
                foreach (var row in rows)
                {
                    var brand = row.Cell(1).GetValue<string>().Trim();
                    var name = row.Cell(2).GetValue<string>().Trim();
                    var os = row.Cell(3).GetValue<string>().Trim();
                    var color = row.Cell(4).GetValue<string>().Trim();
                    var price = row.Cell(5).GetValue<int>();
                    var count = row.Cell(6).GetValue<int>();
                    var year = row.Cell(7).GetValue<int>();

                    var Brand = _db.Brand.Single(x => x.Name == brand);

                    var OS = _db.OS.Where(x => x.Name == os).Single();

                    var phone = new Phone();
                    phone.UserId = userId;
                    phone.CompanyId = compId;
                    phone.Brand = Brand;
                    phone.Name = name;
                    phone.OS = OS;
                    Enum.TryParse(color, out Color ecolor);
                    phone.Color = ecolor;
                    phone.Price = price;
                    phone.Count = count;
                    phone.Year = year;
                    phones.Add(phone);


                }

            }
            _db.Phones.AddRange(phones);
            _db.SaveChanges();
            return Json("");
        }

    }
}