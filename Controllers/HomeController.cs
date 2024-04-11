using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using IntexQueensSlay.Models;
using IntexQueensSlay.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntexQueensSlay.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private ISlayRepository _repo;

        public HomeController(ISlayRepository temp)
        {
            _repo = temp;
        }

        public ActionResult Index()
        {
            if (!HasCookieConsent())
            {
                ViewBag.ShowConsentBanner = true;
            }
            return View();
        }

        private bool HasCookieConsent()
        {
            if (Request.Cookies["cookieConsent"] != null)
            {
                // Compare the cookie value directly
                return Request.Cookies["cookieConsent"] == "true";
            }
            return false;
        }


        //    //return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Products(int pageNum, string? allCat, string? allColor, int pageSize = 15)
        {
            pageSize = Math.Clamp(pageSize, 5, 20);

            // Bundling up multiple models to pass!
            var blah = new ProductListViewModel
            {
                Products = _repo.Products
                    .Where(x => (x.Category1 == allCat || x.Category2 == allCat || x.Category3 == allCat || allCat == null) && (x.PrimaryColor == allColor || x.SecondaryColor == allColor || allColor == null))
                    .OrderBy(x => x.Name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = (allCat == null && allColor == null) ? _repo.Products.Count() :
                         (allCat != null && allColor == null) ? _repo.Products.Where(x => x.Category1 == allColor || x.Category2 == allColor || x.Category3 == allColor).Count() :
                         (allCat == null && allColor != null) ? _repo.Products.Where(x => x.PrimaryColor == allColor || x.SecondaryColor == allColor).Count() :
                         _repo.Products.Where(x => x.Category1 == allCat && (x.PrimaryColor == allColor || x.SecondaryColor == allColor)).Count()
                },

                CurrentAllCat = allCat,
                CurrentAllColor = allColor,
                AllCatFilterTitle = "Category",
                AllColorFilterTitle = "Color"
            };

            return View(blah);
        }


        public IActionResult AboutUs()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secrets()
        {
            return View();
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult CRUDProducts()
        {
            var productData = _repo.Products;

            return View(productData);
        }

        public IActionResult EditProduct(int id, Product productModel)
        {
            if (HttpContext.Request.Method == "POST")
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the product from the database
                    var product = _repo.GetProductById(id);
                    if (product == null)
                    {
                        return NotFound();
                    }

                    // Update the product with the values from the form
                    product.Name = productModel.Name;
                    product.Price = productModel.Price;
                    // ... Update the rest of the properties ...

                    // Update the product in the database
                    _repo.Update(product);
                    _repo.SaveChanges();

                    // Redirect to a confirmation page
                    return RedirectToAction("EditConfirmation");
                }
            }
            else
            {
                productModel = _repo.GetProductById(id);
                if (productModel == null)
                {
                    return NotFound();
                }
            }

            return View(productModel);
        }


        public IActionResult EditConfirmation()
        {
            return View();
        }

        public IActionResult RemoveProduct(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult RemoveConfirmation()
        {
            return View();
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        public IActionResult AddConfirmation()
        {
            return View();
        }

        public IActionResult ReviewOrders()
        {
            var orders = _repo.Orders.Where(o => o.Fraud == 1).Take(200).ToList();

            return View(orders);
        }

        public IActionResult ManageAccounts()
        {
            var orders = _repo.Customers.Take(200).ToList();

            return View(orders);
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
