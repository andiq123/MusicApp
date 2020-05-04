using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ttsBackEnd.Services.Helpers;
using ttsBackEnd.Services.Youtube_DL.Models;

namespace ttsBackEnd.Services
{
    public class ProcessHandler
    {
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
            if (line != null && line.Contains("[download]"))
            {
                args.status = line;
                ProgressEvent?.Invoke(this, args);
            }
        }

        public ProcessStartInfo ProcessInfo(string args)
        {
            var processInfo = new ProcessStartInfo(Paths.Root + "\\youtube-dl.exe", args);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            return processInfo;
        }

    }
}
