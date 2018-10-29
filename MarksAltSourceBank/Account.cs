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
		}		
}

		

