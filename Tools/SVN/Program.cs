using System;

namespace SVNProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            // 全路径不配置 worlDir也可以
            SVNHelper.Commit("I:\\Project\\SVNRepo\\test.txt");

            // "I:\\Project\\SVNRepo"的worlDir +  "\\test.txt" 一起工作
            // SVNHelper.Commit("test.txt");

            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
