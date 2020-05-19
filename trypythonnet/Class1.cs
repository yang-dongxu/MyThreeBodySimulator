using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace tryofpythonnet
{
    public class Class1
    {
        public string a = "wocao";
        static public void hello()
        {
            Console.WriteLine("hello world");
        }

        static public void hello(string name)
        {
            Vector3  vector3 = new Vector3(1, 1, 1);
            Console.WriteLine("hello {0}", name);
        }
    }

    
}
