using System;
using System.Collections.Generic;
using System.Text;

namespace MetaNET.DataHelper
{
	public class OrderBy
	{
        public static string DESC(string name)
        {
            return name + " DESC";
        }
        public static string ASC(string name)
        {
            return name + " ASC";
        }
        public static string MERGE(params string[] order)
        {
            return string.Join(",", order);
        }
	}
}
