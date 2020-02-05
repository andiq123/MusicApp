using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Services
{
    public class ProcessHandler
    {
        private readonly string root = Path.Combine(Environment.CurrentDirectory, "wwwroot", "YoutubeDL");

        public void Start(YTArguments args)
        {
            var processInfo = ProcessInfo(args);
            var process = Process.Start(processInfo);
            process.OutputDataReceived += Process_OutputDataReceived;
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            Console.ReadLine();
        }

        public ProcessStartInfo ProcessInfo(YTArguments args)
        {
            var arguments = string.Join(" ", args.Format, args.Url, args.Output);
            var processInfo = new ProcessStartInfo("youtube-dl.exe", arguments);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            return processInfo;
        }

    }
}
