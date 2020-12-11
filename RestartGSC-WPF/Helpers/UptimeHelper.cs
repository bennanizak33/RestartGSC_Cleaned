using ExecutionWPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RestartGSC_WPF.Helpers
{
    public class UptimeHelper
    {
        public static int GetUptime(string output, LogWriter _logWriter)
        {
            int res = 0;
            try
            {
                string numberDays = output.Substring(output.IndexOf("day") - 3, 3);

                _logWriter.LogWrite($" numberDays : {numberDays}");


                res = int.Parse(numberDays.Trim());
            }
            catch (Exception ex)
            {
                _logWriter.LogWrite($" GetUptime : {ex.Message}");
                throw  ex;
            }
            return res;
            //TODO
            //throw new NotImplementedException();
        }

        public static string processBatch(string cheminBatch, string serverName, LogWriter _logWriter, ref int exitCode)
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            ProcessStartInfo processInfo = new ProcessStartInfo(path + "\\" + cheminBatch)
            {
#if DEBUG
                Arguments = String.Format("{0}", "10"),
#else
                Arguments = String.Format("{0}", serverName),
#endif
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };
            processInfo.UseShellExecute = false;
            Process process = Process.Start(processInfo);
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            _logWriter.LogWrite(Environment.NewLine +
                                "output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output) + Environment.NewLine +
                                 "error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error) + Environment.NewLine +
                                 "ExitCode>> " + exitCode.ToString());
            process.Close();

            return output;
        }

    }
}
