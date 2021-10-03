using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public class AMBHeader
    {
        public const string signature = "#AMB";
        public uint version;
        public const short unkVal1 = 0;
        public const short unkVal2 = 0;
        public bool isBigEndian = false;
        public const byte unkVal3 = 0;
        public const byte unkVal4 = 0;
        public byte compressionType = 0;
    }

}
