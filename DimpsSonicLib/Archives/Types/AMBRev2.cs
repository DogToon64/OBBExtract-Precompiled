using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBRev2SubHeader : AMBSubHeader
    {

    }

    public class AMBRev2Index : AMBFileIndex
    {

    }

    public class AMBRev2 : MemoryBinderReader
    {
        public AMBRev2(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public AMBRev2(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }


    }
}