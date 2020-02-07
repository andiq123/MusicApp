using back.Services.Youtube_DL.Models;
using back.Services.YoutubeDL;
using back.Services.YoutubeDL.Entities;
using back.Services.YoutubeDL.Format;
using back.Services.YoutubeDL.Models;
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
        private ProcessHandler _process;

        public YtbConverter()
        {
            _process = new ProcessHandler();
        }

        public async Task<Youtube> GetInfo(YoutubeDto file)
        {
            string args = ArgsFormatter.FormatGetInfo(file.Url);
            List<string> info = await Task.Run(() => _process.Start(args, false));
            var title = info[0];
            var thumbnail = info[1];
            Youtube fileEntity = new Youtube() { Title = title, Thumbnail = thumbnail, Url = file.Url };
            return fileEntity;
        }

        public async Task<string> Convert(Youtube file)
        {
            var arguments = ArgsFormatter.FormatMp3(file, AudioFormats.mp3);
            _process.ProgressEvent += _process_ProgressEvent;
            List<string> output = await Task.Run(() => _process.Start(arguments, true));
            var path = Path.Combine(Paths.Output, file.Title.Split(" ")[0] + "." + AudioFormats.mp3.ToString());
            return path;
        }

        private void _process_ProgressEvent(object source, EventArgs args)
        {
            var eventArgs = args as ProgressArgs;
            Console.WriteLine(eventArgs.status);
        }
    }
}
