using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Model;

namespace Xayah.Business.Interface
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        List<Transaction> GetAllTransictions();
    }
}
