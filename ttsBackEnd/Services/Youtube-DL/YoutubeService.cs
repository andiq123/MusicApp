using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ttsBackEnd.Services.Helpers;
using ttsBackEnd.Services.Youtube_DL.Models;
using ttsBackEnd.Services.YoutubeDL;
using ttsBackEnd.Services.YoutubeDL.Entities;
using ttsBackEnd.Services.YoutubeDL.Format;
using ttsBackEnd.Services.YoutubeDL.Models;

namespace ttsBackEnd.Services
{
    public class YoutubeService
    {
        private ProcessHandler _process;

        public YoutubeService()
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

        public async Task<string> ConvertMp3(Youtube file)
        {
            var arguments = ArgsFormatter.FormatMp3(file, AudioFormats.mp3);
            _process.ProgressEvent += _process_ProgressEvent;
            List<string> output = await Task.Run(() => _process.Start(arguments, true));
            var path = Path.Combine(Paths.Output, file.Title.Split(" ")[0] + "." + AudioFormats.mp3.ToString());
            return path;
        }

        public async Task<string> ConvertMp4(Youtube file)
        {
            var arguments = ArgsFormatter.FormatMp4(file, VideoFormats.mp4);
            _process.ProgressEvent += _process_ProgressEvent;
            List<string> output = await Task.Run(() => _process.Start(arguments, true));
            var path = Path.Combine(Paths.Output, file.Title.Split(" ")[0] + "." + VideoFormats.mp4.ToString());
            return path;
        }

        private void _process_ProgressEvent(object source, EventArgs args)
        {
            var eventArgs = args as ProgressArgs;
            Console.WriteLine(eventArgs.status);
        }
    }
}
