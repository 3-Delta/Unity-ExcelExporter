using System;

namespace ExcelExporter {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            SheetProcesser.ReadSheet("I:\\Project\\Unity\\Github\\Unity-ExcelExporter\\Resources\\Excel\\FormTest.xlsx", "FormTest");
            //SheetProcesser.ReadSheet("D:\\Project\\Unity\\Unity-ExcelExporter\\Resources\\Excel\\FormTest.xlsx", "FormTest");

            Console.ReadKey();
        }
    }
}
