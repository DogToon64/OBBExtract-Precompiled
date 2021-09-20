using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Archives
{
    public class MemoryBinder : MemoryBinderReader
    {
        public MemoryBinder(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public MemoryBinder(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }

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

        public static void TestingOnlyMethod(Stream stream)
        {
            var reader = new MemoryBinderReader(stream);
            var header = reader.ReadHeader();

            // Do you like how I flip-flop between Switch Statements and "Yandev" Statements? Xd
            switch (GetAMBVersion(header))
            {
                case Version.Rev0:
                    // somethin
                    break;
                case Version.Rev1:
                    // somethin
                    break;
                case Version.Rev2:
                    // somethin
                    break;
                case Version.Unknown:
                    throw new NotImplementedException("Unknown AMB Version");
            }

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
