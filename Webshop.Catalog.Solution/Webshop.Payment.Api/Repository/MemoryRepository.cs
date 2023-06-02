﻿using Webshop.Payment.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Webshop.Domain.Common;

namespace Webshop.Payment.Api.Repository
{
    public class MemoryRepository : IMemoryRepository
    {
        private List<Transaction> transactions;
        public MemoryRepository()
        {
            transactions = new List<Transaction>();
        }

        public Result AddTransaction(Transaction transaction)
        {
            bool exists = ExistsTransaction(transaction);
            if (!exists)
            {
                this.transactions.Add(transaction);
                return Result.Ok();
            }
            else
            {
                return Result.Fail("A transaction with these parameters already exists. For security reasons two identical transactions cannot be processed (Amount and Cardnumber)");
            }
        }

        private bool ExistsTransaction(Transaction transaction)
        {
            return this.transactions.Any(x => x.Amount == transaction.Amount && x.Payment.CardNumber == transaction.Payment.CardNumber);
        }
    }
}
