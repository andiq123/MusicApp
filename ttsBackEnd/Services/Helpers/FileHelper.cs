using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttsBackEnd.Services.Helpers
{
    public static class FileHelper
    {
        public static bool CheckFileExist(this string path)
        {
            return File.Exists(path);
        }

        public static void EnsureDirectoryExists(this string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public static string checkNameForBadChars(this string Name)
        {
            char[] badWords = new char[] { '$', '%', '&', '#', '@', '!', '|', '?', ' ', '_' };
            foreach (var word in badWords)
            {
                if (Name.Contains(word)) Name = Name.Replace($"{word}", "");
            }
            return Name;
        }
    }
}
