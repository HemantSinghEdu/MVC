using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Contracts;

namespace BankApplication.BusinessLayer
{
    public class LoanEligibility : ILoanEligibility
    {
        public bool HasCorrectType(string loanType)
        {
            if(loanType.Equals("Personal"))
            {
                return true;
            }
            return false;
        }
    }
}
