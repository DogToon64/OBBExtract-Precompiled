using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBSubHeader2
    {
        public ulong fileCount;
        public ulong listPointer;
        public ulong dataPointer;
        public ulong nameTable;
    }

    public class AMBFileIndex2
    {
        public uint  filePointer;
        public uint  unknown1;
        public uint  unknown2;
        public uint  fileSize;
        public uint  unknown3;
        public ushort USR0;
        public ushort USR1;
    }
}