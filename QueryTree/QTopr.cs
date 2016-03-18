using System;
using System.Collections.Generic;
namespace test1
{
	public class QTopr
	{
		private List<string> m_collection;
		public QTopr (){
			Console.WriteLine ("Created by MythKAst");
		//	m_collection = new List<string> ();
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

		private string ChangeWhileNoResponse(string str){
			if (str.Equals (""))
				return string.Empty;
			int tmp = Convert.ToInt32 (str, 2);
			tmp++;
			string tmpstr = Convert.ToString (tmp, 2);
			for (int i = tmpstr.Length; i < str.Length; i++) {
				tmpstr = "0" + tmpstr;
			}
			if (tmpstr.Length > m_collection [0].Length) {
				return string.Empty;
			} else {
				return tmpstr;
			}
		}

		private string ChangeWhileCollision(string str){
			string tmp = str + "0";
			if (tmp.Length > m_collection [0].Length) {
				return string.Empty;
			} else {
				return tmp;
			}
		}

		private string ChangeWhileCorrect(string str){
			if (ChangeWhileNoResponse (str).Equals(string.Empty))
				return string.Empty;
			if(str.Length == 0)return string.Empty;
			string tmp = str.Substring (0, str.Length - 1);
			if (str [str.Length - 1] == '0') {
				return tmp + "1";
			} else {
				return ChangeWhileNoResponse (tmp);
			}
		}
		private void Loop(string status){
			if (m_collection.Count == 0)
				return;
			string tmp = string.Empty;
			int ret = Compare (status);
			switch (ret) {
			case 0:
				Console.WriteLine ("{0} -- no response", status);
				tmp = ChangeWhileNoResponse (status);
				break;
			case -1:
				Console.WriteLine ("{0} -- {1}", status, status);
				tmp = ChangeWhileCorrect (status);
				break;
			default:
				Console.WriteLine ("{0} -- collision",status);
				tmp = ChangeWhileCollision (status);
				break;
			}
			if (tmp.Equals (string.Empty)) {
				return;
			} else {
				Loop (tmp);
			}
			
		}
		private void SingleStep(){
			Console.WriteLine ("=====Start Loop======");
			Loop ("0");
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

