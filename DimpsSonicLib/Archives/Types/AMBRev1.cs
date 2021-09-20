using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBSubHeader1
    {
        public uint fileCount;
        public uint listPointer;
        public uint unknown1;
        public uint dataPointer;
        public uint nameTable;
        public uint unknown2;
    }

    public class AMBFileIndex1
    {
        public uint  filePointer;
        public uint  unknown1;
        public uint  fileSize;
        public uint  unknown2;
        public short USR0;
        public short USR1;
    }
}
