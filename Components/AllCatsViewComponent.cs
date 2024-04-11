using IntexQueensSlay.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntexQueensSlay.Components
{
    public class AllCatsViewComponent : ViewComponent
    {
        private readonly ISlayRepository _slayRepo;

        public AllCatsViewComponent(ISlayRepository slayRepo)
        {
            _slayRepo = slayRepo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.AllCatFilterTitle = "Category";

            ViewBag.SelectedAllColor = RouteData?.Values["allCat"];

            var category1 = _slayRepo.Products
                .Select(x => x.Category1)
                .Distinct()
                .OrderBy(x => x);

            var category2 = _slayRepo.Products
                .Select(x => x.Category2)
                .Distinct()
                .OrderBy(x => x);

            var category3 = _slayRepo.Products
                .Select(x => x.Category3)
                .Distinct()
                .OrderBy(x => x);

            var someCats = category1.Concat(category2).Distinct().OrderBy(x => x);
            var allCats = someCats.Concat(category3).Distinct().OrderBy(x => x);


            return View(allCats);
        }
    }
}
