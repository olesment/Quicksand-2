//using KooliProjekt.Data;
//using KooliProjekt.Models;

//namespace KooliProjekt.Services 
//{  //29.11 tehtud selleks, et n2idata transactionite tablet paremini. 
//    public class TransactionsViewModelService : ItransactionsViewModelService
//    {
//        private readonly ApplicationDbContext _context;
            
//        public TransactionsViewModelService(ApplicationDbContext context)
//        {
//        _context = context;
//        }
        
//        public async Task<PagedResult<TransactionsViewModel>> ListViewModel(int page, int pageSize)
//        {
//            var balance = _context.UserFunds.LastOrDefault();
//            var TransactionsQuery = _context.Transactions.Select(t => new TransactionsViewModel
            
//            {
//                TransactionTime = t.TransactionTime,
//                TransactionID = t.TransactionId,
//                AssetId = t.AssetId,
//                AssetName = _context.RealEstates.Where(re => re.RealEstateId == t.AssetId)
//                                                .Select(re =>re.RealEstateName).FirstOrDefault(),
//                Action = t.Action,
//                BalanceBefore = balance, // vajamingi lahendus saada. 
                
                
//                TransactedUnitAmount = t.TransactedAmount,
//                TransactionUnitCost = (int?)t.TransactionUnitCost, //decimal vs int conversion
//                TransactionSum = (double?)t.TransactionResult,

//                BalanceAfter = balance - t.TransactionResult

//                AssetType = t.InvestmentType,
//            });
//        }

//    }   

//}
