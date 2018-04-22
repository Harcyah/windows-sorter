using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleApplication1;

namespace WindowsSorter
{
    internal static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int w, int h, bool repaint);

        [DllImport("user32.dll")]
        private static extern bool UpdateWindow(IntPtr hwnd);
        
        [DllImport("user32.dll")]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref TagWindowinfo info);

        public static void Main()
        {
            Dictionary<string, Position> positions = new Dictionary<string, Position>();
            
            // Left screen
            positions.Add("Steam", new Position(-1440, 35, 1430, 990));
            positions.Add("firefox", new Position(-1448, 5, 1446, 1028));
            positions.Add("foobar2000", new Position(-1925, 5, 488, 1030));
            
            // Right screen
            positions.Add("Origin", new Position(5, 5, 1065, 840));
            positions.Add("upc", new Position(906, 465, 1014, 600));
            positions.Add("Battle.net", new Position(913, 5, 1000, 600));
            positions.Add("iTunes", new Position(10, 400, 1000, 600));
            positions.Add("thunderbird", new Position(40, 40, 1580, 880));
            positions.Add("SyncBackFree", new Position(5, 500, 1000, 570));
            
            List<string> ignored = new List<string>();
            ignored.Add("AISuite3");
            ignored.Add("rider64");
            ignored.Add("NVIDIA Share");
            ignored.Add("SystemSettings");
            ignored.Add("Microsoft.Photos");
            ignored.Add("ApplicationFrameHost");
            
            Process[] processes = Process.GetProcesses();
            foreach(Process p in processes)
            {
                var title = p.MainWindowTitle;
                var process = p.ProcessName;
                var handle = p.MainWindowHandle;
                
                if (string.IsNullOrEmpty(title))
                {
                    continue;
                }

                if (ignored.Contains(process))
                {
                    continue;
                }
                
                TagWindowinfo info = new TagWindowinfo();
                info.cbSize = (uint) Marshal.SizeOf(info);
                GetWindowInfo(p.MainWindowHandle, ref info);
                
                Console.WriteLine("Processing: " + process);
                Console.WriteLine("Window: " + info.rcWindow.GetSize() + " " + info.rcWindow.GetPosition());        
                Console.WriteLine("Client: " + info.rcClient.GetSize() + " " + info.rcClient.GetPosition());
               
                if (!positions.ContainsKey(process))
                {      
                    Console.WriteLine("Skipping: " + process + " / " + title);
                }
                else
                {
                    Console.WriteLine("Moving: " + process);
                    Position rect = positions[process];
                    var moveWindow = MoveWindow(handle, rect.Left, rect.Top, rect.Width, rect.Height, true);
                    if (!moveWindow)
                    {
                        Console.WriteLine("Unable to move " + process + " window");
                    }
                    UpdateWindow(handle);
                }
            }
            
            Console.WriteLine("Done");
        }
    }
}