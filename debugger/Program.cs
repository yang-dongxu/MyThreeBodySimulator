using System;
using System.Numerics;
using System.IO;
using System.Collections.Generic;
using MyThreeBodySimulator;


namespace debugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Try();
            Console.WriteLine("Hello World!");
        }

        static void Try()
        {
            var b = new MyEnvironment();

            List<Vector3[]> positions = new List<Vector3[]>();
            for (var i = 1; i <= 8000; i++)
            {
                var z = b.StepMove(0.01f);
                positions.Add(new Vector3[] { z[0][0], z[0][1], z[0][2] });
                //Console.WriteLine("{0},{1}",z[0][0],z[0][1]);
            }
            //写入txt存档一下
            string filename = @"position.txt";
            StreamWriter sw = new StreamWriter(filename);
            foreach (var i in positions)
            {
                sw.WriteLine(string.Format("{0}\t{1}\t{2}", i[0], i[1], i[2]));
            }
            sw.Close();
            Console.WriteLine("success!");
            Console.ReadKey();
        }
    }
}
