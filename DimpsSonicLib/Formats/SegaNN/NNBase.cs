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
        string chunkID { get; set; }
        dynamic chunkSize { get; set; }
    }

    public interface NN_CHUNKDATA_OFFSET
    {
        uint chunkDataType { get; set; }
        uint chunkDataOffset { get; set; }
    }

    // N*IF; A file's info header, where '*' is replaced with the format's identifer (Ex. NZIF)
    // All following chunk's pointers act like this chunk doesn't exist (Ex: JumpTo(pointer + 32);)
    public class NN_INFO : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public uint chunkCount { get; set; }
        public uint dataPointer { get; set; }
        public uint dataSize { get; set; }
        public uint offsetListPointer { get; set; }
        public uint offsetListSize { get; set; }
        public uint version { get; set; }
        
        public NN_INFO() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            chunkID             = reader.ReadSignature();
            chunkSize           = reader.ReadUInt32();
            chunkCount          = reader.ReadUInt32();
            dataPointer         = reader.ReadUInt32();
            dataSize            = reader.ReadUInt32();
            offsetListPointer   = reader.ReadUInt32();
            offsetListSize      = reader.ReadUInt32();
            version             = reader.ReadUInt32();
        }
    }

    // NFN0; The name of the current file
    public class NN_FILENAME : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }
        // [8 bytes of padding]
        public string fileName { get; set; }

        public NN_FILENAME() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }

    // NOF0; A table of pointers to other offsets in the file
    // Unsure what purpose this serves, but it is REQUIRED by the games
    public class NN_OFFSETLIST : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }
        public uint pointerCount { get; set; }

        public NN_OFFSETLIST() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            chunkID = reader.ReadSignature();
            chunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;
            pointerCount = reader.ReadUInt32();

            reader.JumpAhead(4);

            uint[] pointers = new uint[pointerCount];

            for (int i = 0; i < pointerCount; i++)
                pointers[i] = reader.ReadUInt32();

            reader.JumpTo(jump + chunkSize);
        }
    }

    // NEND; Literally just the end chunk, typically has padding
    public class NN_END : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public NN_END() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }


    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    // N*TL; Texture list 
    public class NN_TEXTURELIST : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public NN_TEXTURELIST() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }

    // N*NN; Node (bone) name list
    public class NN_NODENAMELIST : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public NN_NODENAMELIST() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }

    // N*OB; 3D model data
    public class NN_OBJECT : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public NN_OBJECT() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }

    // N*EF; Effect data
    public class NN_EFFECT : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public NN_EFFECT() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }

    // N*MO; Animation
    public class NN_MOTION : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public NN_MOTION() { }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
