using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Interface;
using Xayah.Business.Interface.Services;
using Xayah.Business.Model;

namespace Xayah.Business.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }


        public async Task Add(Transaction transaction)
        {
            await _transactionRepository.Add(transaction);
        }

        public async Task Remove(int id)
        {
            await _transactionRepository.Remove(id);
        }

        public async Task TransactionList(IEnumerable<Transaction> transactions)
        {
            foreach(var transaction in transactions)
            {
                await _transactionRepository.Add(transaction);
            }
        }

        public async Task Update(Transaction transaction)
        {
            await _transactionRepository.Update(transaction);
        }

        public void Dispose()
        {
            _transactionRepository?.Dispose();
        }
    }
}
