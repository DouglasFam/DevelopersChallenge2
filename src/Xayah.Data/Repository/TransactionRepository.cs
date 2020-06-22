
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

     
    }
}
