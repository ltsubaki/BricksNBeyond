using IntexQueensSlay.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntexQueensSlay.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Orders());





        //public IActionResult OrderConfirmation()
        //{
        //    _repo.ClearCart();

        //    return View();
        //}



    }
}
