using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    [Authorize]
     [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]  
    public class LayoutController:Controller
    {
        private ShopContext _db;
        public LayoutController(ShopContext context){
            _db=context;
        }
        [HttpGet]
        public IActionResult GetAvatar(){
            var userId = int.Parse(User.FindFirst(x => x.Type == "UserId").Value);
            var user =_db.Users.Find(userId);
            return Json(new {Avatar=user.Avatar, Login=user.Login});
        }
         
        
        
    }
}