using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrawl.Helpers
{
    /// <summary>
    /// Docs: https://github.com/rarcega/instagram-scraper
    /// </summary>
    public static class CrawlHelper
    {
        public static void CrawlProfile(string userName)
        {
            string pattern = "{0} -u nhat10a11 -p 15031997X@x -d {1} --latest";
            string profile_folder = $"profiles/{userName}";
            string cmd = string.Format(pattern, userName, profile_folder);
            string exeToolPath = "instagram-scraper";

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

        public static void CrawlHashtag(string hashtag, int maxNewContents = 10000)
        {
            string pattern = "--tag {0} -d {1} --latest --maximum maxNewContents";
            string hashtag_folder = $"hashtags/{hashtag}";
            string cmd = string.Format(pattern, hashtag, hashtag_folder, maxNewContents);
            string exeToolPath = "instagram-scraper";

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
