using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Archives
{
    public class AMB
    {
        public AMBHeader Header { get; set; }
        public IEnumerable<SubHeader> SubHeader { get; set; }
        public IEnumerable<FileIndex> FileIndex { get; set; }
        public Binder.Version version { get; set; }
    }

    public class Binder
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

        // Loading and Parsing.    TODO: un-fuck this logic
        public static AMB ReadAMB(Stream stream)
        {
            var reader = new BinderReader(stream);
            var header = reader.ReadHeader();
            reader.JumpTo(16);
            var ver = GetAMBVersion(header);

            if (ver != Version.Unknown)
            {
                if (header.compressionType != 0)
                    throw new Exception("Compressed binders are currently not supported");

                var subHeader = reader.ReadSubHeader(ver);
                SubHeader fuck = new SubHeader();

                foreach (SubHeader a in subHeader) 
                {
                    fuck.fileCount   = a.fileCount;
                    fuck.listPointer = a.listPointer;
                    fuck.dataPointer = a.dataPointer;
                    fuck.nameTable   = a.nameTable;
                }                 

                var fileIndex = reader.ReadFileIndex(fuck, ver);

                return new AMB { Header = header, SubHeader = subHeader, FileIndex = fileIndex };
            }
            else throw new NotImplementedException("Unknown AMB Version");
        }

        // Extract to a given directory. DOES NOT BELONG IN LIBRARY
        public static void ExtractAMB(AMB amb, string dir)
        {



        }

        // Hmm                           DOES NOT BELONG IN LIBRARY
        public static void WriteAMB(AMB amb, Version ver = Version.Rev0, bool isBigEndian = false)
        {

           // write from loaded or new AMB

        }
    }


    public class BinderReader : ExtendedBinaryReader
    {
        public BinderReader(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public BinderReader(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }

        // Methods
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

        public IEnumerable<SubHeader> ReadSubHeader(Binder.Version ver)
        {
            if (ver == Binder.Version.Rev0)
            {
                List<SubHeader> sub = new List<SubHeader> { };

                sub.Add(new SubHeader()
                {
                    fileCount = ReadUInt32(),
                    listPointer = ReadUInt32(),
                    dataPointer = ReadUInt32(),
                    nameTable = ReadUInt32(),
                });
                return sub;
            }
            else if (ver == Binder.Version.Rev1)
            {
                List<SubHeaderR1> sub = new List<SubHeaderR1> { };

                sub.Add(new SubHeaderR1()
                {
                    fileCount = ReadUInt32(),
                    listPointer = ReadUInt32(),
                    unknown1 = ReadUInt32(),
                    dataPointer = ReadUInt32(),
                    nameTable = ReadUInt32(),
                    unknown2 = ReadUInt32()
                });
                return sub;
            }
            else if (ver == Binder.Version.Rev2)
            {
                List<SubHeaderR2> sub = new List<SubHeaderR2> { };

                sub.Add(new SubHeaderR2()
                {
                    fileCount = ReadUInt64(),
                    listPointer = ReadUInt64(),
                    dataPointer = ReadUInt64(),
                    nameTable = ReadUInt64(),
                });
                return sub;
            }
            else throw new NotImplementedException("Unknown AMB Version");
        }

        public IEnumerable<FileIndex> ReadFileIndex(SubHeader sub, Binder.Version ver)
        {
            if (ver == Binder.Version.Rev0)
            {
                List<FileIndex> index = new List<FileIndex>();
                SubHeader s = sub;

                JumpTo(s.listPointer);

                for (int i = 0; i < (int)s.fileCount; i++)
                {
                    index.Add(new FileIndex()
                    {
                        filePointer = ReadUInt32(),
                        unknown1 = ReadUInt32(),
                        fileSize = ReadUInt32(),
                        USR0 = ReadUInt16(),
                        USR1 = ReadUInt16(),
                    });
                }
                return index;
            }

            else if (ver == Binder.Version.Rev1)
            {
                List<FileIndexR1> index = new List<FileIndexR1>();
                SubHeader s = sub;

                JumpTo(s.listPointer);

                for (int i = 0; i < (int)s.fileCount; i++)
                {
                    index.Add(new FileIndexR1()
                    {
                        filePointer = ReadUInt32(),
                        unknown1 = ReadUInt32(),
                        fileSize = ReadUInt32(),
                        unknown2 = ReadUInt32(),
                        USR0 = ReadUInt16(),
                        USR1 = ReadUInt16(),
                    }); ;
                }
                return index;
            }

            else if (ver == Binder.Version.Rev2)
            {
                List<FileIndexR2> index = new List<FileIndexR2>();
                SubHeader s = sub;

                JumpTo(s.listPointer);

                for (int i = 0; i < (int)s.fileCount; i++)
                {
                    index.Add(new FileIndexR2()
                    {
                        filePointer = ReadUInt32(),
                        unknown1 = ReadUInt32(),
                        unknown2 = ReadUInt32(),
                        fileSize = ReadUInt32(),
                        unknown3 = ReadUInt32(),
                        USR0 = ReadUInt16(),
                        USR1 = ReadUInt16(),
                    }); ;
                }
                return index;
            }

            else throw new NotImplementedException("Unknown AMB Version");
        }
    }

    public class BinderWriter : ExtendedBinaryWriter
    {
        public BinderWriter(Stream output, bool isBigEndian = false) : base(output, isBigEndian) { }

        public BinderWriter(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, isBigEndian) { }




    }
}
