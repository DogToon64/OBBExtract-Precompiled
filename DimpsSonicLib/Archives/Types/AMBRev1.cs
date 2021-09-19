using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBRev1SubHeader : AMBSubHeader
    {

    }
    public class AMBRev1Index : AMBFileIndex
    {

    }

    public class AMBRev1 : MemoryBinderReader
    {
        public AMBRev1(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public AMBRev1(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }


    }
}
