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
		bool CreateAccount();
	}

	public class AccountCreator : IAccountCreator
	{
		public AccountCreator(Dictionary<string, Account> tempAccount)
		{
			Dictionary<string, Account> accounts = tempAccount;
		}


		private string tryAgain;

		public bool CreateAccount(Dictionary<string, Account> allAccounts)
		{
			Account newAccount = new Account();
			Console.Write("PLease enter a username: ");
			string user = Console.ReadLine();
			Console.Write("PLease enter a password: ");
			string pw = Console.ReadLine();

			if (allAccounts.ContainsKey(user))
			{
				Console.WriteLine("That account exists already, press enter to continue");
				Console.ReadLine();
					return false;
				}

				newAccount.username = user;
				newAccount.password = pw;
				newAccount.Balance = 500;
				
				newAccount.userLog.Add("Account created at " + DateTime.Now);
				allAccounts.Add(user, newAccount);
			Console.WriteLine($"You added a new account for {user}, please press enter to continue");
			Console.ReadLine();
				return true;
		}

		public bool CreateAccount()
		{
			throw new NotImplementedException();
		}
	}
}
