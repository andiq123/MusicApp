using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttsBackEnd.Services.YoutubeDL.Arguments
{
    public static class AudioArguments
    {
        public static string ActivateAudio { get; set; } = "-x";
        //Specify audio format: "best", "aac",
        //                         "flac", "mp3", "m4a", "opus", "vorbis", or
        //                         "wav"; "best" by default; No effect without
        //                         -x
        public static string AudioFormat { get; set; } = "--audio-format";
        //Specify ffmpeg/avconv audio quality, insert
        //                          a value between 0 (better) and 9 (worse)
        //                         for VBR or a specific bitrate like 128K
        //                         (default 5)
        public static string AudioQuality { get; set; } = "--audio-quality";
    }
}
