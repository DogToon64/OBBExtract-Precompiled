using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.SegaNN
{
    public enum NNTYPE
    {
        NNZ, NNX, Unknown
    }

    public interface NN_CHUNKBASE
    {
        string ChunkID { get; set; }
        dynamic ChunkSize { get; set; }
    }

    public interface NN_CHUNKDATA_OFFSET
    {
        uint ChunkDataType { get; set; }
        uint ChunkDataOffset { get; set; }
    }

    //  N*IF; A file's info header, where '*' is replaced with the format's identifer (Ex. NZIF)  //
    // All following chunk's pointers act like this chunk doesn't exist (Ex: JumpTo(pointer + 32);)
    public class NN_INFO : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public uint ChunkCount { get; set; }
        public uint DataPointer { get; set; }
        public uint DataSize { get; set; }
        public uint OffsetListPointer { get; set; }
        public uint OffsetListSize { get; set; }
        public uint Version { get; set; }
        
        public NN_INFO() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            ChunkID             = reader.ReadSignature();

            if (ChunkID == "NCIF" || ChunkID == "NEIF" || ChunkID == "NGIF")
                reader.IsBigEndian = true;

            ChunkSize           = reader.ReadUInt32();
            ChunkCount          = reader.ReadUInt32();
            DataPointer         = reader.ReadUInt32();
            DataSize            = reader.ReadUInt32();
            OffsetListPointer   = reader.ReadUInt32();
            OffsetListSize      = reader.ReadUInt32();
            Version             = reader.ReadUInt32();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NFN0; The name of the current file   //
    public class NN_FILENAME : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }
        // [8 bytes of padding]
        public string FileName { get; set; }

        public NN_FILENAME() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(8);

            FileName = reader.ReadNullTerminatedString();

            reader.JumpTo(jump + ChunkSize);
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NOF0; A table of pointers to other offsets in the file  //
    // Unsure what purpose this serves, but it is REQUIRED by the games
    public class NN_OFFSETLIST : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }
        public uint PointerCount { get; set; }
        public uint[] Pointers { get; set; } // ?

        public NN_OFFSETLIST() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;
            PointerCount = reader.ReadUInt32();

            reader.JumpAhead(4);

            Pointers = new uint[PointerCount];

            for (int i = 0; i < PointerCount; i++)
                Pointers[i] = reader.ReadUInt32();

            reader.JumpTo(jump + ChunkSize);
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    // NEND; Literally just the end chunk, typically has padding
    public class NN_END : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_END() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }


    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    //  N*TL; Texture list  //
    public class NN_TEXTURELIST : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_TEXTURELIST() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  N*NN; Node (bone) name list  //
    public class NN_NODENAMELIST : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_NODENAMELIST() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  N*OB; 3D model data  //
    public class NN_OBJECT : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_OBJECT() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  N*EF; Effect data  //
    public class NN_EFFECT : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_EFFECT() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  N*MO; Animation  //
    public class NN_MOTION : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_MOTION() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  N*MA; Material animation  //
    public class NN_VERTEXANIMATION : NN_CHUNKBASE
    {
        public string ChunkID { get; set; }
        public dynamic ChunkSize { get; set; }

        public NN_VERTEXANIMATION() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }


    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    //  NN Texture (Do I need this here? )  //
    public class NN_TEXTURE
    {
        // *cough* 🦗
    }
}
