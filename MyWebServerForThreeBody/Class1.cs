using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServerForThreeBody
{
    class Class1
    {
        static public void Main(string[] args)
        {
            var p = new MyWebServer();
            p.StartListen();
        }
    }
}
