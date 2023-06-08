using Webshop.Payment.Api.Models;
using Webshop.Payment.Api.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;
using Xunit;

namespace Webshop.Payment.Test.Repository
{
    [Category("Repository Unit Tests")]     
    public class MemoryRepositoryUnitTest
    {
        [Fact]
        public void AddSingleTransactionExpectTrue()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/12", 123);
            Transaction transaction = Transaction.Create(1, paymentResult.Value).Value;
            IMemoryRepository memoryRepository = new MemoryRepository();
            //Act
            Result result = memoryRepository.AddTransaction(transaction);            
            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void AddMultipleTransactionExpectFalse()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/12", 123);
            Transaction transaction = Transaction.Create(1, paymentResult.Value).Value;
            IMemoryRepository memoryRepository = new MemoryRepository();
            //Act
            Result firstResult = memoryRepository.AddTransaction(transaction);
            Result secondResult = memoryRepository.AddTransaction(transaction);
            //Assert
            Assert.True(firstResult.Success);
            Assert.False(secondResult.Success);
        }
    }
}
