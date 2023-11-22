using KooliProjekt.Data;
using KooliProjekt.Models; //22.11 Kui see UserFundsStatusView model istub mul Mudelites ja see ei ole [leval mainitud,
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
//siis ta ei leia seda ja annab errori, et wtf on see UserFundsStatusViewModel

namespace KooliProjekt.Services
{
    public class UserFundsStatusViewModelService : IUserFundsStatusViewModelService
    {
        private readonly ApplicationDbContext _context;

        public UserFundsStatusViewModelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserFundsStatusViewModel> GetUserFundsStatus()
        {
            var userFunds = await _context.UserFunds.FirstOrDefaultAsync();

            return new UserFundsStatusViewModel
            {
                UserFundsBalance = userFunds?.Balance.Value ?? 0
            };
        }
    }
}
   
//Maybe siin peaks olema k aGetByID hiljem kui UserFundID>1 