using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
    public class Transaction
    {
		public string transactionType { get; set; }
		public double balance { get; set; }

		public double Deposit()
		{
			double x = 0;
			return x;
			// do the deposit from accounts.txt, write the value back into the file, and return the balance
		}
		public double Withdrawal()
		{
			double x = 0;
			return x;
			// do the withdrawal from accounts.txt, write the value back into the file, and return the balance
		}
		public double Checkbalance()
		{
			double x = 0;
			return x;
			// do the checkbalance from accounts.txt and return the balance
		}


	}
}
