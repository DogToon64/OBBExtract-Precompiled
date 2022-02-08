using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    #region Header
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
    #endregion

    #region Base version
    public class SubHeader
    {
        public dynamic fileCount = 0;
        public dynamic listPointer = 0;
        public dynamic dataPointer = 0;
        public dynamic nameTable = 0;
    }

    public class FileIndex
    {
        public dynamic filePointer = 0;
        public dynamic fileSize = 0;
        public dynamic unknown1 = 0;
        public dynamic USR0 = 0;
        public dynamic USR1 = 0;
    }
    #endregion

    #region Revision 1
    public class SubHeaderR1 : SubHeader
    {
        // uint fileCount
        // uint listPointer
        public uint unknown1 = 0;
        // uint dataPointer
        // uint nameTable
        public uint unknown2 = 0;
    }

    public class FileIndexR1 : FileIndex
    {
        // uint filePointer
        // uint unknown1
        // uint fileSize
        public uint unknown2 = 0;
        // ushort USR0
        // ushort USR1
    }
    #endregion

    #region Revision 2
    public class SubHeaderR2 : SubHeader
    {
        // ulong fileCount
        // ulong listPointer
        // ulong dataPointer
        // ulong nameTable
    }

    public class FileIndexR2 : FileIndex
    {
        // uint filePointer
        // uint unknown1
        public uint unknown2 = 0;
        // uint fileSize
        public uint unknown3 = 0;
        // ushort USR0
        // ushort USR1
    }
    #endregion
}
