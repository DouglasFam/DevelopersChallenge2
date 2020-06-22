using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Model;

namespace Xayah.Business.Interface.Services
{
   public interface ITransactionService : IDisposable
    {
        Task Add(Transaction transaction);

        Task Update(Transaction transaction);

        Task Remove(int id);

        Task TransactionList(IEnumerable<Transaction> transactions);
    }
}
