using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services
{
    public class YtbConverter
    {
        private readonly string root = Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "Uploads");
        public string ConvertSong(YTArguments args)
        {
            var process = new ProcessHandler();
            var path = $"{ root }/{ args.Name }.mp3";
            args.Output = $"-o {path}";
            args.Format = "-x --audio-format mp3";
            process.Start(args);
            return path;
        }
    }
}
