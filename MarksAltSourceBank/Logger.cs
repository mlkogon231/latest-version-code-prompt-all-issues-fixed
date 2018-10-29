using MarksAltSourceBank;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksAltSourceBank
{
		// using an interface as a Logger method would would most likely do more stuff in a real application

		public interface ILogger
		{
			void Log(string logfile, string logEntry);
	}

    public class Logger : ILogger
    {
			public void Log(string logfile, string logEntry)
			{
				using (StreamWriter logger = File.AppendText(logfile))
				{
					logger.WriteLine(logEntry);
				}
			}
	}					
}
