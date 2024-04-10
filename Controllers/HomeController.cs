using System.Diagnostics;
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

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Products(int pageNum, string? productCat, string? primaryColor)
        {
            int pageSize = 15;

            //Bundling up multiple models to pass!
            var blah = new ProductListViewModel
            {
                Products = _repo.Products
                    .Where(x => (x.Category1 == productCat || productCat == null) && (x.PrimaryColor == primaryColor || primaryColor == null))
                    .OrderBy(x => x.Name)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = (productCat == null && primaryColor == null) ? _repo.Products.Count() :
                         (productCat != null && primaryColor == null) ? _repo.Products.Where(x => x.Category1 == productCat).Count() :
                         (productCat == null && primaryColor != null) ? _repo.Products.Where(x => x.PrimaryColor == primaryColor).Count() :
                         _repo.Products.Where(x => x.Category1 == productCat && x.PrimaryColor == primaryColor).Count()
                },

                CurrentProductCat = productCat,
                CurrentPrimaryColor = primaryColor
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

        public IActionResult EditProduct(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
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
