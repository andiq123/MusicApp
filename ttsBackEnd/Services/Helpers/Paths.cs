using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttsBackEnd.Services.Helpers
{
    public static class Paths
    {
        public static readonly string Output = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "Uploads");
        public static readonly string Root = Path.Combine(Environment.CurrentDirectory, "wwwroot", "YoutubeDL");
    }
}
