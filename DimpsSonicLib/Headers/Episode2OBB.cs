using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsLib.Headers
{
    /// <summary>
    /// Android APK Expansion header for pre-Sega Forever versions of the game
    /// </summary>
    public class OBBHeaderv1
    {
        public const string signature = "LPK";
        public ushort unk1;
        public ushort fileCount;
        public ushort dirCount;
        public ushort unk2;

        public uint unkDataPointer;
        public uint unkDataLength;
        public uint listPointer;
        public uint nameTablePointer;
        public uint nameTableLength;
    }

    /// <summary>
    /// Android APK Expansion header for Sega Forever versions of the game
    /// </summary>
    public class OBBHeaderv2 
    {
        public const string signature = "LPK";
    }

    public class OBBFileEntry
    {
        public uint filePtr;
        public uint fileSize;
        public uint fileSize2;
        public ushort dirID;
        public ushort nameID;
    }

    public class OBBFileInfo
    {
        public string fileDirectory;
        public string fileName;
    }
}
