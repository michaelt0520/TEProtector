using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEProtectorV1.UserControlContent;
using System.Windows;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace TEProtectorV1.TabsController
{
    public class ChildModeController
    {
        public ChildModeContent childmode;
        Thread disableTaskMgr;
        Thread BlockAppThread;
        private bool IsDisableTaskMgr { get; set; }

        private List<string> listlink;
        private List<string> listapp;
        public ChildModeController()
        {
            childmode = new ChildModeContent()
            {
                Visibility = Visibility.Hidden
            };

            listlink = new List<string>();
            listapp = new List<string>();

            childmode.btnConfirm.Click += (sender, e) =>
            {
                if (childmode.chkbDisableTaskMng.IsChecked == true)
                {
                    disableTaskMgr = new Thread(KillTaskMgr) { IsBackground = true };
                    disableTaskMgr.Start();
                    IsDisableTaskMgr = true;
                }
                else
                {
                    if (IsDisableTaskMgr == true)
                    {
                        disableTaskMgr.Abort();
                        disableTaskMgr = new Thread(KillTaskMgr) { IsBackground = true };
                    }
                }

                if (!File.Exists("CopyHostsFile\\hosts"))
                    File.Create("CopyHostsFile\\hosts");

                if (listlink.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter("CopyHostsFile\\hosts"))
                    {
                        for (int i = 0; i < listlink.Count; i++)
                        {
                            sw.WriteLine("127.0.0.1 " + listlink[i]);
                        }
                    }
                    using (StreamWriter sw = new StreamWriter("CopyHostsFile\\backuphosts"))
                    {
                        for (int i = 0; i < listlink.Count; i++)
                        {
                            sw.WriteLine("127.0.0.1 " + listlink[i]);
                        }
                    }
                }
                else
                {
                    Process.Start("CopyHostsFile\\DeleteHostsFile.exe");
                }
                Process.Start("CopyHostsFile\\CopyHostsFile.exe");

                if (listapp.Count > 0)
                {
                    BlockAppThread = new Thread(BlockApplication) { IsBackground = true };
                    BlockAppThread.Start();
                }

            };

            childmode.btnAddLink.Click += (sender, e) =>
            {
                if (childmode.txtLink.Text != "")
                {
                    string check = listlink.Where(p => p == childmode.txtLink.Text).SingleOrDefault();
                    if (check == null)
                    {
                        listlink.Add(childmode.txtLink.Text);
                        childmode.dataGridListLinkBlock.ItemsSource = listlink;
                        childmode.dataGridListLinkBlock.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Đường dẫn đã tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            childmode.btnRemoveLink.Click += (sender, e) =>
            {
                string removelink = "";
                try
                { removelink = childmode.dataGridListLinkBlock.SelectedItem.ToString(); }
                catch { }
                if (removelink != "")
                {
                    listlink.Remove(removelink);
                    childmode.dataGridListLinkBlock.ItemsSource = listlink;
                    childmode.dataGridListLinkBlock.Items.Refresh();
                }
            };

            childmode.btnBrowseApp.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                { Filter = "Application files (*.exe)|*.exe" };
                if (openFileDialog.ShowDialog() == true)
                    childmode.txtApp.Text = openFileDialog.FileName;
            };

            childmode.btnAddApp.Click += (sender, e) =>
            {
                if (childmode.txtApp.Text != "")
                {
                    string check = listapp.Where(p => p == childmode.txtApp.Text).SingleOrDefault();
                    if (check == null)
                    {
                        listapp.Add(childmode.txtApp.Text);
                        childmode.dataGridListAppBlock.ItemsSource = listapp;
                        childmode.dataGridListAppBlock.Items.Refresh();
                    }
                    else
                        MessageBox.Show("Ứng dụng đã tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            childmode.btnRemoveApp.Click += (sender, e) =>
            {
                string removeapp = "";
                try
                { removeapp = childmode.dataGridListAppBlock.SelectedItem.ToString(); }
                catch { }
                if (removeapp != "")
                {
                    listapp.Remove(removeapp);
                    childmode.dataGridListAppBlock.ItemsSource = listapp;
                    childmode.dataGridListAppBlock.Items.Refresh();
                }
            };
        }

        private void EndTask(string taskname)
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

        private bool CheckTask(string taskname)
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

        private string GetFullNameApp(string hreflink)
        {
            string filename = "";
            Uri uri = new Uri(hreflink);
            if (uri.IsFile)
            {
                filename = System.IO.Path.GetFileName(uri.LocalPath);
            }
            return filename;
        }

        private void KillTaskMgr()
        {
            while (1 != 0)
            {
                if (CheckTask("Taskmgr") == true)
                    EndTask("Taskmgr");
                Thread.Sleep(100);
            }
        }

        private void BlockApplication()
        {
            BlockApp(listapp);
        }

        private void BlockApp(List<string> listapp)
        {
            while (1 != 0)
            {
                for (int i = 0; i < listapp.Count; i++)
                {
                    string filename = GetFullNameApp(listapp[i]);
                    if (filename != "")
                    {
                        if (CheckTask(filename) == true)
                        {
                            EndTask(filename);
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
