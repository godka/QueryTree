using System;
using System.Collections.Generic;
namespace test1
{
    public class QTopr
    {
        private int times;
        private List<string> m_collection;
        public QTopr()
        {
            Console.WriteLine("Created by MythKAst");
            times = 0;
        }
        #region init
        private void InitQueue()
        {
            times = 0;
            m_collection = new List<string>();
            for (; ; )
            {
                Console.Write("Input str in to RFID_Queue,input '#' to start,input 'q' to quit\n:");
                string str = Console.ReadLine();
                if (str.Equals("q"))
                {
                    Environment.Exit(0);
                }
                if (str.Equals("#"))
                {
                    break;
                }
                else
                {
                    if (str.Equals(string.Empty) || str.Equals(""))
                    {
                        Console.WriteLine("nothing to do", str);
                        continue;
                    }
                    bool mbool = false;
                    foreach (char s in str)
                    {
                        if (!s.Equals('0') && !s.Equals('1'))
                        {
                            mbool = true;
                        }
                    }
                    if (mbool)
                    {
                        Console.WriteLine("Illegal str");
                        continue;
                    }
                    if (!m_collection.Contains(str))
                    {
                        m_collection.Add(str);
                    }
                    else
                    {
                        Console.WriteLine("{0} has already in RFID_QUEUE", str);
                    }
                }
            }
            int len = 0;
            foreach (string s in m_collection)
            {
                if (s.Length > len)
                {
                    len = s.Length;
                }
            }
            for (int i = 0; i < m_collection.Count; i++)
            {
                if (m_collection[i].Length < len)
                {
                    for (int j = m_collection[i].Length; j < len; j++)
                    {
                        m_collection[i] = "0" + m_collection[i];
                    }
                }
                Console.WriteLine(m_collection[i]);
            }
        }
        #endregion
        #region mythkast_method
        private string[] expand(string str)
        {
            string[] ret = { str + "0", str + "1" };
            return ret;
        }
        private void compare_core(string str)
        {
            times++;
            string mstr = string.Empty;
            var ret = strCompare(str, out mstr);
            switch (ret)
            {
                case 0:
                    Console.WriteLine("{0} -- {1}-- no response", times, str);
                    return;
                case 1:
                    Console.WriteLine("{0} -- {1} -- {2}", times, str, mstr);
                    return;
                default:
                    Console.WriteLine("{0} -- {1} -- collision", times, str);
                    foreach (string s in expand(str))
                    {
                        compare_core(s);
                    }
                    break;
            }
        }
#endregion
        private int strCompare(string s, out string mstr)
        {
            mstr = string.Empty;
            if (m_collection.Count == 0)
                return 0;
            int ret = 0;
            int len = m_collection[0].Length;
            foreach (string str in m_collection)
            {
                bool m_bool = true;
                if (s.Equals(str))
                {
                    mstr = str;
                    return 1;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    if (!str[i].Equals(s[i]))
                    {
                        m_bool = false;
                    }
                }
                if (m_bool)
                {
                    mstr = str;
                    ret++;
                }
            }
            return ret;
        }
        private void SingleStep()
        {
            Console.WriteLine("=====Start Loop(mythkast)===");
            //Loop ("0");
            compare_core(string.Empty);
            Console.WriteLine("=====End Loop======");
            if (m_collection.Count == 0)
                Environment.Exit(0);
            Console.WriteLine("=====Start Loop(classical)==");
            SingleStepLikeQueue();
            Console.WriteLine("=====End Loop======");
            if (m_collection.Count == 0)
                Environment.Exit(0);
        }
        public void SingleStepLikeQueue()
        {
            //times++;
            times = 0;
            List<string> list = new List<string>();
            list.Add("0"); list.Add("1");
            while (list.Count > 0)
            {
                string mstr = string.Empty;
                string str = list[0];
                var ret = strCompare(list[0], out mstr);
                switch (ret)
                {
                    case 0:
                        Console.WriteLine("{0} -- {1}-- no response", times, str);
                        //nothing to do
                        break;
                    case 1:
                        Console.WriteLine("{0} -- {1} -- {2}", times, str, mstr);
                        break;
                    case 2:
                        Console.WriteLine("{0} -- {1} -- collision", times, str);
                        list.Add(str + "0");list.Add(str + "1");
                        break;
                }
                list.RemoveAt(0);
                times++;
            }
        }
        public void StartLoop()
        {
            InitQueue();
            SingleStep();
            //SingleStepLikeQueue();
        }
    }
}

