using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    #region Header
    public class AMBHeader
    {
        public const string signature = "#AMB";
        public uint version { get; set; } = 0;
        public short unkVal1 { get; set; } = 0;
        public short unkVal2 { get; set; } = 0;
        public bool isBigEndian { get; set; } = false;
        public byte unkVal3 { get; set; } = 0;
        public byte unkVal4 { get; set; } = 0;
        public byte compressionType { get; set; } = 0;
    }
    #endregion

    #region Base version
    public class SubHeader
    {
        public dynamic fileCount   { get; set; } = 0;
        public dynamic listPointer { get; set; } = 0;
        public dynamic dataPointer { get; set; } = 0;
        public dynamic nameTable   { get; set; } = 0;
    }

    public class FileIndex
    {
        public dynamic filePointer  { get; set; } = 0;
        public dynamic fileSize     { get; set; } = 0;
        public dynamic unknown1     { get; set; } = 0;
        public ushort  USR0         { get; set; } = 0;
        public ushort  USR1         { get; set; } = 0;
    }
    #endregion

    #region Revision 1
    public class SubHeaderR1 : SubHeader
    {
        // uint fileCount
        // uint listPointer
        public uint unknown1 { get; set; } = 0;
        // uint dataPointer
        // uint nameTable
        public uint unknown2 { get; set; } = 0;
    }

    public class FileIndexR1 : FileIndex
    {
        // uint filePointer
        // uint unknown1
        // uint fileSize
        public uint unknown2 { get; set; } = 0;
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
        public uint unknown2 { get; set; } = 0;
        // uint fileSize
        public uint unknown3 { get; set; } = 0;
        // ushort USR0
        // ushort USR1
    }
    #endregion
}
