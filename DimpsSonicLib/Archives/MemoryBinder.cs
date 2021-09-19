using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Archives
{
    #region AMB Base Classes
    public class AMBSubHeader
    {
        public uint fileCount;
        public uint listPointer;
        public uint dataPointer;
        public uint nameTable;
    }
    public class AMBFileIndex
    {
        public uint filePointer;
        public uint fileSize;
        public uint unkEditorVar5;
        public short USR0;
        public short USR1;
    }
    #endregion

    public class MemoryBinder
    {
        public enum Version
        {
            //Common, Type 1, Type 2
            Rev0, Rev1, Rev2, Unknown
        }

        public static Version GetAMBVersion(AMBHeader header)
        {
            if (header.version == 32)
                return Version.Rev0;
            else if (header.version == 40)
                return Version.Rev1;
            else if (header.version == 48)
                return Version.Rev2;
            else
                return Version.Unknown;
        }

        public static void DevFunc(Stream stream)
        {
            var reader = new MemoryBinderReader(stream);
            var header = reader.ReadHeader();

            switch (GetAMBVersion(header))
            {
                case Version.Rev0:
                    Logger.PrintError("Cast to AMBRev0");
                    break;
                case Version.Rev1:
                    Logger.PrintError("Cast to AMBRev1");
                    break;
                case Version.Rev2:
                    Logger.PrintError("Cast to AMBRev2");
                    break;
                case Version.Unknown:
                    throw new NotImplementedException("Unknown AMB Version");
            }

            // Brain has stopped working. 

        }

    }

    public class MemoryBinderReader : ExtendedBinaryReader
    {
        public MemoryBinderReader(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public MemoryBinderReader(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }


        // Methods

        public AMBHeader ReadHeader()
        {
            AMBHeader header = new AMBHeader { };

            var sig = ReadSignature(4);

            if (sig != AMBHeader.signature)
            {
                throw new Exception("Signature of input does not match");
            }


            JumpAhead(8);
            header.isBigEndian = ReadByte() == 1 ? true : false;

            JumpAhead(2);
            header.compressionType = ReadByte();

            JumpTo(4);
            if (header.isBigEndian)
            {
                IsBigEndian = true; header.version = ReadUInt32();
            }
            else
                header.version = ReadUInt32();

            return header;
        }

        public virtual AMBSubHeader ReadSubHeader()
        {
            throw new NotImplementedException("MemoryBinder.cs does not implement this function. Please call it from a derived class instead");
        }

        public virtual List<AMBFileIndex> ReadFileIndex(uint fileIndexPointer)
        {
            throw new NotImplementedException("MemoryBinder.cs does not implement this function. Please call it from a derived class instead");
        }

    }

    public class MemoryBinderWriter : ExtendedBinaryWriter
    {
        public MemoryBinderWriter(Stream output, bool isBigEndian = false) : base(output, isBigEndian) { }
        public MemoryBinderWriter(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, isBigEndian) { }

        // Methods



    }

}
