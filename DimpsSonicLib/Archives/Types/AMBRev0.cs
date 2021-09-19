using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBRev0SubHeader : AMBSubHeader
    {

    }
    public class AMBRev0Index : AMBFileIndex
    {

    }

    public class AMBRev0 : MemoryBinderReader
    {
        public AMBRev0(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public AMBRev0(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }

        public override AMBSubHeader ReadSubHeader()
        {
            AMBSubHeader SubHeader = new AMBSubHeader { };



            return SubHeader;
        }

        public override List<AMBFileIndex> ReadFileIndex(uint fileIndexPointer)
        {
            List<AMBFileIndex> rev0Index = new List<AMBFileIndex> { };



            return rev0Index;
        }

    }

}
