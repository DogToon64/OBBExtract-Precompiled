using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Archives
{
    public class AMB
    {
        // The type returned from ReadAMB().. the problem is identifying the object types after getting everything.
        public object Header { get; set; }
        public object SubHeader { get; set; }
        public object FileIndex { get; set; }
        public MemoryBinder.Version Version { get; set; }
    }

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

        public static AMB ReadAMB(Stream stream)
        {
            var reader = new MemoryBinderReader(stream);
            var header = reader.ReadHeader();
            reader.JumpTo(16);
            var ver = GetAMBVersion(header);

            if (ver != Version.Unknown)
            {
                var subHeader = reader.ReadSubHeader(ver);
                if (header.compressionType != 0)
                    throw new Exception("Compressed binders are currently not supported");

                var fileIndex = reader.ReadFileIndex(subHeader, ver);

                return new AMB { Header = header, SubHeader = subHeader, FileIndex = fileIndex, Version = ver };
            }
            else throw new NotImplementedException("Unknown AMB Version");
        }

        public static void WriteBinder()
        {
            throw new NotImplementedException("Writing of Memory Binders has not yet been implemented!");
        }

    }

    public class MemoryBinderReader : ExtendedBinaryReader
    {
        public MemoryBinderReader(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public MemoryBinderReader(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }

        // Methods
        /// <summary>
        /// Parses the header of the a loaded AMB file
        /// </summary>
        /// <returns>Returns a new AMBHeader type</returns>
        public AMBHeader ReadHeader()
        {
            AMBHeader header = new AMBHeader { };

            var sig = ReadSignature(4);

            if (sig != AMBHeader.signature)
            {
                throw new Exception("Signature of input does not match.");
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

        /// <summary>
        /// Parses the sub-header of a loaded AMB file
        /// </summary>
        /// <param name="version">The version of the curently loaded AMB file, used to determine the correct reading method</param>
        /// <returns>Returns a new AMBSubHeader type</returns>
        public object ReadSubHeader(MemoryBinder.Version version)
        {
            if (version == MemoryBinder.Version.Rev0)
            {
                AMBSubHeader subHeader = new AMBSubHeader { };

                subHeader.fileCount   = ReadUInt32();
                subHeader.listPointer = ReadUInt32();
                subHeader.dataPointer = ReadUInt32();
                subHeader.nameTable   = ReadUInt32();

                return subHeader;
            }
            else if (version == MemoryBinder.Version.Rev1)
            {
                AMBSubHeader1 subHeader = new AMBSubHeader1 { };

                subHeader.fileCount   = ReadUInt32();
                subHeader.listPointer = ReadUInt32();
                subHeader.unknown1    = ReadUInt32();
                subHeader.dataPointer = ReadUInt32();
                subHeader.nameTable   = ReadUInt32();
                subHeader.unknown2    = ReadUInt32();

                return subHeader;
            }
            else if (version == MemoryBinder.Version.Rev2)
            {
                AMBSubHeader2 subHeader = new AMBSubHeader2 { };

                subHeader.fileCount   = ReadUInt64();
                subHeader.listPointer = ReadUInt64();
                subHeader.dataPointer = ReadUInt64();
                subHeader.nameTable   = ReadUInt64();

                return subHeader;
            }
            else throw new NotImplementedException("Unknown AMB Version");
        }

        /// <summary>
        /// Parses the file index of a loaded AMB file
        /// </summary>
        /// <param name="subHeader">The sub-header of the currently loaded AMB file, used to parse binder contents</param>
        /// <param name="version">The version of the curently loaded AMB file, used to determine the correct reading method</param>
        /// <returns></returns>
        public object ReadFileIndex(object subHeader, MemoryBinder.Version version)
        {
            if (version == MemoryBinder.Version.Rev0)
            {
                List<AMBFileIndex> fileIndexList = new List<AMBFileIndex>();
                AMBSubHeader sub = (AMBSubHeader)subHeader;

                JumpTo(sub.listPointer);

                for (int i = 0; i < (int)sub.fileCount; i++)
                {
                    fileIndexList.Add(new AMBFileIndex()
                    {
                        filePointer = ReadUInt32(),
                        unknown1    = ReadUInt32(),
                        fileSize    = ReadUInt32(),
                        USR0        = ReadUInt16(),
                        USR1        = ReadUInt16(),
                    });
                }

                return fileIndexList;
            }
            else if (version == MemoryBinder.Version.Rev1)
            {
                List<AMBFileIndex1> fileIndexList = new List<AMBFileIndex1>();
                AMBSubHeader1 sub = (AMBSubHeader1)subHeader;

                JumpTo(sub.listPointer);

                for (int i = 0; i < (int)sub.fileCount; i++)
                {
                    fileIndexList.Add(new AMBFileIndex1()
                    {
                        filePointer = ReadUInt32(),
                        unknown1    = ReadUInt32(),
                        fileSize    = ReadUInt32(),
                        unknown2    = ReadUInt32(),
                        USR0        = ReadUInt16(),
                        USR1        = ReadUInt16(),
                    });
                }

                return fileIndexList;
            }
            else if (version == MemoryBinder.Version.Rev2)
            {
                List<AMBFileIndex2> fileIndexList = new List<AMBFileIndex2>();
                AMBSubHeader2 sub = (AMBSubHeader2)subHeader;

                JumpTo((long)sub.listPointer);

                for (int i = 0; i < (int)sub.fileCount; i++)
                {
                    fileIndexList.Add(new AMBFileIndex2()
                    {
                        filePointer = ReadUInt32(),
                        unknown1    = ReadUInt32(),
                        unknown2    = ReadUInt32(),
                        fileSize    = ReadUInt32(),
                        unknown3    = ReadUInt32(),
                        USR0        = ReadUInt16(),
                        USR1        = ReadUInt16(),
                    });
                }

                return fileIndexList;
            }
            else throw new NotImplementedException("Unknown AMB Version");
        }

    }

    public class MemoryBinderWriter : ExtendedBinaryWriter
    {
        public MemoryBinderWriter(Stream output, bool isBigEndian = false) : base(output, isBigEndian) { }
        public MemoryBinderWriter(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, isBigEndian) { }

        // Methods


    }
}