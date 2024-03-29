﻿using EnsureThat;
using Microsoft.Extensions.Logging;
using System;
using Webshop.Domain.Common;

namespace Webshop.Payment.Api.Models
{
    public class Transaction
    {        
        private Transaction(int amount, Payment payment)
        {
            Amount = amount;
            Payment = payment;
            TransactionId = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }
        public Transaction() { }
        static Transaction() { }
        public static Result<Transaction> Create(int amount, Payment payment)
        {
            try
            {
                Ensure.That(amount, nameof(amount)).IsGt<int>(0);
                Ensure.That(payment, nameof(payment)).IsNotNull<Payment>();                
                return Result.Ok<Transaction>(new Transaction(amount, payment));
            }
            catch (Exception ex)
            {
                return Result.Fail<Transaction>(new Error(nameof(Exception), ex.Message));
            }
        }
        public DateTime Created { get; set; }
        public int Amount { get; set; }
        public Payment Payment { get; set; }
        public Guid TransactionId { get; set; }
    }
}
