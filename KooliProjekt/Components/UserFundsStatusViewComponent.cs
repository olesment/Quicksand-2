using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Components
{
    
    public class UserFundsStatusViewComponent: ViewComponent
    {
        private readonly IUserFundsStatusViewModelService _userFundsStatusViewModelService;

        public UserFundsStatusViewComponent (IUserFundsStatusViewModelService userFundsStatusViewModelService)
        {
            _userFundsStatusViewModelService = userFundsStatusViewModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _userFundsStatusViewModelService.GetUserFundsStatus();
            return View(model);
        }
    }
}
