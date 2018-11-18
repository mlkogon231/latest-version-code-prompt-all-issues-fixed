using MarksAltSourceBank;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{

		public interface ILogger
		{
            void Log(Account logforAccount, string logEntry);
	    }

        public class Logger : ILogger
        {
	    	public void Log(Account logforAccount, string logEntry)
	    	{
                logforAccount.userLog.Add(logEntry);
	    	}
	    }					
}
