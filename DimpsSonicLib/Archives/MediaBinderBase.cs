using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Archives
{
    public enum BinderVersion
    {
        //Common, Type 1, Type 2
        Rev0, Rev1, Rev2, Unknown
    }

    public class BINDER_HEADER
    {
        public const string signature = "#AMB";
        public uint version { get; set; } = 0;
        public ushort unkVal1 { get; set; } = 0;
        public ushort unkVal2 { get; set; } = 0;
        public bool endianness { get; set; } = false;
        public byte unkVal3 { get; set; } = 0;
        public byte unkVal4 { get; set; } = 0;
        public byte compressionType { get; set; } = 0;

        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            // Read endian flag early then jump back
            reader.JumpTo(12);
            endianness = reader.ReadByte() == 1 ? true : false;
            reader.JumpTo(4);

            // Set reader mode
            reader.IsBigEndian = endianness;

            version = reader.ReadUInt32();
            unkVal1 = reader.ReadUInt16();
            unkVal1 = reader.ReadUInt16();

            reader.JumpAhead(1); // skip the endian flag that we already checked

            unkVal3 = reader.ReadByte();
            unkVal4 = reader.ReadByte();
            compressionType = reader.ReadByte();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            writer.WriteSignature(signature);
            writer.Write(version);
            writer.Write(unkVal1);
            writer.Write(unkVal2);
            writer.Write(endianness ? (byte)1 : (byte)0);
            writer.Write(unkVal3);
            writer.Write(unkVal4);
            writer.Write(compressionType);
        }
    }

    public class BINDER_SUBHEADER
    {
        public dynamic fileCount { get; set; } = 0;
        public dynamic listPointer { get; set; } = 0;
        public dynamic dataPointer { get; set; } = 0;
        public dynamic nameTable { get; set; } = 0;

        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            fileCount = reader.ReadUInt32();
            listPointer = reader.ReadUInt32();
            dataPointer = reader.ReadUInt32();
            nameTable = reader.ReadUInt32();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            writer.Write((uint)fileCount);
            writer.Write((uint)listPointer);
            writer.Write((uint)dataPointer);
            writer.Write((uint)nameTable);
        }
    }

    public class BINDER_FILE
    {
        public string name { get; set; } = "";
        public dynamic filePointer { get; set; } = 0;
        public dynamic fileSize { get; set; } = 0;
        public dynamic unknown1 { get; set; } = 0;
        public dynamic USR0 { get; set; } = 0;
        public dynamic USR1 { get; set; } = 0;
    }


    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    public class BINDERv1_SUBHEADER : BINDER_SUBHEADER
    {
        // uint fileCount
        // uint listPointer
        public uint unknown1 { get; set; } = 0;
        // uint dataPointer
        // uint nameTable
        public uint unknown2 { get; set; } = 0;


        public override void Read(IO.ExtendedBinaryReader reader)
        {
            fileCount = reader.ReadUInt32();
            listPointer = reader.ReadUInt32();
            unknown1 = reader.ReadUInt32();
            dataPointer = reader.ReadUInt32();
            nameTable = reader.ReadUInt32();
            unknown2 = reader.ReadUInt32();
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            writer.Write((uint)fileCount);
            writer.Write((uint)listPointer);
            writer.Write((uint)unknown1);
            writer.Write((uint)dataPointer);
            writer.Write((uint)nameTable);
            writer.Write((uint)unknown2);
        }
    }

    public class BINDERv2_SUBHEADER : BINDER_SUBHEADER
    {
        public override void Read(IO.ExtendedBinaryReader reader)
        {
            fileCount = reader.ReadUInt64();
            listPointer = reader.ReadUInt64();
            dataPointer = reader.ReadUInt64();
            nameTable = reader.ReadUInt64();
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            writer.Write((ulong)fileCount);
            writer.Write((ulong)listPointer);
            writer.Write((ulong)dataPointer);
            writer.Write((ulong)nameTable);
        }
    }

    public class BINDERv1_FILE : BINDER_FILE
    {
        // uint filePointer
        // uint unknown1
        // uint fileSize
        public uint unknown2 { get; set; } = 0;
        // ushort USR0
        // ushort USR1
    }

    public class BINDERv2_FILE : BINDER_FILE
    {
        // uint filePointer
        // uint unknown1
        public uint unknown2 { get; set; } = 0;
        // uint fileSize
        public uint unknown3 { get; set; } = 0;
        // ushort USR0
        // ushort USR1
    }

    public class BINDER_FILEA : BINDER_FILE
    {
        //public string name { get; set; } = "";
        //public uint filePointer { get; set; } = 0;
        //public uint unknown1 { get; set; } = 0;
        public uint unknown2 { get; set; } = 0;
        public uint unknown3 { get; set; } = 0;
        //public uint fileSize { get; set; } = 0;
        //public ushort USR0 { get; set; } = 0;
        //public ushort USR1 { get; set; } = 0;
    }
}
