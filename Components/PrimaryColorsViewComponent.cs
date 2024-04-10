using Microsoft.AspNetCore.Mvc;
using IntexQueensSlay.Models;

namespace IntexQueensSlay.Components
{
    public class PrimaryColorsViewComponent : ViewComponent
    {
        private ISlayRepository _slayRepo;
        //Constructor
        public PrimaryColorsViewComponent(ISlayRepository temp)
        {
            _slayRepo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.PrimaryColorFilterTitle = "Primary Color";

            ViewBag.SelectedPrimaryColor = RouteData?.Values["primaryColor"];

            var primaryColors = _slayRepo.Products
                .Select(x => x.PrimaryColor)
                .Distinct()
                .OrderBy(x => x);

            return View(primaryColors);
        }
    }
}

