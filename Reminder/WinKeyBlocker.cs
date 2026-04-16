using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reminder
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class WinKeyBlocker
    {
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private static LowLevelKeyboardProc _proc;
        private static IntPtr _hookID = IntPtr.Zero;

        public static void Install()
        {
            _proc = HookCallback;
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void Uninstall()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int[] keys = { 8, 13,  27, 8, 46, 91, 92, 93, 112, 119, 160, 162, 163, 164, 165 }; // 左Win、右Win、Esc、Backspace、Delete、Enter、Ctrl、Shift、Alt
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                // 屏蔽 左Win (91) 和 右Win (92)
               if (keys.Contains(vkCode))
                {
                    return (IntPtr)1; // 返回 1 表示拦截，不传递给系统
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }

}
