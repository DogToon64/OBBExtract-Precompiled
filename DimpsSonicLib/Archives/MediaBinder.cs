using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Archives
{
    public class BinderReader : ExtendedBinaryReader
    {
        // Constructors
        public BinderReader(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { Initialize(); }
        public BinderReader(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { Initialize(); }

        // Variables
        public BINDER_HEADER Header { get; set; }
        public BINDER_SUBHEADER SubHeader { get; set; }
        public List<BINDER_FILE> Index { get; set; }

        // Methods

        public static Version GetBinderVersion(uint input)
        {
            switch (input)
            {
                case 32:
                    return Version.Rev0;
                case 40:
                    return Version.Rev1;
                case 48: 
                    return Version.Rev2;
                default:
                    return Version.Unknown;
            }
        }


        public void Initialize()
        {
            Header    = new BINDER_HEADER();
            SubHeader = new BINDER_SUBHEADER();
            Index     = new List<BINDER_FILE>();
        }

        public void ReadBinder()
        {
            int  curr = 0;
            long last;
            string n = "";

            Initialize();

            Header.Read(this);

            Version ver = GetBinderVersion(Header.version);

            if (ver == Version.Unknown)
            {
                Log.PrintWarning("Binder seems to be an unknown version");
                goto SkipBinderRead;
            }

            if (Header.compressionType != 0)
            {
                Log.PrintWarning("Compressed binders are currently not supported");
                goto SkipBinderRead;
            }

            SubHeader = BinderSubHeader(ver);
            SubHeader.Read(this);

            Index = BinderFileIndex(ver);

            for (int i = 0; i < SubHeader.fileCount; i++)
            {
                if (SubHeader.nameTable != 0)
                {
                    last = stream.Position;
                    JumpTo(SubHeader.nameTable + (32 * curr));
                    n = ReadLengthPrefixedString(32);
                    JumpTo(last);
                    curr++;
                }
         
                switch (ver)
                {
                    case Version.Rev0:
                        Index.Add(new BINDER_FILE()
                        {
                            name = n,
                            filePointer = ReadUInt32(),
                            unknown1 = ReadUInt32(),
                            fileSize = ReadUInt32(),
                            USR0 = ReadUInt16(),
                            USR1 = ReadUInt16(),
                        });
                        break;

                    case Version.Rev1:
                        Index.Add(new BINDERv1_FILE()
                        {
                            name = n,
                            filePointer = ReadUInt32(),
                            unknown1 = ReadUInt32(),
                            fileSize = ReadUInt32(),
                            unknown2 = ReadUInt32(),
                            USR0 = ReadUInt16(),
                            USR1 = ReadUInt16(),
                        });
                        break;

                    case Version.Rev2:
                        Index.Add(new BINDERv2_FILE()
                        {
                            name = n,
                            filePointer = ReadUInt32(),
                            unknown1 = ReadUInt32(),
                            unknown2 = ReadUInt32(),
                            fileSize = ReadUInt32(),
                            unknown3 = ReadUInt32(),
                            USR0 = ReadUInt16(),
                            USR1 = ReadUInt16(),
                        });
                        break;

                    case Version.Unknown:
                        throw new NotImplementedException();
                    default:
                        throw new Exception();
                }
            }       

        SkipBinderRead:;
        }

        public dynamic BinderSubHeader(Version v)
        {
            switch (v)
            {
                case Version.Rev0:
                    return new BINDER_SUBHEADER();
                case Version.Rev1:
                    return new BINDERv1_SUBHEADER();
                case Version.Rev2:
                    return new BINDERv2_SUBHEADER();
                case Version.Unknown:
                    throw new NotImplementedException();
                default:
                    throw new Exception();
            }
        }

        public dynamic BinderFileIndex(Version v)
        {
            switch (v)
            {
                case Version.Rev0:
                    return new List<BINDER_FILE>();
                case Version.Rev1:
                    return new List<BINDERv1_FILE>();
                case Version.Rev2:
                    return new List<BINDERv2_FILE>();
                case Version.Unknown:
                    throw new NotImplementedException();
                default:
                    throw new Exception();
            }
        }




        public void WriteAMB()
        {

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
    }

    public class AMB_O
    {
        public AMBHeader Header { get; set; }
        public IEnumerable<SubHeader> SubHeader { get; set; }
        public IEnumerable<FileIndex> FileIndex { get; set; }
        public Binder_O.Version_O version { get; set; }
    }

    public class Binder_O
    {
        public enum Version_O
        {
            //Common, Type 1, Type 2
            Rev0, Rev1, Rev2, Unknown
        }

        public static Version_O GetAMBVersion(AMBHeader header)
        {
            if (header.version == 32)
                return Version_O.Rev0;
            else if (header.version == 40)
                return Version_O.Rev1;
            else if (header.version == 48)
                return Version_O.Rev2;
            else
                return Version_O.Unknown;
        }

        // Loading and Parsing.    TODO: un-fuck this logic
        public static AMB_O ReadAMB(Stream stream)
        {
            var reader = new BinderReader_O(stream);
            var header = reader.ReadHeader();
            reader.JumpTo(16);
            var ver = GetAMBVersion(header);

            if (ver != Version_O.Unknown)
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

                return new AMB_O { Header = header, SubHeader = subHeader, FileIndex = fileIndex };
            }
            else throw new NotImplementedException("Unknown AMB Version");
        }

        // Extract to a given directory. DOES NOT BELONG IN LIBRARY
        public static void ExtractAMB(AMB_O amb, string dir)
        {



        }

        // Hmm                           DOES NOT BELONG IN LIBRARY
        public static void WriteAMB(AMB_O amb, Version_O ver = Version_O.Rev0, bool isBigEndian = false)
        {

           // write from loaded or new AMB

        }
    }


    public class BinderReader_O : ExtendedBinaryReader
    {
        public BinderReader_O(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public BinderReader_O(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }

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

        public IEnumerable<SubHeader> ReadSubHeader(Binder_O.Version_O ver)
        {
            if (ver == Binder_O.Version_O.Rev0)
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
            else if (ver == Binder_O.Version_O.Rev1)
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
            else if (ver == Binder_O.Version_O.Rev2)
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

        public IEnumerable<FileIndex> ReadFileIndex(SubHeader sub, Binder_O.Version_O ver)
        {
            if (ver == Binder_O.Version_O.Rev0)
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

            else if (ver == Binder_O.Version_O.Rev1)
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

            else if (ver == Binder_O.Version_O.Rev2)
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

    public class BinderWriter_O : ExtendedBinaryWriter
    {
        public BinderWriter_O(Stream output, bool isBigEndian = false) : base(output, isBigEndian) { }

        public BinderWriter_O(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, isBigEndian) { }




    }
}
