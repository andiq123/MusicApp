using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using back.Services.Youtube_DL.Models;
using back.Services.YoutubeDL;
using back.Services.YoutubeDL.Entities;

namespace back.Services
{
    public class ProcessHandler
    {
        private readonly string root = Path.Combine(Environment.CurrentDirectory, "wwwroot", "YoutubeDL");

        public delegate void progressChanged(object source, ProgressArgs args);
        public event progressChanged ProgressEvent;

        public async Task<List<string>> Start(string args, bool reportProgress)
        {
            var processInfo = ProcessInfo(args);
            var process = Process.Start(processInfo);
            process.WaitForExit();

            //Read all the info from the process
            List<string> lines = new List<string>();
            await Task.Run(() =>
            {
                ProgressArgs progressArgs = new ProgressArgs();
                while (!process.StandardOutput.EndOfStream)
                {
                    lines.Add(process.StandardOutput.ReadLine());
                    if (reportProgress) invokeEvent(progressArgs, process.StandardOutput.ReadLine());
                }
            });
            process.Close();
            return lines;
        }
        private void invokeEvent(ProgressArgs args, string line)
        {
            if (line.Contains("[download]"))
            {
                args.status = line;
                ProgressEvent?.Invoke(this, args);
            }
        }

        public ProcessStartInfo ProcessInfo(string args)
        {
            var processInfo = new ProcessStartInfo("youtube-dl.exe", args);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            return processInfo;
        }

    }
}
