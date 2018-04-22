using System;
using System.Runtime.InteropServices;

namespace WindowsSorter
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TagRect
    {
        private readonly int left;
        private readonly int top;
        private readonly int right;
        private readonly int bottom;

        public string GetPosition()
        {      
            return "Position: " + top + "/" + left;        
        }

        public string GetSize()
        {
            int width = Math.Abs(right - left);
            int height = Math.Abs(bottom - top);    
            return "Size: " + width + "/" + height;
        }
    }
}