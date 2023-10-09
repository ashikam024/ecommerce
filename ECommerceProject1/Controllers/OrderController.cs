using ECommerceProject1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceProject1.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        public ActionResult Index()
        {
            // Get the currently logged-in user's ID
            string userId = User.Identity.GetUserId();

            // Query the Order table to retrieve all orders for the logged-in user
            var userOrders = db.Orders.Where(o => o.UserId == userId).ToList();

            // You can pass the list of user orders to a view for rendering
            return View(userOrders);
        }     
        public ActionResult OrderHistory()
        {            
            return View(db.Orders.ToList());
        }
    }
}