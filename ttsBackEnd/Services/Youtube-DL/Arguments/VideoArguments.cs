using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttsBackEnd.Services.YoutubeDL.Arguments
{
    public static class VideoArguments
    {
        public static string ActivateVideo { get; set; } = "-f";
        public static string FormatVideo { get; set; } = "--format";
        public static string DownloadAllVideoFormats { get; set; } = "--all-formats";
        public static string MergeBestFromAll { get; set; } = "--merge-output-format";
    }
}
