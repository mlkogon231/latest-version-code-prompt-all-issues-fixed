using System;
using System.Collections.Generic;
using System.IO;

namespace MarksAltSourceBank
{
	class Program
	{
		static void Main(string[] args)
		{
			//bool loggedIn = false;
			//string menuSelection = "";
            var accounts = new Dictionary<string, Account>();
            var logs = new Dictionary<string, ILogger>();
            var userAccount = new Account();
           // string currentUser = "";
           // string userLoggedIn = "";
            //bool loggedIn = false;
            SessionState sessionLogin = new SessionState();
            sessionLogin.loggedInUser = "";
            Transaction action = new Transaction();
            Menu mainMenu = new Menu();
			mainMenu.ShowMenu();
			Account account = new Account();
		}
	}
}
