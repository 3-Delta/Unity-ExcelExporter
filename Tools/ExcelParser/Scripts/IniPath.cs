using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParser {
    public class IniPath {
        public static string ExcelFolderPath { get; set; }
        public static string ExcelIniFilePath { get; set; }

        public static string ServerOutputDirPath { get; set; }
        public static string ClientOutputDirPath { get; set; }

        public static string TmpIniFilePath { get; set; }
    }
}
