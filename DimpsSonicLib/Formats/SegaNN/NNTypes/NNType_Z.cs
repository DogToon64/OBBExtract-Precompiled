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

    public class NNZ_FILE
    {
        public static void ReadNNZ(NNFile nnf)
        {
            for (int i = 0; i < nnf.Info.chunkCount; i++)
            {
                string identifier = nnf.ReadSignature();
                nnf.JumpBehind(4);

                switch (identifier.Remove(0, 2))
                {
                    case "OB":
                        Log.PrintInfo("Reading mesh data");
                        nnf.Mesh = new NNTypes.NNZ_OBJECT();
                        nnf.Mesh.Read(nnf);
                        break;

                    case "NN":
                        Log.PrintInfo("Reading bone names");
                        nnf.NodeNames = new NNTypes.NNZ_NODENAMELIST();
                        nnf.NodeNames.Read(nnf);
                        break;

                    case "TL":
                        Log.PrintInfo("Reading texture list");
                        nnf.Textures = new NNTypes.NNZ_TEXTURELIST();
                        nnf.Textures.Read(nnf);
                        break;

                    case "EF":
                        Log.PrintInfo("Reading effect");
                        nnf.Effect = new NNTypes.NNZ_EFFECT();
                        nnf.Effect.Read(nnf);
                        break;

                    case "MO":
                        Log.PrintInfo("Reading animation");
                        nnf.Motion = new NNTypes.NNZ_MOTION();
                        nnf.Motion.Read(nnf);
                        break;

                    case "MA":
                        Log.PrintInfo("Reading material anim");
                        throw new NotImplementedException();
                    //break;

                    default:
                        Log.PrintWarning("Default case hit");
                        break;
                }
            }
        }
    }
}
