using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.Helpers
{
    public static class FileHelper
    {
        public static bool CheckFileExist(string path)
        {
            return File.Exists(path);
        }

        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}
