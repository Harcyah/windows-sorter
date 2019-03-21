using System.Runtime.InteropServices;
using WindowsSorter;

namespace WindowsSorter
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TagWindowinfo
    {
        public uint cbSize;
        public TagRect rcWindow;
        public TagRect rcClient;
        public readonly uint dwStyle;
        public readonly uint dwExStyle;
        public readonly uint dwWindowStatus;
        public readonly uint cxWindowBorders;
        public readonly uint cyWindowBorders;
        public readonly ushort atomWindowType;
        public readonly ushort wCreatorVersion;
    }
}