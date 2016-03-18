using System;
using System.Collections.Generic;
namespace test1
{
	public class QTopr
	{
		private List<string> m_collection;
		public QTopr (){
			Console.WriteLine ("Created by MythKAst");
		}
		private void InitQueue(){
			m_collection = new List<string> ();
			for (;;) {
				Console.Write ("Input str in to RFID_Queue,input '#' to exit\n:");
				string str = Console.ReadLine ();
				if (str.Equals ("#")) {
					break;
				} else {
					if (str.Equals (string.Empty) || str.Equals("")) {
						Console.WriteLine ("nothing to do", str);
						continue;
					}
					if (!m_collection.Contains (str)) {
						m_collection.Add (str);
					} else {
						Console.WriteLine ("{0} has already in RFID_QUEUE", str);
					}
				}
			}
			int len = 0;
			foreach (string s in m_collection) {
				if (s.Length > len) {
					len = s.Length;
				}
			}
			for(int i = 0;i < m_collection.Count;i++){
				if (m_collection [i].Length < len) {
					for (int j = m_collection [i].Length; j < len; j++) {
						m_collection[i] = "0" + m_collection[i];
					}
				}
				Console.WriteLine (m_collection[i]);
			}
		}
        private string[] expand(string str)
        {
            string[] ret = { str + "0", str + "1" };
            return ret;
        }
        private void compare_core(string str){

            var ret = Compare(str);
            switch (ret)
            {
                case 0:
                    Console.WriteLine("{0} -- no response", str);
                    return;
                case -1:
                    Console.WriteLine("{0} -- {1}", str, str);
                    return;
                default:
                    Console.WriteLine("{0} -- collision", str);
                    foreach (string s in expand(str))
                    {
                        compare_core(s);
                    }
                    break;
            }
        }
		private int Compare(string s){
			if (m_collection.Count == 0)
				return 0;
			int ret = 0;
			int len = m_collection [0].Length;
			foreach(string str in m_collection){
				bool m_bool = true;
				for(int i = 0;i < s.Length;i++){
					if (!str [i].Equals (s [i])) {
						m_bool = false;	
					}
					if (s.Equals (str)) {
						return -1;
					}
				}
				if (m_bool)
					ret++;
			}
			return ret;
		}
		private void SingleStep(){
			Console.WriteLine ("=====Start Loop======");
			//Loop ("0");
            compare_core("");
			Console.WriteLine ("=====End Loop======");
			if (m_collection.Count == 0)
				Environment.Exit(0);
		}
		public void StartLoop(){
			InitQueue ();
			SingleStep ();
		}
	}
}

