﻿using Webshop.Payment.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;
using Xunit;

namespace Webshop.Payment.Test.Models
{
    [Category("Model Unit Tests")]
    public class PaymentUnitTest
    {
        [Fact]
        public void Create_Valid_Payment_Expect_No_Exception()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("123", "11/15", 123);
            //Assert
            Assert.True(paymentResult.Success);
            Assert.Equal("123", paymentResult.Value.CardNumber);
            Assert.Equal("11/15", paymentResult.Value.ExpirationDate);
            Assert.Equal(123, paymentResult.Value.CVC);
        }

        [Fact]
        public void Create_Invalid_Payment_Empty_Cardnumber_Expect_False()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("", "11/15", 123);
            //Assert
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void Create_Invalid_Payment_Empty_ExpirationDate_Expect_False()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "", 123);
            //Assert
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void Create_Invalid_Payment_Bad_Format_ExpirationDate_Expect_False()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "1/1", 123);
            //Act + Assert
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void Create_Invalid_Payment_Too_Low_CVC_Expect_False()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 1);
            //Act + Assert
            Assert.False(paymentResult.Success);
        }

        [Fact]
        public void Create_Valid_Payment_LowBoundary_Exact_CVC_Expect_True()
        {
            //Arrange + act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 100);
            //Assert
            Assert.True(paymentResult.Success);
            Assert.Equal(100, paymentResult.Value.CVC);
        }

        [Fact]
        public void Create_Invalid_Payment_Too_High_CVC_Expect_False()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 1000);
            //Assert
            Assert.False(paymentResult.Success);
        }

        // Remember how to test exceptions in constructor
        //[Fact]
        //public void Create_Invalid_Payment_Too_High_CVC_Expect_Exception()
        //{
        //    //Arrange
        //    Action action = () => Payment.Create("1234", "11/11", 1000);
        //    //Act + Assert
        //    Assert.Throws<ArgumentOutOfRangeException>(() => action());
        //}

        [Fact]
        public void Create_Invalid_Payment_LowBoundary_Below_CVC_Expect_False()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 99);
            //Assert
            Assert.False(paymentResult?.Success);
        }

        [Fact]
        public void Create_Invalid_Payment_LowBoundary_Above_CVC_Expect_True()
        {
            //Arrange + act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 101);
            //Assert
            Assert.True(paymentResult.Success);
            Assert.Equal(101, paymentResult.Value.CVC);
        }

        [Fact]
        public void Create_Invalid_Payment_HighBoundary_Exact_CVC_Expect_True()
        {
            //Arrange + Act
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 999);
            //Assert
            Assert.True(paymentResult.Success);
            Assert.Equal(999, paymentResult.Value.CVC);
        }

        [Fact]
        public void Create_Invalid_Payment_HighBoundary_Above_CVC_Expect_False()
        {
            //Arrange
            Result<Api.Models.Payment> paymentResult = Api.Models.Payment.Create("1234", "11/11", 1001);
            //Act + Assert
            Assert.False(paymentResult.Success);
        }
    }
}
