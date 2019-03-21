using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsSorter
{
    internal static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int w, int h, bool repaint);

        [DllImport("user32.dll")]
        private static extern bool UpdateWindow(IntPtr hwnd);
        
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);
        
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        private static Process FindProcessByName(Process[] processes, String name)
        {
            foreach (Process p in processes)
            {
                if ((p.ProcessName == name) && (p.MainWindowHandle.ToInt32() > 0))
                {
                    return p;
                }
            }

            throw new ProcessNotFoundException();
        }

        public static void Main(String[] args)
        {
            if (args.Length != 5)
            {
                throw new ArgumentException("Expecting 5 args, found " + args.Length);
            }

            try
            {
                string name = args[0];
                int left = int.Parse(args[1]);
                int top = int.Parse(args[2]);
                int width = int.Parse(args[3]);
                int height = int.Parse(args[4]);
                Console.WriteLine("Processing: {0} position:{1},{2} size:{3},{4}", name, left, top, width, height);

                Process[] processes = Process.GetProcessesByName(name);
                Process process = FindProcessByName(processes, name);
                Console.WriteLine("Found process: {0}", process.ProcessName);
                
                var handle = process.MainWindowHandle;
                var moveWindow = MoveWindow(handle, left, top, width, height, true);
                if (!moveWindow)
                {
                    Console.WriteLine("Unable to move {0} window", name);
                }

                UpdateWindow(handle);
                ShowWindow(handle, SW_RESTORE);
                SetForegroundWindow(handle);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to read arguments");
            }
        }

        private const int SW_RESTORE = 9;
    }
}