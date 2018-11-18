using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
    public class SessionState
    {
			public string loggedInUser { get; set; }
			public string logfile { get; set; }
			public double loggedInUserBalance { get; set; }

			Account currentAccount = new Account();

        public string getLoggedInUser(Dictionary<string, Account> accounts)
        {
            var tempAccounts = accounts;
            foreach (var account in tempAccounts)
            {
                var tempvalue = account.Value;
                var tempuser = account.Key;

                if (!tempvalue.isLoggedin)
                {
                    continue;
                }
                return tempuser;
            }
            return "";
        }
    }
}
