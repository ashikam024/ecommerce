using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceProject1.Models;
using ECommerceProject1.ViewModel;
using Microsoft.AspNet.Identity;

namespace ECommerceProject1.Controllers
{
    public class ProductDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductDetail
        public ActionResult Index(int? id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                // Handle the case where the product doesn't exist
                return HttpNotFound();
            }
            var sizeOptions = new List<string> { "S", "M", "L", "XL", "XXL" };
            var viewModel = new ProductViewModel
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Size = string.Join(",", sizeOptions),
                ProductId = product.Id,

            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddToCart(ProductViewModel viewModel)
        {
            string userId = User.Identity.GetUserId();
            // Create a CartItem entity from the view model
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = viewModel.ProductId,
                ImageUrl = viewModel.ImageUrl,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                Total = viewModel.Total,
                Size = viewModel.Size
            };
            // Add the cart item to the database
            db.CartItems.Add(cartItem);
            db.SaveChanges();

            return RedirectToAction("Show"); // Assuming you have a Cart action to display the cart
        }
        public ActionResult Show()
        {
            string userId = User.Identity.GetUserId();
            // Fetch cart items for the logged-in user
            var cartItems = db.CartItems.Where(c => c.UserId == userId).ToList();

            return View(cartItems);
        }
        [HttpPost]
        public ActionResult Show(List<CartItem> cart)
        {
            string userId = User.Identity.GetUserId();
            string username = User.Identity.GetUserName();
            var cartItems = db.CartItems.Where(c => c.UserId == userId).ToList();
            foreach (var item in cartItems)
            {
                var order = new Order
                {
                    UserId = userId,
                    UserName = username,                    
                    ProductId = item.ProductId,                    
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = item.Total
                };
                db.Orders.Add(order);
                var product = db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Availability -= item.Quantity;
                }
                else
                {
                    Console.WriteLine("unavailabile");
                }
            }
            // Save changes to the database
            db.SaveChanges();
           
            db.CartItems.RemoveRange(cartItems);
            db.SaveChanges();

            // Redirect to the "Show" action
            return RedirectToAction("Shipment");
        }
        public ActionResult Remove(int id)
        {
            string userId = User.Identity.GetUserId();
            var itemToRemove = db.CartItems.FirstOrDefault(item => item.Id == id && item.UserId == userId);
            if (itemToRemove != null)
            {
                db.CartItems.Remove(itemToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Show");
            //return View(db.CartItems.Where(x=>x.UserId==userId));
        }
        [HttpGet]
        public ActionResult Shipment()
        {
            return View();
        }
        [HttpPost]    
        public ActionResult Shipment(ShipmentViewModel viewmodel)
        {
            string userId = User.Identity.GetUserId();
            var shipment = new ShipmentModel
            {
                UserId = userId,
                Address = viewmodel.Address,
                City = viewmodel.City,
                State = viewmodel.State,
                Country = viewmodel.Country               
            };
            db.ShipmentModels.Add(shipment);
            db.SaveChanges();
            return RedirectToAction("PaymentOption");
        }
        public ActionResult PaymentOption()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PaymentOption(Payment model)
        {
            if (ModelState.IsValid)
            {                
                db.Payments.Add(model);
                db.SaveChanges();
                // Redirect to a success page or perform other actions
                return RedirectToAction("Success");
            }
            // If the model is not valid, return to the payment form
            return View("PaymentOption", model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}