using IntexQueensSlay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IntexQueensSlay.Infrastructure;

namespace IntexQueensSlay.Pages
{
    public class CartModel : PageModel
    {      
        private ISlayRepository repository;
        public CartModel(ISlayRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //Cart = HttpContext.Session.GetJson<Cart>("cart") 
            //    ?? new Cart();
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Products? product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public void Clear()
        {
            Cart.Lines.Clear();
        }
    }
}
