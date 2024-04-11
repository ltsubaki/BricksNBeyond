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
                .Where(color => !string.IsNullOrEmpty(color)) // Filter out null or empty primary colors
                .Distinct()
                .OrderBy(color => color);

            var secondaryColors = _slayRepo.Products
                .Select(x => x.SecondaryColor)
                .Where(color => !string.IsNullOrEmpty(color)) // Filter out null or empty secondary colors
                .Distinct()
                .OrderBy(color => color);

            var allColors = primaryColors.Concat(secondaryColors).Distinct().OrderBy(x => x);

            return View(allColors);
        }
    }
}