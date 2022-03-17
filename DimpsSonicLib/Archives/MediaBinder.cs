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
        }


        public void ReadBinder()
        {
            int    curr = 0;
            long   last;
            string Name = "";

            Initialize();

            Header.Read(this);

            BinderVersion ver = GetBinderVersion(Header.version);


            if (ver == BinderVersion.Unknown)
                throw new Exception("Binder seems to be an unknown version");

            if (Header.compressionType != 0)
                throw new Exception("Compressed binders are not currently supported");


            SubHeader = BinderSubHeader(ver);
            SubHeader.Read(this);
            

            for (uint i = 0; i < SubHeader.fileCount; i++)
            {
                if (SubHeader.nameTable != 0)
                {
                    last = stream.Position;
                    JumpTo((long)SubHeader.nameTable + (32 * curr));
                    Name = ReadNullTerminatedString();
                    JumpTo(last);
                    curr++;
                }
         
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
                            name = Name,
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
                            name = Name,
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
                            name = Name,
                        });
                        break;

                    case BinderVersion.Unknown:
                        throw new NotImplementedException();
                    default:
                        throw new Exception();
                }
            }       
        }

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

        //public dynamic BinderFileIndex(BinderVersion v)
        //{
        //    switch (v)
        //    {
        //        case BinderVersion.Rev0:
        //            return new List<BINDER_FILE>();
        //        case BinderVersion.Rev1:
        //            return new List<BINDERv1_FILE>();
        //        case BinderVersion.Rev2:
        //            return new List<BINDERv2_FILE>();
        //        case BinderVersion.Unknown:
        //            throw new NotImplementedException();
        //        default:
        //            throw new Exception();
        //    }
        //}


        public void WriteAMB()
        {

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
