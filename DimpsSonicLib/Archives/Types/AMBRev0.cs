using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBSubHeader
    {
        public uint fileCount;
        public uint listPointer;
        public uint dataPointer;
        public uint nameTable;
    }

    public class AMBFileIndex
    {
        public uint  filePointer;
        public uint  fileSize;
        public uint  unknown1;
        public ushort USR0;
        public ushort USR1;
    }
}
