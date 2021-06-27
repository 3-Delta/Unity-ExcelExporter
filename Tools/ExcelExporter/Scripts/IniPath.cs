using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class IniPath {
        public static string ExcelFolderPath { get; set; }
        public static string ExcelIniFilePath { get; set; }

        // 类定义导出
        public static string ServerClassOutputDirPath { get; set; }
        public static string ClientClassOutputDirPath { get; set; }

        // 类数据导出
        public static string ServerFormOutputDirPath { get; set; }
        public static string ClientFormOutputDirPath { get; set; }

        // 导出的sheetlist配置
        public static string TmpExportListIniFilePath { get; set; }
        // 导出的类，类数据配置
        public static string TmpExportDataIniFilePath { get; set; }
    }
}
