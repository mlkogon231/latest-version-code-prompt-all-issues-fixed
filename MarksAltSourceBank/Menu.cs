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
		void ShowMenu();
	}

	public class Menu : IMenu
	{

		public void ShowMenu()

		{
			var accounts = new Dictionary<string, Account>();
			var logs = new Dictionary<string, ILogger>();
            var userAccount = new Account();
            string currentUser = "";
            string userLoggedIn = "";
			SessionState sessionLogin = new SessionState();
			sessionLogin.loggedInUser = "";
			Transaction action = new Transaction();

            // last attempt at moving logging here
            Logger logger = new Logger();
			bool value = true;


			while (value)
			{
				string result = "";
				if (sessionLogin.loggedInUser != "")
				{
					Console.WriteLine(sessionLogin.loggedInUser);
				} 
				Console.WriteLine();
				string thisUser = sessionLogin.getLoggedInUser(accounts);

				if (thisUser != "")
				{
					Console.Clear();
					Console.WriteLine($"You are logged in as {thisUser}");
					Console.WriteLine();
					currentUser = thisUser;
                    Console.WriteLine("Press N to create a new account");
                    Console.WriteLine("Press D to make a deposit");
                    Console.WriteLine("Press W to make a withdrawal");
                    Console.WriteLine("Press B to check your balance");
                    Console.WriteLine("Press V to view your transaction log");
                    Console.WriteLine("Press O to Logout");
                    Console.WriteLine("Press E to exit");
                    Console.WriteLine();
                }
				else
				{
					Console.Clear();
                    Console.WriteLine("Press N to create a new account");
                    Console.WriteLine("Press L to login");
                    Console.WriteLine("Press E to exit");
                    Console.WriteLine();
				}

                while (string.IsNullOrEmpty(result)) 
                {                                                         
                    Console.Write("Please enter a selection: ");
				    result = Console.ReadLine();
				    result = result.ToUpper();

                    switch (result)
				    {
				    	case "N":
				    		Console.WriteLine("You are going to create a new account");
				    		AccountCreator acc = new AccountCreator(accounts);
				    		Console.WriteLine();
				    		acc.CreateAccount(accounts);
				    		break;
				    	case "L":
                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("You haven't created any accounts");
                                Console.ReadLine();
                                break;

                            }
				    		if (thisUser != "")
				    		{
				    			Console.Clear();
				    			Console.Write($"You are already logged in as {thisUser}, please logout to login again as another user, please hit enter to continue");
				    			Console.ReadLine();
				    			break;

				    		}
                            bool loginsuccess = userAccount.Login(accounts, sessionLogin, currentUser);
				    		double origBalance = sessionLogin.loggedInUserBalance;
				    		if (loginsuccess) {
				    			Console.WriteLine(sessionLogin.loggedInUserBalance) ;
				    			currentUser = thisUser;						
				    			Console.WriteLine("should be logged in");
				    			string userLoggedin = thisUser;
				    		}
				    		else
				    		{ 
				    			Console.WriteLine("not logged in");
				    		}
				    		break;
				    	case "D":
				    		userLoggedIn = sessionLogin.loggedInUser;
                                
                            if (string.IsNullOrEmpty(userLoggedIn))
                            {
                                Console.WriteLine("You did not select a valid option");
                                Console.WriteLine("Press Enter to Continue");
                                Console.ReadLine();
                                break;
                            }
                            action.makeDeposit(accounts, sessionLogin);
				    		break;
				    	case "W":
				    		userLoggedIn = sessionLogin.loggedInUser;
                            if (string.IsNullOrEmpty(userLoggedIn))
                            {
                                Console.WriteLine("You did not select a valid option");
                                Console.WriteLine("Press Enter to Continue");
                                Console.ReadLine();
                                break;
                            }
                            action.makeWithdrawal(accounts, sessionLogin);
				    		break;
				    	case "B":
				    		userLoggedIn = sessionLogin.loggedInUser;
                            if (string.IsNullOrEmpty(userLoggedIn))
                            {
                                Console.WriteLine("You did not select a valid option");
                                Console.WriteLine("Press Enter to Continue");
                                Console.ReadLine();
                                break;
                            }
                            action.checkBalance(accounts, sessionLogin);
				    		break;
				    	case "V":
				    		userLoggedIn = sessionLogin.loggedInUser;
                            if (string.IsNullOrEmpty(userLoggedIn))
                            {
                                Console.WriteLine("You did not select a valid option");
                                Console.WriteLine("Press Enter to Continue");
                                Console.ReadLine();
                                break;
                            }    
                            action.viewLog(accounts, sessionLogin);
				    		break;
				    	case "O":
				    		userLoggedIn = sessionLogin.loggedInUser;
                            if (string.IsNullOrEmpty(userLoggedIn))
                            {
                                Console.WriteLine("You did not select a valid option");
                                Console.WriteLine("Press Enter to Continue");
                                Console.ReadLine();
                                break;
                            }
                            bool loggedOut = userAccount.logOut(accounts, sessionLogin);
				    		break;
				    	case "E":
				    		Console.WriteLine("You are going to exit the application");
				    		Console.WriteLine("Have a Nice Day");
				    		value = false;
				    		break;
				    	default:
				    		Console.WriteLine("You did not select a valid option");
				    		Console.WriteLine("Press Enter to Continue");
                            Console.ReadLine();                                
                            break;
				    }
               }
            }
		}
	}
}
