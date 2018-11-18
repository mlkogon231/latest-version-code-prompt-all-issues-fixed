using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
	// using interface in case admin and/or other specialized menus are needed in the future
	
	public interface IAccountCreator
	{
		bool CreateAccount(Dictionary<string, Account> allAccounts);
	}

	public class AccountCreator : IAccountCreator
	{
		public AccountCreator(Dictionary<string, Account> tempAccount)
		{
			Dictionary<string, Account> accounts = tempAccount;
		}


        public bool CreateAccount(Dictionary<string, Account> allAccounts)
        {
            Account newAccount = new Account();
            Logger logger = new Logger();
            string user = "";
            string pw = "";
            while (string.IsNullOrEmpty(user))
            { 
                Console.Write("PLease enter a username: ");
                user = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(pw))
            {
                Console.Write("PLease enter a password: ");
                pw = Console.ReadLine();
            }

			if (allAccounts.ContainsKey(user))
			{
				Console.WriteLine("That account exists already, press enter to continue");
				Console.ReadLine();
					return false;
				}

				newAccount.username = user;
				newAccount.password = pw;
				newAccount.Balance = 500;
				
				//newAccount.userLog.Add("Account created on " + DateTime.Now);
                string logEntry = "Account created on " + DateTime.Now;
            logger.Log(newAccount, logEntry);
            allAccounts.Add(user, newAccount);
			    Console.WriteLine($"You added a new account for {user}, please press enter to continue");
			    Console.ReadLine();
				return true;
		}
	}
}
