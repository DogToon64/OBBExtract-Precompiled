using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimpsLib.Archives
{
    public class BinderHeader
    {
        public const string signature = "#AMB";
        public uint version;
        public ushort unknownEditorVal = 0;
        public ushort unknownEditorVal2 = 4;
        public bool isBigEndian;
        public byte unknownEditorVal3 = 0;
        public byte unknownEditorVal4 = 0;
        public byte compresionType; //0 = None, 1 = Unknown, 2 = zlib(Default, no preset dict.)
    }

    public class BinderSubHeader
    {
        public uint fileCount;
        public uint listPointer;
        public uint dataPointer;
        public uint nameTablePointer;
    }

    public class BinderFileEntry
    {
        public uint dataPointer;
        public uint dataLength;
        public uint unknown1;
        public short usr0;
        public short usr1;
    }

    public class BinderFileName
    {
        public string binderFileName;
    }
}
