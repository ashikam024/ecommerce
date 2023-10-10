
using ECommerceProject1;
using ECommerceProject1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEE.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserManagementController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //private readonly UserManager<ApplicationUser> _userManager;

        //public UserManagementController(UserManager<ApplicationUser> userManager)
        //{
        //    _userManager = userManager;
        //}
        private UserManager<ApplicationUser> UserManager
        {
            get
            {
                // Make sure you have the correct namespace for ApplicationUserManager:
                // using e_commerce_clothing_store.Models;
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public ActionResult Index()
        {
            return View(db.Users.Where(C => C.UserType != "Admin").ToList());
        }

        public ActionResult BlockUser(string userId)
        {
            //string userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            if (user != null)
            {
                user.IsBlocked = true; // Set the IsBlocked property to true
                UserManager.Update(user); // Save the changes
            }

            return RedirectToAction("Index", "UserManagement");
        }

        public ActionResult UnblockUser(string userId)
        {
            var user = UserManager.FindById(userId);

            if (user != null)
            {
                user.IsBlocked = false; // Set the IsBlocked property to false
                UserManager.Update(user); // Save the changes
            }

            return RedirectToAction("Index", "UserManagement");
        }

    }
}

