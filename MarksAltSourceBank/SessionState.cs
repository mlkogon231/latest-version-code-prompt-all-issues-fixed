using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
    public class SessionState
    {
			public string loggedInUser { get; set; }
			public string logfile { get; set; }
			public double loggedInUserBalance { get; set; }

			Account currentAccount = new Account();
		string temp = "debugging";
		
	}
}
