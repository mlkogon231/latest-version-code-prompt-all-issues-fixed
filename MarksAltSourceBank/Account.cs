using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
    public class Account
    {
		public string username { get; set; }
		public string password { get; set; }
		public float Balance { get; set; }
		public bool isLoggedin;
		public IList<string> userLog = new List<string>();

        public bool Login(Dictionary<string, Account> accounts, SessionState state, string currentUser)
        {
            var tempAccounts = accounts;
            if (tempAccounts.Count == 0)
            {
                Console.WriteLine("Error, you must create at least one account to login, press enter to return to main menu ");
                Console.ReadLine();
                return false;
            }
            string user = null;
            string pass = null;
            while (string.IsNullOrEmpty(user))
            {
                Console.Write("Please enter your username: ");
                user = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(pass))
            {
                Console.Write("Please enter your password: ");
                pass = Console.ReadLine();
            }
            Dictionary<string, Account> userAccounts = accounts;

            foreach (var account in tempAccounts)
            {
                var tempvalue = account.Value;
                var tempuser = account.Key;

                if ((user == tempuser) && (pass == tempvalue.password))
                {
                    state.loggedInUser = user;
                    state.loggedInUserBalance = tempvalue.Balance;
                    tempvalue.isLoggedin = true;
                    double originalBalance = tempvalue.Balance;
                    Console.WriteLine();
                }

                if ((user == tempuser) && (tempvalue.isLoggedin))
                {
                    currentUser = tempuser;
                    double originalBalance = tempvalue.Balance;
                    return true;
                }

            }

            Console.WriteLine("Error, you have not entered the correct username/password combo, please hit enter to continue");
            Console.ReadLine();
            return false;
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
                session.loggedInUser = "";

            }
            Console.WriteLine($"You have logged out, have a great day! Please press enter to continue");
            Console.ReadLine();
            return true;
        }
    }		
}

		

