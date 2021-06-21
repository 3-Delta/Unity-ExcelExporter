using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace SVN {
    // 处理SVN
    public class SVNHelper {
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
            CMDProcess.Exec(workDirectory, "TortoiseProc.exe", arg);
        }
    }

    // 处理进程
    public class CMDProcess {
        public static void Exec(string workDirectory, string fileName, string arg, bool waitForExit = false) {
            Process process = new Process();
            // 需要安装 svn命令行
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = true;

            // workingDirectory的意思是：如果args是全路径，则不设置workDir也行，如果只是一个文件名，则必须设置workDir
            // 可以理解为：workDirectory + fileName = 最终的路径
            process.StartInfo.WorkingDirectory = workDirectory;

            process.Start();
            if (waitForExit) {
                // waitForExit的意思是：A启动一个进程B之后，阻塞当前进程A直到B退出，A才能重新激活
                process.WaitForExit();
            }
        }
    }
}
