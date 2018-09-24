using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Contracts
{
    public interface ILoanEligibility
    {
        bool HasCorrectType(string loanType);
    }
}
