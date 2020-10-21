using System;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class RegistrationController : Controller
    {
        private ShopContext _db { get; }

        public RegistrationController(ShopContext context)
        {
            _db = context;

        }
        public ViewResult Index()
        {
            return View();
        }

        public class TData
        {
            public TCompany Company { get; set; }
            public TDirector Director { get; set; }
        }
        public class TCompany
        {
            public string Name { get; set; }
            public string XIN { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }
        public class TDirector
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string IIN { get; set; }
            public string Mobile { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string AboutMe { get; set; }

        }
        [HttpPost]
        public JsonResult Registr([FromBody] TData dt)
        {

            var director = dt.Director.Adapt<Users>();
            var dirErr=_db.Users.SingleOrDefault(x=>x.IIN== director.IIN);
            if(dirErr!=null){
                return Json("Уже есть пользователь с таким ИИНом");
            }
            dirErr=_db.Users.SingleOrDefault(x=>x.Login== director.Login);
            if(dirErr!=null){
                return Json("Уже есть пользователь с таким логином");
            }
             dirErr=_db.Users.SingleOrDefault(x=>x.Name== director.Name &&x.LastName== director.LastName );
            if(dirErr!=null){
                return Json("Уже есть пользователь с таким именем и фамилеей");
            }
            

            var company = dt.Company.Adapt<Company>();
            var compErr =_db.Companies.SingleOrDefault(x=>x.XIN== company.XIN);
            if(compErr!=null){
                return Json("Уже есть компаниия с таким названием");
            }
            company.Director = director.Name + " " + director.LastName;

            director.Company = company;
            director.PositionId = 4;
            director.StatusWork = StatusWork.Work;
            _db.Companies.Add(company);
            _db.Users.Add(director);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);

            }
            return Json("Данные сохранены ");

        }

    }
}