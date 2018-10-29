using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
	// using interface in case admin and/or other specialized menus are needed in the future

	public interface IMenu
	{
		string ShowMenu();
	}

	public class Menu : IMenu
	{

		private readonly string result;
		private readonly int password;

		public string ShowMenu()

		{
			var accounts = new Dictionary<string, Account>();
			var logs = new Dictionary<string, ILogger>();
			string currentUser = "";
			bool loggedIn = false;
			SessionState sessionLogin = new SessionState();
			sessionLogin.loggedInUser = "";
			Transaction action = new Transaction();
			bool value = true;
			double originalBalance;

			while (value)
			{
				string result = "";
				if (sessionLogin.loggedInUser != "")
				{
					Console.WriteLine(sessionLogin.loggedInUser);
				} 
				Console.WriteLine();
				string thisUser = getLoggedInUser(accounts);
				Console.WriteLine(thisUser);
				if (thisUser != "")
				{
					Console.Clear();
					Console.WriteLine($"You are logged in as {thisUser}");
					Console.WriteLine();
					currentUser = thisUser;
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Please login or select N to create a new account");
					Console.WriteLine();
				}
				Console.WriteLine("Press N to create a new account");
				Console.WriteLine("Press L to login");
				Console.WriteLine("Press D to make a deposit");
				Console.WriteLine("Press W to make a withdrawal");
				Console.WriteLine("Press B to check your balance");
				Console.WriteLine("Press V to view your transaction log");
				Console.WriteLine("Press O to Logout");
				Console.WriteLine("Press E to exit");
				Console.WriteLine();
				Console.Write("Please enter a selection: ");
				result = Console.ReadLine();
				result = result.ToUpper();


				switch (result)
				{
					case "N":
						Console.WriteLine("You are going to create a new account");
						bool success;
						AccountCreator acc = new AccountCreator(accounts);
						Console.WriteLine();
						acc.CreateAccount(accounts);
						break;
					case "L":
						if (thisUser != "")
						{
							Console.Clear();
							Console.Write($"You are already logged in as {thisUser}, please logout to login again as another user, please hit enter to continue");
							Console.ReadLine();
							break;

						}
						bool loginsuccess = AttemptLogin(accounts, sessionLogin, currentUser);
						double origBalance = sessionLogin.loggedInUserBalance;
						if (loginsuccess) {
							Console.WriteLine(sessionLogin.loggedInUserBalance) ;
							currentUser = thisUser;						
							Console.WriteLine("should be logged in");
							string userLoggedin = thisUser;
							loggedIn = true;
						}
						else
						{ 
							Console.WriteLine("not logged in");
						}
						break;
					case "D":
						string userLoggedIn = sessionLogin.loggedInUser;
						bool loggedinForDeposit = makeDeposit(accounts, sessionLogin);
						break;
					case "W":
						userLoggedIn = sessionLogin.loggedInUser;
						bool loggedinForWithdrawal = makeWithdrawal(accounts, sessionLogin);
						break;
					case "B":
						userLoggedIn = sessionLogin.loggedInUser;
						bool loggedinCheckBalance = checkBalance(accounts, sessionLogin);
						break;
					case "V":
						userLoggedIn = sessionLogin.loggedInUser;
						bool loggedinForViewLog = viewLog(accounts, sessionLogin);
						break;
					case "O":
						userLoggedIn = sessionLogin.loggedInUser;
						bool loggedOut = logOut(accounts, sessionLogin);
						break;
					case "E":
						Console.WriteLine("You are going to exit the application");
						Console.WriteLine("Have a Nice Day");
						value = false;
						break;
					default:
						Console.WriteLine("You did not select a valid option");
						Console.WriteLine();
						break;
				}
			}
			return result;
		}

		private string getLoggedInUser(Dictionary<string, Account> accounts)
		{
			var tempAccounts = accounts;
			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;


				Console.WriteLine(tempvalue.password);
				Console.WriteLine(tempuser);
				Console.WriteLine(tempvalue.isLoggedin);
				if (!tempvalue.isLoggedin)
				{

					continue;
				}
				return tempuser;
			}
			return "";
		}

		public double Deposit()
		{
			return 0;
		}


		public bool AttemptLogin(Dictionary<string, Account> accounts, SessionState state, string currentUser)
		{

			var tempAccounts = accounts;
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must create at least one account to login, press enter to return to main menu ");
				Console.ReadLine();
				return false;
			}
			Console.Write("Please enter your username: ");
			string user = Console.ReadLine();
			Console.Write("Please enter your password: ");
			string pass = Console.ReadLine();
			Dictionary <string, Account> userAccounts = accounts;

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;
				
				if ((user == tempuser) && (pass == tempvalue.password)) 
				{
					bool loggedIn = true;
					state.loggedInUser = user;
					state.loggedInUserBalance = tempvalue.Balance;
					tempvalue.isLoggedin = true;
					double originalBalance = tempvalue.Balance;
					Console.WriteLine();
				}
				else
				{
					bool loggedIn = false;
				}
				if ((user == tempuser) && (tempvalue.isLoggedin))
				{
					bool loggedIn = true;
					currentUser = tempuser;
					double originalBalance = tempvalue.Balance;
					return true;
				}
				else
				{
					bool loggedIn = false;
				}
								
			}

				Console.WriteLine("Error, you have not entered the correct username/password combo, please hit enter to continue");
				Console.ReadLine();
				return false;
	
		}

			public bool makeDeposit(Dictionary<string, Account> accounts, SessionState state)
		{

			var tempAccounts = accounts;
			SessionState session = state;
			double originalBalance = session.loggedInUserBalance;
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must be logged in, press enter to return to main menu 1");
				Console.ReadLine();
				return false;
			}

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;

				if (!tempvalue.isLoggedin)
				{
					continue;
				}
				Console.WriteLine($"Your current balance is {tempvalue.Balance}");
				double origBalance = tempvalue.Balance;

				double depositAmount;
				Console.Write("Please enter an amount to deposit: ");
				depositAmount = Convert.ToDouble(Console.ReadLine());
				double newBalance = origBalance + depositAmount;
				origBalance = newBalance;
				string stringBalance = newBalance.ToString();
				tempvalue.Balance = float.Parse(stringBalance);
				tempvalue.userLog.Add($"Deposited {depositAmount} at " + DateTime.Now);
				Console.WriteLine($"You deposited {depositAmount}, current balance is {stringBalance}, please hit enter to continue");
				Console.ReadLine();

			}
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must be logged in");
				return false;
			}
			return true;
		}

		public bool makeWithdrawal(Dictionary<string, Account> accounts, SessionState state)
		{

			var tempAccounts = accounts;

			SessionState session = state;
			double originalBalance = session.loggedInUserBalance;
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must be logged in, press enter to return to main menu 2 ");
				Console.ReadLine();
				return false;
			}

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;



				if (!tempvalue.isLoggedin)
				{
					continue;
				}
				Console.WriteLine($"Your current balance is {tempvalue.Balance}");
				double origBalance = tempvalue.Balance;

				double withrawAmount;
				Console.Write("Please enter an amount to withdraw: ");
				withrawAmount = Convert.ToDouble(Console.ReadLine());
				double newBalance = origBalance - withrawAmount;
				if (newBalance < 0)
				{
					Console.WriteLine("Error, not enough funds available for withdrawal, please hit enter to continue");
					Console.ReadLine();
					return false;
				}
				origBalance = newBalance;
				string stringBalance = newBalance.ToString();
				tempvalue.Balance = float.Parse(stringBalance);
				tempvalue.userLog.Add($"Withdrew {withrawAmount} at " + DateTime.Now);
				Console.WriteLine($"You withdrew {withrawAmount}, current balance is {stringBalance}, please hit enter to continue");
				Console.ReadLine();

			}
			return true;
		}

		public bool checkBalance(Dictionary<string, Account> accounts, SessionState state)
		{

			var tempAccounts = accounts;
			SessionState session = state;
			double originalBalance = session.loggedInUserBalance;
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must be logged in, press enter to return to main menu ");
				Console.ReadLine();
				return false;
			}

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;



				if (!tempvalue.isLoggedin)
				{
					continue;
				}

				double origBalance = tempvalue.Balance;
				Console.WriteLine($"Your balance is {origBalance}, hit enter to continue");
				Console.ReadLine();

				tempvalue.userLog.Add("Checked balance at " + DateTime.Now);

			}
			return true;
		}

		public bool logOut(Dictionary<string, Account> accounts, SessionState state)
		{

			var tempAccounts = accounts;
			SessionState session = state;
			double originalBalance = session.loggedInUserBalance;
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must be logged in, press enter to return to main menu ");
				Console.ReadLine();
				return false;
			}

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;
				tempvalue.isLoggedin = false;
			}
			Console.WriteLine($"You have logged out, have a great day! Please press enter to continue");
			Console.ReadLine();
			return true;
		}

		public bool viewLog(Dictionary<string, Account> accounts, SessionState state)
		{

			var tempAccounts = accounts;
			SessionState session = state;
			double originalBalance = session.loggedInUserBalance;
			if (tempAccounts.Count == 0)
			{
				Console.WriteLine("Error, you must be logged in, press enter to return to main menu ");
				Console.ReadLine();
				return false;
			}

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;

				if (!tempvalue.isLoggedin)
				{
					continue;
				}
				Console.WriteLine();
				Console.WriteLine($"Transaction Log for {tempuser}");
				Console.WriteLine();
				foreach (var line in tempvalue.userLog)
				{
					Console.WriteLine(line);
				}
				Console.WriteLine();
				Console.WriteLine("Press enter to continue");
				Console.ReadLine();
			}
			return true;
		}
	}
}
