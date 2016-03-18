using System;
using System.Net;
using System.Collections.Generic;
namespace test1
{
	//public static class 
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			QTopr opr = new QTopr ();
			while (true) {
				opr.StartLoop ();
			}
			//Console.ReadKey ();
		}
	}
}
