using System;
using System.Collections.Generic;
using System.IO;

namespace MarksAltSourceBank
{
	class Program
	{
		static void Main(string[] args)
		{
			bool loggedIn = false;
			string menuSelection = "";
			Menu mainMenu = new Menu();
			mainMenu.ShowMenu();
			string usersFile = @"c:\windows\temp\Users.txt"; // represents a Users table in a Database
			string accountsFile = @"c:\windows\temp\Accounts.txt"; // represents an Accounts table in a Database
			Account account = new Account();
			var accounts = new Dictionary<string, Account>();
		}
	}
}
