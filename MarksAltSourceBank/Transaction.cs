using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{

    public class Transaction
    {
        public string username { get; set; }
		public double balance { get; set; }
        public double depositAmount;
        public double withdrawAmount;
        public string amount { get; set; }
        // last attempt at moving logging here
        Logger logger = new Logger();

        public bool makeDeposit(Dictionary<string, Account> accounts, SessionState state)
        {
            var tempAccounts = accounts;
            SessionState session = state;
            double originalBalance = session.loggedInUserBalance;
            if (tempAccounts.Count == 0)
            {
                Console.WriteLine("Error, you must be logged in, press enter to return to main menu");
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
                double? origBalance = tempvalue.Balance;

                bool validAmount = false;
                while (!validAmount)
                {
                    Console.Write("Please enter an amount to deposit between 1 and 10000: ");
                    amount = Console.ReadLine();
                    Double.TryParse(amount, out depositAmount);
                    if ((depositAmount > 0 ) && (depositAmount <= 10000)) {
                        validAmount = true;
                    }
                }
                    double? newBalance = origBalance + depositAmount;
                    origBalance = newBalance;
                    string stringBalance = newBalance.ToString();
                    tempvalue.Balance = float.Parse(stringBalance);
                    string logEntry = $"Deposited {depositAmount} on " + DateTime.Now;
                    logger.Log(tempvalue, logEntry);
                    
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
                Console.WriteLine("Error, you must be logged in, press enter to return to main menu");
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
                double? origBalance = tempvalue.Balance;

                bool validAmount = false;
                while (!validAmount)
                {
                    Console.Write("Please enter an amount to withdraw between 1 and 10000: ");
                    amount = Console.ReadLine();
                    Double.TryParse(amount, out withdrawAmount);
                    if ((withdrawAmount > 0) && (withdrawAmount <= 10000))
                    {
                        validAmount = true;
                    }
                }
                double? newBalance = origBalance - withdrawAmount;
                if (newBalance < 0)
                {
                    Console.WriteLine("Error, not enough funds available for withdrawal, please hit enter to continue");
                    Console.ReadLine();
                    return false;
                }
                origBalance = newBalance;
                string stringBalance = newBalance.ToString();
                tempvalue.Balance = float.Parse(stringBalance);
                string logEntry = $"Withdrew {withdrawAmount} on " + DateTime.Now;
                logger.Log(tempvalue, logEntry);

                Console.WriteLine($"You withdrew {withdrawAmount}, current balance is {stringBalance}, please hit enter to continue");
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
                string logEntry = "Checked balance on " + DateTime.Now;
                logger.Log(tempvalue, logEntry);


            }
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
