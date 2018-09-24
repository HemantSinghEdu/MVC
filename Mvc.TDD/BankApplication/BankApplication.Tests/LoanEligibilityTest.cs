using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankApplication.Contracts;

namespace BankApplication.Tests
{
    [TestClass]
    public class LoanEligibilityTest
    {
        private ILoanEligibility _loanEligibility;
        public LoanEligibilityTest(ILoanEligibility loanEligibility)
        {
            _loanEligibility = loanEligibility;
        }

        [TestMethod]
        public void TestLoanTypePersonal()
        {
            //Arrange
            string loanType = "Personal";

            //Act
            bool expected = _loanEligibility.HasCorrectType(loanType);

            //Assert
            Assert.IsTrue(expected);
        }
    }
}
