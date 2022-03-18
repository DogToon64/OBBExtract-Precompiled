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
        public List<BINDER_FILEA> Index { get; set; }
        public List<byte[]> FileData { get; set; }
        public List<string> FileSys { get; set; }

        // Methods
        public static BinderVersion GetBinderVersion(uint input)
        {
            switch (input)
            {
                case 32:
                    return BinderVersion.Rev0;
                case 40:
                    return BinderVersion.Rev1;
                case 48: 
                    return BinderVersion.Rev2;
                default:
                    return BinderVersion.Unknown;
            }
        }
        

        public void Initialize()
        {
            Header    = new BINDER_HEADER();
            SubHeader = new BINDER_SUBHEADER();
            Index     = new List<BINDER_FILEA>();
            FileData  = new List<byte[]>();
            FileSys   = new List<string>();
        }


        public void ReadBinder()
        {
            // Init variables
            Initialize();

            // Read the Header
            Header.Read(this);

            // Version Check
            BinderVersion ver = GetBinderVersion(Header.version);
            if (ver == BinderVersion.Unknown)
                throw new Exception("Binder seems to be an unknown version");

            // Read a compressed chunk separately if detected
            if (Header.compressionType != 0)
            {
                MemoryStream result = new MemoryStream();

                JumpAhead(4);
                byte[] data = ReadBytes((int)(BaseStream.Length - 20));
                byte[] deflated = Compression.DecompressZlibChunk(data);

                result.Write(deflated, 0, deflated.Length);

                ExtendedBinaryReader read = new ExtendedBinaryReader(result);

                // Same as below, but isolated
                ReadCompressedContents(read, ver);

                return;
            }


            // Read Sub Header
            SubHeader = BinderSubHeader(ver);
            SubHeader.Read(this);

            // Read BinderFS data if possible
            if (SubHeader.nameTable != 0)
            {
                for (uint j = 0; j < SubHeader.fileCount; j++)
                {
                    JumpTo((long)SubHeader.nameTable + (32 * j));
                    FileSys.Add(ReadNullTerminatedString());   
                }
            }

            JumpTo((int)SubHeader.listPointer);

            // Read the File Index
            for (uint i = 0; i < SubHeader.fileCount; i++)
            {
                switch (ver)
                {
                    case BinderVersion.Rev0:
                        Index.Add(new BINDER_FILEA()
                        {
                            filePointer = ReadUInt32(),                           
                            fileSize = ReadUInt32(),
                            unknown1 = ReadUInt32(),
                            USR0 = ReadUInt16(),
                            USR1 = ReadUInt16(),
                        });
                        break;

                    case BinderVersion.Rev1:
                        Index.Add(new BINDER_FILEA()
                        {
                            filePointer = ReadUInt32(),
                            unknown2 = ReadUInt32(),
                            fileSize = ReadUInt32(),
                            unknown1 = ReadUInt32(),
                            USR0 = ReadUInt16(),
                            USR1 = ReadUInt16(),
                        });
                        break;

                    case BinderVersion.Rev2:
                        Index.Add(new BINDER_FILEA()
                        {
                            filePointer = ReadUInt32(),
                            unknown2 = ReadUInt32(),
                            unknown3 = ReadUInt32(),
                            fileSize = ReadUInt32(),
                            unknown1 = ReadUInt32(),
                            USR0 = ReadUInt16(),
                            USR1 = ReadUInt16(),
                        });
                        break;

                    case BinderVersion.Unknown:
                        throw new NotImplementedException();
                    default:
                        throw new Exception();
                }
            }

            for (int i = 0; i < (int)SubHeader.fileCount; i++)
            {
                JumpTo(Index[i].filePointer);
                FileData.Add(ReadBytes((int)Index[i].fileSize));
            }
        }



        // Same as above, but isolated to read decompressed contents
        public void ReadCompressedContents(ExtendedBinaryReader r, BinderVersion ver)
        {
            SubHeader = BinderSubHeader(ver);
            r.JumpTo(0);
            SubHeader.Read(r);

            if (SubHeader.nameTable != 0)
            {
                for (uint j = 0; j < SubHeader.fileCount; j++)
                {
                    r.JumpTo((long)(SubHeader.nameTable - 16) + (32 * j));
                    FileSys.Add(r.ReadNullTerminatedString());                    
                }
            }

            r.JumpTo(SubHeader.listPointer - 16);

            for (uint i = 0; i < SubHeader.fileCount; i++)
            {
                switch (ver)
                {
                    case BinderVersion.Rev0:
                        Index.Add(new BINDER_FILEA()
                        {
                            filePointer = r.ReadUInt32(),
                            fileSize = r.ReadUInt32(),
                            unknown1 = r.ReadUInt32(),
                            USR0 = r.ReadUInt16(),
                            USR1 = r.ReadUInt16(),
                        });
                        break;

                    case BinderVersion.Rev1:
                        Index.Add(new BINDER_FILEA()
                        {
                            filePointer = r.ReadUInt32(),
                            unknown2 = r.ReadUInt32(),
                            fileSize = r.ReadUInt32(),
                            unknown1 = r.ReadUInt32(),
                            USR0 = r.ReadUInt16(),
                            USR1 = r.ReadUInt16(),
                        });
                        break;

                    case BinderVersion.Rev2:
                        Index.Add(new BINDER_FILEA()
                        {
                            filePointer = r.ReadUInt32(),
                            unknown2 = r.ReadUInt32(),
                            unknown3 = r.ReadUInt32(),
                            fileSize = r.ReadUInt32(),
                            unknown1 = r.ReadUInt32(),
                            USR0 = r.ReadUInt16(),
                            USR1 = r.ReadUInt16(),
                        });
                        break;

                    case BinderVersion.Unknown:
                        throw new NotImplementedException();
                    default:
                        throw new Exception();
                }
            }

            for (int i = 0; i < (int)SubHeader.fileCount; i++)
            {
                Console.WriteLine("CMP File Pointer:  " + (Index[i].filePointer));

                if (Index[i].filePointer != 0)
                    r.JumpTo((Index[i].filePointer - 16));
                else
                    r.JumpTo(Index[i].filePointer);

                FileData.Add(r.ReadBytes((int)Index[i].fileSize));
            }
        }

        // Returns the proper Sub Header type depending on the version
        public dynamic BinderSubHeader(BinderVersion v)
        {
            switch (v)
            {
                case BinderVersion.Rev0:
                    return new BINDER_SUBHEADER();
                case BinderVersion.Rev1:
                    return new BINDERv1_SUBHEADER();
                case BinderVersion.Rev2:
                    return new BINDERv2_SUBHEADER();
                case BinderVersion.Unknown:
                    throw new NotImplementedException();
                default:
                    throw new Exception();
            }
        }


    }



    public class BinderWriter : ExtendedBinaryWriter
    {
        public BinderWriter(Stream output, bool isBigEndian = false) : base(output, isBigEndian) { }
        public BinderWriter(Stream output, Encoding encoding, bool isBigEndian = false) : base(output, encoding, isBigEndian) { }


        // Variables
        public BINDER_HEADER Header { get; set; }
        public BINDER_SUBHEADER SubHeader { get; set; }
        public List<BINDER_FILEA> Index { get; set; }

        // Methods
        public void Initialize()
        {
            Header = new BINDER_HEADER();
            SubHeader = new BINDER_SUBHEADER();
            Index = new List<BINDER_FILEA>();
        }

        public void WriteBinder(BinderVersion ver)
        {

        }



    }
}
