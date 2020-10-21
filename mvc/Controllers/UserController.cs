using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Controllers
{
    public class UserController : Controller
    {
        private IWebHostEnvironment _env;
        private ShopContext _db;
        public IActionResult Passport(){
            return View();
        }

        public UserController(ShopContext context, IWebHostEnvironment env)
        {
            _env = env;
            _db = context;
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUser()
        {
            var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
            var user = _db.Users.Include(x => x.Position).SingleOrDefault(x => x.Id == userId);
            var tUser = new { user.Id, user.Name, user.IIN, user.LastName, user.Mobile, user.AboutMe, Position = user.Position.Name, user.Avatar, user.Password };
            return Json(tUser);
        }
        [HttpPost]
        public IActionResult SaveAvatar (IFormFile image){
            string path="/Avatar/"+image.FileName;
            using (var filesStream = new FileStream(_env.WebRootPath+path, FileMode.Create)){
                image.CopyTo(filesStream);
            }
             var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
                var user = _db.Users.Find(userId);
                user.Avatar=path;
                _db.SaveChanges();
            return Json("Файл сохранен");

        }
        [HttpGet]
        public IActionResult GetUserData(){
             var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
                var user = _db.Users.Include(x=>x.Position).Include(x=>x.Company).Single(x=>x.Id==userId);
                var userData = new{user.Name, user.LastName,user.IIN,Position= user.Position.Name, user.Mobile, user.AboutMe,user.Avatar,
                Company=user.Company.Name, user.Company.XIN, user.Company.Director,user.Company.Phone,user.Company.Email, user.Company.Address};
                
            return Json(userData);
        }
        [HttpPost]
        public IActionResult SaveUser([FromBody] TUser us)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
                var user = _db.Users.Find(us.Id);
                user = us.Adapt(user);
                _db.SaveChanges();
                return Json(true);
            }else{
                return Json(false);
            }

        }

        public class TUser
        {

            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            public string LastName { get; set; }

            [MinLength(12), MaxLength(12)]
            public string IIN { get; set; }
            public string Mobile { get; set; }
            public string password { get; set; }
            public string AboutMe { get; set; }
        }



    }

}