using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace SVN {
    // 处理SVN
    public static class SVNHelper {
        public static string workDirectory { get; set; } = null;

        public static void Revert(IList<string> paths) {
            if (paths != null && paths.Count > 0) {
                Revert(string.Join("*", paths));
            }
        }
        public static void Update(IList<string> paths) {
            if (paths != null && paths.Count > 0) {
                Update(string.Join("*", paths));
            }
        }
        public static void Commit(IList<string> paths) {
            if (paths != null && paths.Count > 0) {
                Commit(string.Join("*", paths));
            }
        }

        public static void Revert(string paths) {
            string format = string.Format("/command:revert /path:{0}", paths);
            _SVNCmd(format);
        }
        public static void Update(string paths) {
            string format = string.Format("/command:update /path:{0}", paths);
            _SVNCmd(format);
        }
        public static void Commit(string paths) {
            string format = string.Format("/command:commit /path:{0}", paths);
            _SVNCmd(format);
        }

        private static void _SVNCmd(string arg) {
            // 需要安装 svn命令行
            CMDProcess.Exec(workDirectory, "TortoiseProc.exe", arg, true);
        }
    }

    // 处理进程
    public static class CMDProcess {
        public static void Exec(string workDirectory, string fileName, string arg, bool waitForExit = true) {
            Process process = new Process();
            // 需要安装 svn命令行
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WorkingDirectory = workDirectory;

            process.Start();
            if (waitForExit) {
                process.WaitForExit();
            }
        }
    }
}
