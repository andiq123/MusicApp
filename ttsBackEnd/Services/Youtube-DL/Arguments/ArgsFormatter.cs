using System.IO;
using ttsBackEnd.Services.Helpers;
using ttsBackEnd.Services.YoutubeDL.Arguments;
using ttsBackEnd.Services.YoutubeDL.Entities;
using ttsBackEnd.Services.YoutubeDL.Format;

namespace ttsBackEnd.Services.YoutubeDL
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
