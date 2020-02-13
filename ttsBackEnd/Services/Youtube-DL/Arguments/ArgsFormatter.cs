using back.Services.Helpers;
using back.Services.YoutubeDL.Arguments;
using back.Services.YoutubeDL.Entities;
using back.Services.YoutubeDL.Format;
using back.Services.YoutubeDL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.YoutubeDL
{
    public static class ArgsFormatter
    {
        //youtube-dl [OPTIONS] URL [URL...]
        public static string FormatMp3(Youtube file, AudioFormats format)
        {
            var activate = AudioArguments.ActivateAudio;
            var audioFormat = AudioArguments.AudioFormat + " " + format;
            var audioQuality = AudioArguments.AudioQuality + " " + (int)AudioQuality.best;
            var output = "-o " + Path.Combine(Paths.Output, file.Title.Split(" ")[0] + "." + format.ToString());
            var arguments = string.Join(' ', activate, audioFormat, audioQuality, output, file.Url);
            return arguments;
        }
        public static string FormatGetInfo(string link)
        {
            var activate = QuietArguments.Activate;
            var getTitle = QuietArguments.GetTitle;
            var getThumbnail = QuietArguments.GetThumbnail;
            var arguments = string.Join(' ', activate, getTitle, getThumbnail, link);
            return arguments;
        }
    }
}
