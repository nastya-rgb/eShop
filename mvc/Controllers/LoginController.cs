using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        private ShopContext _db;

        [HttpGet]
        public IActionResult Exit(){
            HttpContext.SignOutAsync();
            return RedirectToAction("Enter", "Login");
        }
        public LoginController(ShopContext context)
        {
            _db = context;
        }
        public IActionResult Enter()
        {
            return View();
        }
        [HttpGet]
         public IActionResult Role(){
             return View();
         }
         [HttpGet]
         public IActionResult LockUser(int id){
              HttpContext.SignOutAsync();
             var user =_db.Users.Find(id);
             return View(user);
         }
         [HttpPost]
public IActionResult LockUser(string Login, string Password){
    var user = _db.Users.Include(x=>x.Position).FirstOrDefault(x=>x.Login==Login && x.Password==Password);
    if(user==null){
        return RedirectToAction("Enter", "Login");
    }else{
        var claims = new List<Claim>{
                            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Position.Name),

                            new Claim("UserId", user.Id.ToString()),
                            new Claim("CompanyId", user.CompanyId.ToString())
                            };
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
       return RedirectToAction("Index", "Home");
    }
  
}

         [HttpGet]
         public IActionResult GetUserName(){
            var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
            var user = _db.Users.Find(userId);
             return Json(user.Name);
         }

        [HttpPost]
        public  IActionResult Enter(TUser tuser)
        {


            if (ModelState.IsValid)
            {
                var user = _db.Users.Include(x=>x.Position).SingleOrDefault(x => x.Login == tuser.Login && x.Password == tuser.Password);
                if (user != null)
                {
                    var claims = new List<Claim>{
                            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Position.Name),

                            new Claim("UserId", user.Id.ToString()),
                            new Claim("CompanyId", user.CompanyId.ToString())
                            };
                    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Enter", "Login");
        }
        public class TUser
        {
            [Required]
            [MaxLength(10)]
            public string Login { get; set; }
            [Required]
            [MinLength(5), MaxLength(10)]
            public string Password { get; set; }
        }
    }
}