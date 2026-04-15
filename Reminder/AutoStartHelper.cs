using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reminder
{
    public class AutoStartHelper
    {
        public static bool IsAutoStartEnabled()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false))
            {
                return key != null && key.GetValue("Reminder") != null;
            }
        }

        public static void SetAutoStart(bool enable)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (enable)
                {
                    key.SetValue("Reminder", Application.ExecutablePath);
                }
                else
                {
                    key.DeleteValue("Reminder", false);
                }
            }
        }
    }
}
