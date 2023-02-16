using System;
using System.Collections.Generic;
using System.Text;
using DimpsSonicLib.Formats.SegaNN;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Formats.SegaNN.NNTypes
{
    //  NZTL; Texture list  //
    public class NNZ_TEXTURELIST : NN_TEXTURELIST
    {
        public uint texCountPtr;
        public uint unknown1;
        public uint texCount;
        public uint unknown2;
        public List<NNZ_TEXTURE> TexList = new List<NNZ_TEXTURE>();

        public override void Read(ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;
            
            texCountPtr = reader.ReadUInt32();

            reader.JumpTo(texCountPtr + 28);
            unknown1 = reader.ReadUInt32();
            texCount = reader.ReadUInt32();
            unknown2 = reader.ReadUInt32();

            reader.JumpTo(jump + 4);

            for (int i = 0; i < texCount; i++)
            {
                TexList.Add(new NNZ_TEXTURE()
                {
                    Unknown1 = reader.ReadUInt32(),
                    Unknown2 = reader.ReadUInt32(),
                    NameOfst = reader.ReadUInt32(),
                    Filter1 = reader.ReadInt16(),
                    Filter2 = reader.ReadInt16(),
                    Unknown3 = reader.ReadUInt32(),
                });

                // Why the fuck didn't they store names on an index like they did for Nodes???
                // I really don't want to assume they wrote the names in the same order
                long jump2 = reader.BaseStream.Position;
                reader.JumpTo(32 + TexList[i].NameOfst);
                TexList[i].TexName = reader.ReadNullTerminatedString();
                reader.JumpTo(jump2);
            }

            //reader.JumpAhead(ChunkSize);
            reader.JumpTo(jump + ChunkSize);
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NZNN; Node (bone) name list  //
    public class NNZ_NODENAMELIST : NN_NODENAMELIST
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(ChunkSize);
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NZOB; 3D model data  //
    public class NNZ_OBJECT : NN_OBJECT
    {
        public uint modelInfoPtr;
        public uint unknown;

        public uint vertBufferLen;
        public uint vertBufferPtr;


        public override void Read(ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            modelInfoPtr = reader.ReadUInt32();
            unknown = reader.ReadUInt32();
            vertBufferLen = reader.ReadUInt32();
            vertBufferPtr = reader.ReadUInt32();

            reader.JumpTo(jump + ChunkSize);
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NZMO; Animation  //
    public class NNZ_MOTION : NN_MOTION
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(ChunkSize);
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NZEF; Effect data  //
    public class NNZ_EFFECT : NN_EFFECT
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(ChunkSize);
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    //  NZMA; Material animation  //
    public class NNZ_VERTEXANIMATION : NN_VERTEXANIMATION
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            ChunkID = reader.ReadSignature();
            ChunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(ChunkSize);
        }

        public override void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }


    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    public class NNZ_TEXTURE 
    {
        public uint Unknown1 { get; set; }
        public uint Unknown2 { get; set; }
        public uint NameOfst { get; set; }
        public short Filter1 { get; set; } //(NNE_MIN Type?)
        public short Filter2 { get; set; } //(NNE_MAG Type?)
        public uint Unknown3 { get; set; }
        public string TexName { get; set; }
    }
}
