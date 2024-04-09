using IntexQueensSlay.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntexQueensSlay.Components
{
    public class CategoryMenuViewComponent : ViewComponent 
    {
        private ISlayRepository _repo;

        public CategoryMenuViewComponent(ISlayRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            var categoryMenu = _repo.Products
                .Select(x => x.Category1)
                .Distinct()
                .OrderBy(x => x);

            return View(categoryMenu);
    }   
    }
}
