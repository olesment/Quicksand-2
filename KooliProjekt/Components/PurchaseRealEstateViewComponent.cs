using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Models;
using KooliProjekt.Services;
using System.Threading.Tasks;

namespace KooliProjekt.Components
{
    public class PurchaseRealEstateViewComponent : ViewComponent
    {
        private readonly IRealEstatesService _realEstatesService;

        public PurchaseRealEstateViewComponent(IRealEstatesService realEstatesService)
        {
            _realEstatesService = realEstatesService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new PurchaseRealEstatesViewModel();
            return View(model);
        }
    }
}
