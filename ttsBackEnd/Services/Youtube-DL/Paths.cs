using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.YoutubeDL
{
    public static class Paths
    {
        public static readonly string Output = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "Uploads");
    }
}
