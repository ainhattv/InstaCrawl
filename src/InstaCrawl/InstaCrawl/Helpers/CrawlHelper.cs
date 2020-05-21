using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrawl.Helpers
{
    public static class CrawlHelper
    {
        public static void CrawlProfile(string userName)
        {
            string pattern = "-m instalooter user {0} {1}";
            string profile_folder = $"profiles/{userName}";
            string cmd = string.Format(pattern, userName, profile_folder);
            string exeToolPath = "C:/Users/ainha/AppData/Local/Programs/Python/Python38-32/python.exe";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = exeToolPath;
            start.Arguments = string.Format("{0}", cmd);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
    }
}
