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
    public class AuthController : Controller
    {
        private IauthRepository _repo2;

        public AuthController(IauthRepository temp)
        {
            _repo2 = temp;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManageAccess()
        {
            var orders = _repo2.AspNetUserss.ToList();

            return View(orders);
        }
    }
}
