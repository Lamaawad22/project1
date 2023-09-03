using homework1.Data;
using homework1.Models; 
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using products = homework1.Data.products;

namespace homework1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext Context;
        public HomeController(ApplicationDbContext Context)
        {
            Context = Context;

        }


        [Authorize]
        public IActionResult Index()
        {
            var Name = HttpContext.User.Identity.Name;
            //CookieOptions options = new CookieOptions(); // عملية انشاء الكوكيز - جهة العميل
            //options.Expires = DateTime.Now.AddMinutes(10);
            //Response.Cookies.Append("Name", Name, options);

            //HttpContext.Session.SetString("Name", Name); // عملية انشاء السيشن - جهة السيرفر
            TempData["Name"] = Name; // عملية انشاء التيمب داتا - جهة العميل
            var product = Context.products.ToList();
            ViewBag.Name = Name;
            return View(product);
        }

        [HttpPost]
        public IActionResult Index(string productname)
        {
            var producsearch = Context.products.Where(x => x.productName.Contains(productname)).ToList();

            return View(producsearch);
        }

        public IActionResult AddProduct(products product)
        {
            Context.products.Add(product);
            Context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var product = Context.products.SingleOrDefault(p => p.id == id);
            if (product != null)
            {
                Context.products.Remove(product);
                Context.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public IActionResult DeleteDetails(int id)
        {
            var product = Context.productDetails.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                Context.productDetails.Remove(product);
                Context.SaveChanges();
            }
            return RedirectToAction("ProductDetails");

        }


        public IActionResult Edit(int id)
        {
            var product = Context.products.SingleOrDefault(p => p.id == id);
            return View(product);
        }

        public IActionResult UpdateProducts(products product) // Way 1
        {
            Context.products.Update(product);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        /*
			   public IActionResult UpdateProducts(Product product) // Way 2
			   {
				   Product productupdate = context.Product.SingleOrDefault(p => p.Id == product.Id) ?? new Product();
				   if(productupdate != null) 
				   {
					   productupdate.ProductName = product.ProductName;
				   }
				   context.SaveChanges();
				   return RedirectToAction("Index");

			   }

			   */


        public IActionResult ProductDetails()
        {
            var product = Context.products.ToList();
            var ProductDetails = Context.productDetails.ToList();
            ViewBag.ProductDetails1 = ProductDetails;
            /*ViewBag.Name = Request.Cookies["Name"];*/ // عملية استرجاع البيانات الكوكيز
                                                        //ViewBag.Name = HttpContext.Session.GetString("Name"); // عملية استرجاع البيانات السيشن 
            ViewBag.Name = TempData["Name"]; // عملية استرجاع البيانات عن طريق TempData
            return View(product);
        }

        [HttpPost]
        public IActionResult ProductDetails(int id)
        {

            var ProductDetails = Context.productDetails.Where(p => p.ProductId == id).ToList();
            var product = Context.products.ToList();
            ViewBag.ProductDetails1 = ProductDetails;
            return View(product);
        }


        public IActionResult AddProductDetails(productDetails productdetails)
        {

            Context.productDetails.Add(productdetails);
            Context.SaveChanges();
            return RedirectToAction("ProductDetails");

        }

        public IActionResult PaymentAccept()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PaymentAccept(PaymentAccept paymentaccept)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}

