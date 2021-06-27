using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class Loger {
        public static void Print(string arg) {
            Console.WriteLine(arg);
        }
        public static void Print(StringBuilder sb, string arg, bool print = true) {
            sb.Append(arg);

            if (print) {
                Console.WriteLine(arg);
            }
        }
    }
}
