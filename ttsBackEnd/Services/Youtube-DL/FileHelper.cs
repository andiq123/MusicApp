using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.YoutubeDL
{
    public static class FileHelper
    {
        public static bool CheckExist(string path)
        {
            return File.Exists(path);
        }
    }
}
