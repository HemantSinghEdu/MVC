using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApplication.Contracts;
using BankApplication.BusinessLayer;
using Moq;

namespace BankApplication.Tests
{
    [TestClass]
    public class LoanEligibilityTest
    {
        private Mock<ILoanEligibility> _loanEligibility;

        [TestMethod]
        public void TestLoanTypePersonal()
        {
            SetMockLoanEligibility();

            //Arrange
            string loanType = "Personal";

            //Act
            bool expected = _loanEligibility.Object.HasCorrectType(loanType);

            //Assert
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void TestLoanTypeInvalid()
        {
            SetMockLoanEligibility();

            //Arrange
            string loanType = "House";

            //Act
            bool expected = _loanEligibility.Object.HasCorrectType(loanType);

            //Assert
            Assert.IsFalse(expected);
        }

        //helper method
        public void SetMockLoanEligibility()
        {
            _loanEligibility = new Mock<ILoanEligibility>();
            _loanEligibility
                .Setup(loanElg => loanElg.HasCorrectType("Personal"))
                .Returns(true);
        }

    }
}
