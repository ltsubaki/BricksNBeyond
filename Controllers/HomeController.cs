using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
using IntexQueensSlay.Models;
using IntexQueensSlay.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

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
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;

        public HomeController(ISlayRepository temp, IHostEnvironment hostEnvironment)
        {
            _repo = temp;

            // these are for ReviewOrders
            _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "saved_model_reg.onnx");

            // initialize the InferenceSession
            _session = new InferenceSession(_onnxModelPath);
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
        [AllowAnonymous]
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

        [Authorize(Roles = "Admin")]
        public IActionResult EditConfirmation()
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
            return View(new Product());
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct(Product productModel)
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

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            //delete the record by ID num
            var recordToDelete = _repo.Customers
                .Single(x => x.CustomerId == id);

            return View(recordToDelete);
        }
        [HttpPost]
        //calls the repo pattern method to delete
        public IActionResult DeleteUser(Customer task)
        {
            var recordToDelete = _repo.Customers
                .Single(x => x.CustomerId == task.CustomerId);
            //actually delete it
            _repo.DeleteCustomer(recordToDelete);
            //goes back to quadrant views
            return RedirectToAction("ManageAccounts");
        }

        [HttpGet]
        public IActionResult EditUser(int id)

        {
            var recordToEdit = _repo.Customers
                .Single(x => x.CustomerId == id);

            //ViewBag.Category = _repo.Category
            //    .OrderBy(x => x.CategoryName)
            //    .ToList();
            return View("AddCustomer", recordToEdit);
        }

        //updates the reccord and redirects to view
        [HttpPost]
        public IActionResult Edituser(Customer updateresponse)
        {
            //update the datebase with the new edits
            _repo.EditCustomer(updateresponse);
            //return to view
            return RedirectToAction("ManageAccounts");
        }

        public IActionResult AddCustomer(int id, Customer customerModel)
        {
            if (HttpContext.Request.Method == "POST")
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the product from the database
                    var customer = _repo.GetCustomerById(id);
                    if (customer == null)
                    {
                        return NotFound();
                    }

                    // Update the product with the values from the form
                    customer.FirstName = customerModel.FirstName;
                    customer.LastName = customerModel.LastName;
                    customer.BirthDate = customerModel.BirthDate;
                    customer.ResCountry = customerModel.ResCountry;
                    customer.Gender = customerModel.Gender;
                    customer.Age = customerModel.Age;
                    // ... Update the rest of the properties ...

                    // Update the product in the database
                    _repo.UpdateCustomer(customer);
                    _repo.SaveChanges();

                    // Redirect to a confirmation page
                    return RedirectToAction("EditConfirmation");
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

        public IActionResult ReviewOrders()
        {
            var orders = _repo.Orders.Where(o => o.Fraud == 1).Take(200).ToList();

            return View(orders);

            var records = _repo.Orders
                .OrderByDescending(o => o.Date)
                .Take(20)
                .ToList(); //fetch the 20 most recent records
            var predictions = new List<OrderPredictions>(); //your ViewModel for the view

            //Dictionary mapping the numeric prediction to a fraud type
            var class_type_dict = new Dictionary<int, string>
            {
                {0, "Not Fraud" },
                {1, "Fraud" }
            };

            foreach (var record in records)
            {
                // calculate days since Jan 1, 2022
                var daysSinceJan12022 = 0;
                if (!string.IsNullOrEmpty(record.Date))
                {
                    var date = DateTime.Parse(record.Date);
                    var january1_2022 = new DateTime(2022, 1, 1);
                    daysSinceJan12022 = Math.Abs((date - january1_2022).Days);
                }


                //preprocess features to make them combatible with model
                var input = new List<float>
                {
                    (float)record.CustomerId,
                    (float)record.Time,
                    // fix amount if it's null
                    (float)(record.Subtotal ?? 0),

                    // fix date
                    daysSinceJan12022,

                    // check the dummy coded data
                    record.WeekDay == "Mon" ? 1 : 0,
                    record.WeekDay == "Sat" ? 1 : 0,
                    record.WeekDay == "Sun" ? 1 : 0,
                    record.WeekDay == "Thu" ? 1 : 0,
                    record.WeekDay == "Tue" ? 1 : 0,
                    record.WeekDay == "Wed" ? 1 : 0,
                    record.WeekDay == "Fri" ? 1 : 0,

                    record.EntryMode == "Pin" ? 1 : 0,
                    record.EntryMode == "Tap" ? 1 : 0,

                    record.TransactionType == "Online" ? 1 : 0,
                    record.TransactionType == "POS" ? 1 : 0,

                    record.TransCountry == "India" ? 1 : 0,
                    record.TransCountry == "Russia" ? 1 : 0,
                    record.TransCountry == "USA" ? 1 : 0,
                    record.TransCountry == "UnitedKingdon" ? 1 : 0,

                    // use transCountry if shipping address is null
                    (record.ShippingAddress ?? record.TransCountry) == "India" ? 1 : 0,
                    (record.ShippingAddress ?? record.TransCountry) == "Russia" ? 1 : 0,
                    (record.ShippingAddress ?? record.TransCountry) == "USA" ? 1 : 0,
                    (record.ShippingAddress ?? record.TransCountry) == "UnitedKingdom" ? 1 : 0,


                    record.Bank == "HSBC" ? 1 : 0,
                    record.Bank == "Halifax" ? 1 : 0,
                    record.Bank == "Lloyds" ? 1 : 0,
                    record.Bank == "Metro" ? 1 : 0,
                    record.Bank == "Monzo" ? 1 : 0,
                    record.Bank == "RBS" ? 1 : 0,

                    record.CardType == "Visa" ? 1 : 0,
                };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };

                string predictionResult;
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
                }

                predictions.Add(new OrderPredictions { Orders = record, Predictions = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel
            }

            return View(predictions);
        }
    }
}