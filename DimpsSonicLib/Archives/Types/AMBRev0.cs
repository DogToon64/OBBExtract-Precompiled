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
        public short USR0;
        public short USR1;
    }
}
