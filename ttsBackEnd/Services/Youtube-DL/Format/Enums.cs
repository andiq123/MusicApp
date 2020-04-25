using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttsBackEnd.Services.YoutubeDL.Format
{
    public enum AudioQuality
    {
        best = 0,
        good = 5,
        worst = 9
    }
    public enum AudioFormats
    {
        flac,
        mp3,
        m4a,
        opus,
        vorbis,
        wav,
        best
    }
    public enum VideoFormats
    {
        mp4,
        mkv
    }
}
