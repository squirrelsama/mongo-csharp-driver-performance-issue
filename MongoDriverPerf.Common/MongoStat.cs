using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace MongoDriverPerf
{
    public class MongoStat
    {
        public static void Watch(TextWriter writer, Action act)
        {
            var p = Process.Start(new ProcessStartInfo
            {
                FileName = "mongostat",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });

            Task.Factory.StartNew(() =>
            {
                string s;
                while (!p.HasExited && (s = p.StandardOutput.ReadLine()) != null)
                        writer.WriteLine(s);
            });

            act();
            p.Kill();
        }
    }
}

