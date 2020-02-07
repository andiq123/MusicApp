using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services.YoutubeDL.Format
{
    public class QuietArguments
    {
        public static string Activate { get; set; } = "-q";
        public static string GetId { get; set; } = "--get-id";
        public static string GetTitle { get; set; } = "--get-title";
        public static string SkipDownload { get; set; } = "--skip-download";
        public static string GetThumbnail { get; set; } = "--get-thumbnail";
        public static string GetDescription { get; set; } = "--get-description";
        public static string GetDuration { get; set; } = "--get-duration";
        public static string GetFileName { get; set; } = "--get-filename";
        public static string GetFormat { get; set; } = "--get-format";
    }
}
