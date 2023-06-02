using Webshop.Payment.Api.Models;
using System.ComponentModel;
using Webshop.Domain.Common;
using Xunit;

namespace Webshop.Payment.Test.Models
{
    [Category("Transaction Unit Tests")]
    public class TransactionUnitTest
    {        
        [Fact]
        public void Valid_Transaction_Expect_True()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("123", "11/11", 123);
            //Act
            Result result = Transaction.Create(1, paymentResult.Value);                        
            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void InValid_Transaction_negative_amount_Expect_False()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("123", "11/11", 123);
            //Act
            Result result = Transaction.Create(-100, paymentResult.Value);
            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void InValid_Transaction_payment_null_Expect_False()
        {
            //Act
            Result result = Transaction.Create(-100, null);
            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void InValid_Transaction_negative_amount_low_boundary_Expect_False()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("123", "11/11", 123);
            //Act
            Result result = Transaction.Create(-1, paymentResult.Value);
            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Valid_Transaction_negative_amount_low_boundary_Expect_True()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("123", "11/11", 123);
            //Act
            Result result = Transaction.Create(1, paymentResult.Value);
            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void InValid_Transaction_negative_amount_zero_boundary_Expect_False()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("123", "11/11", 123);
            //Act
            Result result = Transaction.Create(0, paymentResult.Value);
            //Assert
            Assert.False(result.Success);
        }
    }
}
