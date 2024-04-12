using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Products(int pageNum, string? allCat, string? allColor, int pageSize = 15)
        {
            // Ensure pageNum is always greater than or equal to 1
            pageNum = Math.Max(pageNum, 1);

            // Ensure pageSize is within the range of 5 to 20
            pageSize = Math.Clamp(pageSize, 5, 20);

            // Calculate the skip count using pageNum and pageSize
            int skipCount = (pageNum - 1) * pageSize;

            // Bundling up multiple models to pass!
            var blah = new ProductListViewModel
            {
                Products = _repo.Products
                    .Where(x => (x.Category1 == allCat || x.Category2 == allCat || x.Category3 == allCat || allCat == null) &&
                                (x.PrimaryColor == allColor || x.SecondaryColor == allColor || allColor == null))
                    .OrderBy(x => x.Name)
                    .Skip(skipCount)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products
                        .Where(x => (allCat == null || x.Category1 == allCat || x.Category2 == allCat || x.Category3 == allCat) &&
                                    (allColor == null || x.PrimaryColor == allColor || x.SecondaryColor == allColor))
                        .Count()

                },

                CurrentAllCat = allCat,
                CurrentAllColor = allColor,
                AllCatFilterTitle = "Category",
                AllColorFilterTitle = "Color"
            };

            return View(blah);
        }

        [AllowAnonymous]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secrets()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ProductDetails(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CRUDProducts()
        {
            var productData = _repo.Products;

            return View(productData);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditProduct(int id, Products productModel)
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

        [Authorize(Roles = "Admin")]
        public IActionResult EditConfirmation()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddAdminConfirm()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditAdminConfirm()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveAdminConfirm()
        {
            return View();
        }


        //[Authorize(Roles = "Admin")]
        //public IActionResult RemoveProduct(int id)
        //{
        //    var product = _repo.GetProductById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        [HttpGet]
        public IActionResult RemoveProduct(int id)
        {
            // Retrieve the product from the database
            var product = _repo.GetProductById(id);

            if (product == null)
            {
                // If the product is not found, return an error view
                return View("Error");
            }

            // Pass the product to the view
            return View(product);
        }


        [HttpPost]
        public IActionResult RemoveProductConfirmed(int id)
        {
            // Retrieve the product from the database
            var product = _repo.GetProductById(id);

            if (product != null)
            {
                // Remove the product from the database
                _repo.RemoveProduct(product);
                _repo.SaveChanges();

                // Redirect to a confirmation page
                return RedirectToAction("RemoveConfirmation");
            }

            // If the product is not found, return an error view
            return View("Error");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new Products());
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct(Products productModel)
        {
            if (ModelState.IsValid)
            {
                // Add the product to the database
                _repo.AddProduct(productModel);
                _repo.SaveChanges();

                // Redirect to a confirmation page
                return RedirectToAction("AddConfirmation");
            }

            // If the model state is not valid, return the form
            return View(productModel);
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public IActionResult AddCustomer()
        //{
        //    return View(new Product());
        //}


        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public IActionResult AddCustomer(Product productModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Add the product to the database
        //        _repo.AddProduct(productModel);
        //        _repo.SaveChanges();

        //        // Redirect to a confirmation page
        //        return RedirectToAction("AddConfirmation");
        //    }

        //    // If the model state is not valid, return the form
        //    return View(productModel);
        //}

        [Authorize(Roles = "Admin")]
        public IActionResult RemoveConfirmation()
        {
            return View();
        }
        [Authorize(Roles = "Customer,Admin")]

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        public IActionResult ReviewOrderConfirmation()
        {
            return View();
        }

        //public IActionResult ReviewOrderDone()
        //{
        //    return View();
        //}

        //public IActionResult ReviewOrderDone()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult ReviewOrderDone(int id, int fraud)
        //{
        //    // Retrieve the order using the id
        //    var order = _repo.GetOrderById(id);

        //    // Check if the order exists
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update the fraud status
        //    order.Fraud = fraud;

        //    // Save changes
        //    _repo.SaveChanges();

        //    // Redirect to the ReviewOrderDone view
        //    return View("ReviewOrderDone");
        //}


        public IActionResult AddConfirmation()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ReviewOrders()
        {
            var orders = _repo.Orders.Where(o => o.Fraud == 1).Take(200).ToList();

            return View(orders);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ManageAccounts()
        {
            var orders = _repo.Customers.Take(200).ToList();

            return View(orders);
        }

        //[HttpPost]
        //public IActionResult ManageAccounts()
        //    {

        //    }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RemoveCustomer(int id)
        {
            // Retrieve the customer from the database
            var customer = _repo.GetCustomerById(id);

            if (customer == null)
            {
                // If the customer is not found, return an error view
                return View("Error");
            }

            // Pass the customer to the view
            return View(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        //calls the repo pattern method to delete
        public IActionResult RemoveCustomerConfirmed(int id)
        {
            // Retrieve the customer from the database
            var customer = _repo.GetCustomerById(id);

            if (customer != null)
            {
                // Remove the customer from the database
                _repo.RemoveCustomer(customer);

                // Redirect to a confirmation page
                return RedirectToAction("RemoveAdminConfirm");
            }

            // If the customer is not found, return an error view
            return View("Error");
        }
    //updates the reccord and redirects to view
    [HttpPost]
        public IActionResult Edituser(Customers updateresponse)
        {
            //update the datebase with the new edits
            _repo.EditCustomer(updateresponse);
            //return to view
            return RedirectToAction("ManageAccounts");
        }


        //[HttpGet]
        //public IActionResult EditUser(int id)

        //{
        //    var recordToEdit = _repo.Customers
        //        .Single(x => x.CustomerId == id);

        //    //ViewBag.Category = _repo.Category
        //    //    .OrderBy(x => x.CategoryName)
        //    //    .ToList();
        //    return View("AddCustomer", recordToEdit);
        //}

        ////updates the reccord and redirects to view
        //[HttpPost]
        //public IActionResult Edituser(Customer updateresponse)
        //{
        //    //update the datebase with the new edits
        //    _repo.EditCustomer(updateresponse);
        //    //return to view
        //    return RedirectToAction("ManageAccounts");
        //}


        [Authorize(Roles = "Admin")]
        public IActionResult EditCustomer(int id, Customers customerModel)
        {
            if (HttpContext.Request.Method == "POST")
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the customer from the database
                    var customer = _repo.GetCustomerById(id);
                    if (customer == null)
                    {
                        return NotFound();
                    }

                    // Update the customer with the values from the form
                    customer.FirstName = customerModel.FirstName;
                    customer.LastName = customerModel.LastName;
                    customer.BirthDate = customerModel.BirthDate;
                    customer.ResCountry = customerModel.ResCountry;
                    customer.Gender = customerModel.Gender;
                    customer.Age = customerModel.Age;
                    // ... Update the rest of the properties ...

                    // Update the customer in the database
                    _repo.UpdateCustomer(customer);
                    _repo.SaveChanges();

                    // Redirect to a confirmation page
                    return RedirectToAction("EditAdminConfirm");
                }
            }
            else
            {
                customerModel = _repo.GetCustomerById(id);
                if (customerModel == null)
                {
                    return NotFound();
                }
            }

            return View(customerModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddCustomer(Customers customerModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new customer to the database
                _repo.AddCustomer(customerModel);
                _repo.SaveChanges();

                // Redirect to a confirmation page
                return RedirectToAction("AddAdminConfirm");
            }

            // If the model state is not valid, return the form
            return View(customerModel);
        }


        [Authorize(Roles = "Customer,Admin")]
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

