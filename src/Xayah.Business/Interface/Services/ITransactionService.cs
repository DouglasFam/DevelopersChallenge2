using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Model;

namespace Xayah.Business.Interface.Services
{
   public interface ITransactionService : IDisposable
    {      
        Task TransactionList(IEnumerable<Transaction> transactions);
    }
}
