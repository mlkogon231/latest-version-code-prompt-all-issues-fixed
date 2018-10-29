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
			Account account = new Account();
			var accounts = new Dictionary<string, Account>();
		}
	}
}
