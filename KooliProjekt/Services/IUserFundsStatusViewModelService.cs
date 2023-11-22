using KooliProjekt.Models;
using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IUserFundsStatusViewModelService
    {
        Task<UserFundsStatusViewModel> GetUserFundsStatus();
    }
}
