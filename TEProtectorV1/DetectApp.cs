using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace TEProtectorV1
{
    class DetectApp
    {
        string path { get; set; }
        Process deny { get; set; }

        public DetectApp() { path = ""; deny = null; }

        public void DenyApp()
        {
            deny = new Process();
            path = AppDomain.CurrentDomain.BaseDirectory;
            deny.StartInfo.FileName = path + "\\TEProtectorDuo.exe";
            while (1 != 2)
            {
                if (CheckTask("TEProtectorDuo") == false)
                {
                    deny.Start();
                }
                Thread.Sleep(200);
            }
        }

        public void EndTask(string taskname)
        {
            string processName = taskname;
            string fixstring = taskname.Replace(".exe", "");

            if (taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(fixstring))
                {
                    process.Kill();
                }
            }
            else if (!taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                }
            }
        }

        public bool CheckTask(string taskname)
        {
            string processName = taskname;
            string fixstring = taskname.Replace(".exe", "");

            if (taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(fixstring))
                {
                    return true;
                }
            }
            else if (!taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
