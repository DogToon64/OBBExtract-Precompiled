using System;
using System.Collections.Generic;
using System.Text;
using DimpsSonicLib.Formats.SegaNN;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Formats.SegaNN.NNTypes
{
    public class NNZ_TEXTURELIST : NN_TEXTURELIST
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            chunkID = reader.ReadSignature();
            chunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(chunkSize);
        }
    }

    public class NNZ_NODENAMELIST : NN_NODENAMELIST
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            chunkID = reader.ReadSignature();
            chunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(chunkSize);
        }
    }

    public class NNZ_OBJECT : NN_OBJECT
    {
        public uint modelInfoPtr;
        public uint unknown;

        public uint vertBufferLen;
        public uint vertBufferPtr;


        public override void Read(ExtendedBinaryReader reader)
        {
            chunkID = reader.ReadSignature();
            chunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            modelInfoPtr = reader.ReadUInt32();
            unknown = reader.ReadUInt32();
            vertBufferLen = reader.ReadUInt32();
            vertBufferPtr = reader.ReadUInt32();

            reader.JumpTo(jump + chunkSize);
        }
    }

    public class NNZ_MOTION : NN_MOTION
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            chunkID = reader.ReadSignature();
            chunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(chunkSize);
        }
    }

    public class NNZ_EFFECT : NN_EFFECT
    {
        public override void Read(ExtendedBinaryReader reader)
        {
            chunkID = reader.ReadSignature();
            chunkSize = reader.ReadUInt32();
            long jump = reader.BaseStream.Position;

            reader.JumpAhead(chunkSize);
        }
    }
}
