
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xayah.Business.Interface;
using Xayah.Business.Model;
using Xayah.Data.Context;

namespace Xayah.Data.Repository
{
   public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(XayahContext context) : base(context)
        {

        }

        public List<Transaction> GetAllTransictions()
        {        
                var transactions = (from t in Db.Transactions
                                    orderby 1 descending
                                    select new Transaction
                                    {
                                        TRNTYPE = t.TRNTYPE,
                                        DTPOSTED = t.DTPOSTED,
                                        TRNAMT = t.TRNAMT,
                                        MEMO = t.MEMO


                                    });
                return transactions.Distinct().ToList();        
        }
    }
}
