using System;

namespace ExcelParser {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            SheetReader.ReadSheet("I:\\Project\\Unity\\Github\\Unity-ExcelExporter\\Resources\\Excel\\CSVTest.xlsx", "CSVTest");

            Console.ReadKey();
        }
    }
}
