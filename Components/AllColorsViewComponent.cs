using Microsoft.AspNetCore.Mvc;
using IntexQueensSlay.Models;

namespace IntexQueensSlay.Components
{
    public class AllColorsViewComponent : ViewComponent
    {
        private readonly ISlayRepository _slayRepo;

        public AllColorsViewComponent(ISlayRepository slayRepo)
        {
            _slayRepo = slayRepo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.AllColorFilterTitle = "Color";

            ViewBag.SelectedAllColor = RouteData?.Values["allColor"];

            var primaryColors = _slayRepo.Products
                .Select(x => x.PrimaryColor)
                .Distinct()
                .OrderBy(x => x);

            var secondaryColors = _slayRepo.Products
                .Select(x => x.SecondaryColor)
                .Distinct()
                .OrderBy(x => x);

            var allColors = primaryColors.Concat(secondaryColors).Distinct().OrderBy(x => x);

            return View(allColors);
        }
    }
}