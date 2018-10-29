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

		//private readonly object menuSelecton;
		//public  Menu(string menuSelection = "Choose")
		//	{
		//	return menuSelecton;
		//	}
		//	string result = "";


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
			//string loggedInUser = getLoggedInUser(accounts);
			while (value)
			{
				//Console.WriteLine(currentUser);
				string result = "";
				//	if (loggedInUser != "") Console.WriteLine($"Hello {loggedInUser}");
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
			//	Console.Write("Press N for New Account, L for Login, D for Deposit, W for Withdraw, B for Check Balance, " +
			//	"V for Viewing the Transaction Log, O for logout, E for Exit: ");
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


						//string loguser = "transaction.log.for." + userName + ".log";
						//string logfile = @"c:\windows\temp\" + loguser;
						//logger.Log(logfile, "New account created at " + DateTime.Now);

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
//							sessionLogin.loggedInUser = loggedInUser;
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



						// handle login shortly
						//				loggedInUser = LoginUser(accounts);
						//						if (accounts.ContainsKey(username) && (accounts.TryGetValue(username, out Account password)
						//						{ 
						//							Console.WriteLine("You are here, deal with success or failure of login");
						//						}
						//if (loggedInUser != "")
						//{
						//	sessionLogin.loggedInUser = loggedInUser;
						//	sessionLogin.logfile = "log.for." + loggedInUser + ".log";
						//}
						break;
					case "D":
						//		originalBalance = sessionLogin.loggedInUserBalance;
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
				Console.WriteLine(tempvalue.Balance);
				double origBalance = tempvalue.Balance;
				Console.WriteLine($"Your balance is {origBalance}, hit enter to continue");
				Console.ReadLine();

				tempvalue.userLog.Add("Checked balance at " + DateTime.Now);
				Console.WriteLine("you are here 4");

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
			Console.Write("Please enter your username:");
			string user = Console.ReadLine();
			Console.Write("Please enter your password:");
			string pass = Console.ReadLine();
			Dictionary <string, Account> userAccounts = accounts;

			foreach (var account in tempAccounts)
			{
				var tempvalue = account.Value;
				var tempuser = account.Key;
				


				if ((user == tempuser) && (pass == tempvalue.password)) 
				{
					Console.WriteLine("You will be logged in");
					bool loggedIn = true;
					state.loggedInUser = user;
					state.loggedInUserBalance = tempvalue.Balance;
					tempvalue.isLoggedin = true;
					double originalBalance = tempvalue.Balance;
					
					
					tempvalue.userLog.Add("Logged in at " + DateTime.Now);
					Console.WriteLine();
				}
				else
				{
			//		Console.WriteLine("Error, your username or password is incorrect, please hit enter to continue");
			//		Console.ReadLine();
					bool loggedIn = false;
			//		Console.WriteLine();

				}
				if ((user == tempuser) && (tempvalue.isLoggedin))
				{
					Console.WriteLine("You will be logged in");
					bool loggedIn = true;
					currentUser = tempuser;
					double originalBalance = tempvalue.Balance;
					return true;

					Console.WriteLine();
				}
				else
				{
					Console.WriteLine("You will not be logged in");
					bool loggedIn = false;
					Console.WriteLine();
					//return false;

				}
				
				
				Console.WriteLine();
			}

				Console.WriteLine("Error, you have not entered the correct username/password combo, please hit enter to continue");
				Console.ReadLine();
				return false;
	
		}

		public double getBalance(Account account)
		{
			double origBalance = account.Balance;
			Console.WriteLine("You are here 2");
			return 0;
		}

		// deposit stuff here - mlk

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


				//Console.WriteLine(tempvalue.password);
				//Console.WriteLine(tempuser);
				//Console.WriteLine(tempvalue.isLoggedin);

				if (!tempvalue.isLoggedin)
				{
					//Console.WriteLine("Error, the user I just checked is not the user attempting to login ");
					//Console.ReadLine();
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
				tempvalue.userLog.Add("Logged out at " + DateTime.Now);
				tempvalue.isLoggedin = false;
				//Console.WriteLine($"You have logged out, have a great day! Please press enter to continue");
				//Console.ReadLine();
				//return true;
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

		public string LoginUser(Account userLogin)
		{
			string loginFile = @"c:\windows\temp\loggedin.txt";
			bool login = false;
			string accountsFile = @"c:\windows\temp\Accounts.txt";
			Console.Write("Please enter your username: ");
			userLogin.username = Console.ReadLine();
			Console.Write("Please enter your password: ");
			userLogin.password = Console.ReadLine();

			return "";



			//if (!File.Exists(accountsFile))
			//{
			//	using (StreamReader sr = new StreamReader(accountsFile))
			//	{
			//		string record = sr.ReadLine();
			//		string[] entries = record.Split(',');
			//		int tries;
			//		while (!login)
			//		{
			//			for (var i = 0; i < 3; i++)
			//			{
			//				tries = i + 1;

			//				//							foreach (string entry in entries)
			//				//							{
			//				//								if ((entries[0] == username) && (entries[1] == password))
			//				//								{
			//				//									loggedInUser = username;
			//				//									login = true;
			//				//								}
			//				//								else
			//				//								{
			//				//									Console.WriteLine($"Invalid Login, you have tried {tries} times out of 3");
			//				//								}
			//				//				}
			//				//				if (login)
			//				//				{
			//				//					using (StreamWriter sw = File.AppendText(loginFile))
			//				//					{
			//				//						sw.WriteLine(username);
			//				//					}
			//				//					return username;
			//				//				}
			//				//				else return "";
			//				//			}
			//				//		}
			//				//	}
			//				//}
			//				//Console.Clear();
			//				//Console.WriteLine($"Hello {username}, please enter a selection");
			//				//return username;
			//				//}

			//				//	private object LoginUser(string user, string pw)
			//				//	{
			//				//string usersFile = @"c:\windows\temp\Accounts";
			//				//Console.Write("Please enter your username: ");
			//				//string username = Console.ReadLine();
			//				//Console.Write("Please enter your password: ");
			//				//string password = Console.ReadLine();


			//				//	IList<string> accounts = new List<string>();
			//				//	bool nameExists;
			//				//if (!File.Exists(usersFile))
			//				//{
			//				//	using (StreamWriter sw = File.AppendText(usersFile))
			//				//	{
			//				//		sw.WriteLine("Username,Password");
			//				//	}
			//				//}
			//				//Console.WriteLine("Users File about to open for auth");
			//				//	using (StreamReader sr = new StreamReader(usersFile))
			//				//	{
			//				//		string record = sr.ReadLine();


			//				//		//string[] entries = record.Split(',');
			//				//		//string tempuser;

			//				//		//foreach (string entry in entries)
			//				//		//{
			//				//		//	tempuser = entry;
			//				//		//	accounts.Add(username);
			//				//		//}
			//				//		//if (accounts.Contains(user))
			//				//		//{
			//				//		//	Console.WriteLine("Error, name already exists");
			//				//		//	Console.ReadLine();
			//				//		//	nameExists = true;
			//				//		//}
			//				//		//else
			//				//		//{
			//				//		//	Console.WriteLine("Success, user added");
			//				//		//	Console.ReadLine();
			//				//		//	nameExists = false;

			//				//		//}
			//				//	}

			//				//	if (!nameExists)
			//				//	{
			//				//		// make entry into account file for user
			//				//		ILogger accEntry = new Logger();
			//				//		string logfile = @"c:\windows\temp\Accounts.txt";
			//				//		string Entry = "${user}, 0";
			//				//		accEntry.Log(logfile, Entry);

			//				//		// log transaction
			//				//		ILogger logger = new Logger();
			//				//		string loguser = "transaction.log.for." + user + ".log";
			//				//		logfile = @"c:\windows\temp\" + loguser;
			//				//		Console.WriteLine("logfile = " + logfile);
			//				//		logger.Log(logfile, "New account created at " + DateTime.Now);
			//				//	}
			//				//	else
			//				//	{
			//				//		throw Exception();
			//				//	}
			//				//	return true;

			//				//}
			//				//}
			//				//	}
			//			}
		}
	}
}


//	case "notD":
//		// check for if loggedInUser
//		Console.WriteLine(sessionLogin.loggedInUser);
//		Console.WriteLine(sessionLogin.logfile);

//		Console.WriteLine(sessionLogin.loggedInUserBalance);


//		Console.WriteLine();
//		// see if you can get balance here
//		if (loggedIn)
//		{

//			//double newBalance = Deposit(
//			Console.WriteLine("You can continue with login");
//			Console.Write("Please enter a dollar amount to deposit: ");
//			double deposit = Convert.ToDouble(Console.ReadLine());
//			double newBalance = sessionLogin.loggedInUserBalance + deposit;

//			if (newBalance > 0)
//			{
//				sessionLogin.loggedInUserBalance = newBalance;
////				sessionLogin.action = "Deposited " + deposit + " on " + DateTime.Now;
//			}

//			Console.WriteLine("you are here 1");
//			break;
//			// get falnce using cheesy foreach loop for now.
//		}
//		else Console.WriteLine("You must be logged in to make a deposit");
//		break;