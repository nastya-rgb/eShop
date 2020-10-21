using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Models;
using mvc.Models.Common;

namespace mvc.Controllers
{
//dsdsddccz

    [Authorize(Roles = "Директор")]
    public class CompanyController : Controller
    {
        private ShopContext _db;
        public CompanyController(ShopContext context)
        {
            _db = context;
        }
        [HttpGet]
        public IActionResult Employees()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetEmpoyees()
        {
            var compId = int.Parse(User.FindFirst(x => x.Type == "CompanyId").Value);
            var empl = _db.Users.Where(x => x.CompanyId == compId);
            var employees = empl.Select(x => new
            {
                x.LastName,
                x.Name,
                x.Login,
                x.IIN,
                x.Id,
                StatusWorkText = x.StatusWork.GetDescription(),
                x.StatusWork,
                Position = x.Position.Name,
                x.PositionId,
                x.Password,
                IsEdit = false
            });
            return Json(employees);
        }
        public IActionResult GetExcelEmployees()
        {

            var compId = int.Parse(User.FindFirst(x => x.Type == "CompanyId").Value);
            var users=_db.Users.Include(x=>x.Position).Where(x => x.CompanyId == compId);
           // var company = _db.Companies.Include(x => x.Employees).ThenInclude(x => x.Position).Single(x => x.Id == compId);

            DataTable dt = new DataTable();
            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("Имя", typeof(string));
            dt.Columns.Add("Фамилия", typeof(string));
            dt.Columns.Add("Логин", typeof(string));
            dt.Columns.Add("ИИН", typeof(string));
            dt.Columns.Add("Пароль", typeof(string));
            dt.Columns.Add("Статус", typeof(string));
            dt.Columns.Add("Позиция", typeof(string));
            int i = 0;
            foreach (var u in users)
            {
                i++;
                dt.Rows.Add(i, u.LastName, u.Name, u.Login, u.IIN, u.Password, u.StatusWork.GetDescription(), u.Position.Name);
            }

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(dt, "Cотрудники");
            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Сотрудники.xlsx");


            }
        }
        [HttpGet]
        public IActionResult GetPosition()
        {
            var position = _db.Positions.ToList();
            return Json(position);
        }
        public class TId
        {
            public int Id { get; set; }
        }
        [HttpPost]
        public IActionResult DeleteEmloyee([FromBody] TId tId)
        {
            var employee = _db.Users.Find(tId.Id);
            _db.Users.Remove(employee);
            _db.SaveChanges();
            return Json(true);
        }
        [HttpPost]
        public IActionResult SaveEmpoyee([FromBody] TEmployee empl)
        {
            if (ModelState.IsValid)
            {
                var compId = int.Parse(User.FindFirst(x => x.Type == "CompanyId").Value);
                if (empl.Id == 0)
                {
                    var employee = empl.Adapt<Users>();
                    employee.CompanyId = compId;
                    _db.Users.Add(employee);
                    try
                    {

                        _db.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        return Json("Не повторять иин и логин \n" + ex.InnerException.Message);
                    }

                }
                else
                {
                    var employee = _db.Users.Find(empl.Id);
                    employee = empl.Adapt(employee);
                    _db.SaveChanges();
                }



                return Json(true);
            }
            else
            {
                return Json("Данные имеют неверный формат");
            }
        }
        public class TEmployee
        {
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }
            [Required]
            public string LastName { get; set; }


            [Required]
            [RegularExpression("[0-9]{12}", ErrorMessage = "Введите ИИН из 12 чисел")]
            [MinLength(12), MaxLength(12)]
            public string IIN { get; set; }

            public int PositionId { get; set; }

            [Required]
            [MaxLength(10)]
            public string Login { get; set; }
            [Required]
            [MinLength(5), MaxLength(10)]
            public string Password { get; set; }

            public StatusWork StatusWork { get; set; }
        }

    }
}