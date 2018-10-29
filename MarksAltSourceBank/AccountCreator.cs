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

		//	string usersFile = @"c:\windows\temp\Users.txt"; // represents an Accounts table in a Database

		private string tryAgain;

		public bool CreateAccount(Dictionary<string, Account> allAccounts)
		{
			// TRY THE NEW DICT STUFF HERE
			Account newAccount = new Account();
			Console.Write("PLease enter a username: ");
			string user = Console.ReadLine();
			Console.Write("PLease enter a password: ");
			string pw = Console.ReadLine();

			if (allAccounts.ContainsKey(user))
			{
				//while ((tryAgain != "Y") || (tryAgain != "N"))
				//{
				//	Console.WriteLine("Error, that account already exists, would you like to try another username ? (Y/N): ");
				//	string tryAgain = Console.ReadLine();
				//	tryAgain = tryAgain.ToUpper();
				//	if (tryAgain == "Y")
				//	{
				//		acc.CreateAccount(allAccounts);
				//	}
				Console.WriteLine("That account exists already, press enter to continue");
				Console.ReadLine();
					return false;
				}


	//			var newAccount = new Account();
				newAccount.username = user;
				newAccount.password = pw;
				newAccount.Balance = 500;
				
				newAccount.userLog.Add("Account created at " + DateTime.Now);
				allAccounts.Add(user, newAccount);
				return true;
		}

		public bool CreateAccount()
		{
			throw new NotImplementedException();
		}
	}
}





			//	bool nameExists;
			//	if (!File.Exists(usersFile))
			//	{
			//		using (StreamWriter sw = File.AppendText(usersFile))
			//		{
			//			sw.WriteLine("Username,Password");
			//		}
			//	}
			//	Console.WriteLine("Users File about to open for reading");
			//	using (StreamReader sr = new StreamReader(usersFile))
			//	{
			//		string record = sr.ReadLine();

//		string[] entries = record.Split(',');
//		string username;

//		foreach (string entry in entries)
//		{
//			username = entry;
//			accounts.Add(username);
//		}
//		if (accounts.Contains(user))
//		{
//			Console.WriteLine("Error, name already exists");
//			Console.ReadLine();
//			nameExists = true;
//		}
//		else
//		{
//			Console.WriteLine("Success, user added");
//			Console.ReadLine();
//			nameExists = false;

//		}
//	}

//	if (!nameExists) {
//		// make entry into account file for user
//		ILogger accEntry = new Logger();
//		string logfile = @"c:\windows\temp\Accounts.txt";
//		Console.WriteLine(user);
//		string Entry = $"{user},0";
//		accEntry.Log(logfile, Entry);

//		// log transaction
//		ILogger logger = new Logger();
//		string loguser = "transaction.log.for." + user + ".log";
//		logfile = @"c:\windows\temp\" + loguser;
//		Console.WriteLine("logfile = " + logfile);
//		logger.Log(logfile, "New account created at " + DateTime.Now);
//	}
//	else
//	{
//		throw Exception();
//	}
//	return true;

//}

//private Exception Exception()
//{
//	throw new NotImplementedException();
//}
