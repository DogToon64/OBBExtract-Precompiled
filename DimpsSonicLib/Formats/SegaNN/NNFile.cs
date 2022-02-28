using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.IO;

//  HUGE WORK IN PROGRESS, DO NOT USE
namespace DimpsSonicLib.Formats.SegaNN
{
    public class NNFile : ExtendedBinaryReader
    {
        // Constructors
        public NNFile(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { }
        public NNFile(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { }


        // Variables
        public NN_INFO Info { get; set; }
        public NN_OFFSETLIST OffsetList { get; set; }
        public NN_FILENAME Footer { get; set; }

        public NN_OBJECT Mesh { get; set; }
        public NN_TEXTURELIST Textures { get; set; }
        public NN_NODENAMELIST NodeNames { get; set; }
        public NN_EFFECT Effect { get; set; }
        public NN_MOTION Motion { get; set; }


        // Methods
        public static NNTYPE GetNNTYPE(string input)
        {
            if (input[1] == 'X')
                return NNTYPE.NNX;
            else if (input[1] == 'Z')
                return NNTYPE.NNZ;
            else
                return NNTYPE.Unknown;
        }

        public void ReadNNFile()
        {
            // Base chunks to be populated from reading
            Info        = new NN_INFO();
            OffsetList  = new NN_OFFSETLIST();
            Footer      = new NN_FILENAME();
            Mesh        = new NN_OBJECT();
            Textures    = new NN_TEXTURELIST();
            NodeNames   = new NN_NODENAMELIST();
            Effect      = new NN_EFFECT();
            Motion      = new NN_MOTION();

            Log.PrintInfo("Reading file info");
            Info.Read(this);

            // Determine the format we're reading from the info chunk's signature
            NNTYPE type = GetNNTYPE(Info.chunkID);

            if (type == NNTYPE.Unknown)
            {
                Log.PrintWarning("NNTYPE is unknown, this file format may not be supported yet.");
                goto SkipNNRead;
            }

            JumpTo(Info.dataPointer);

            // Maybe move to the respective type's class file, replace this with a switch case for detecting the type?
            //for (int i = 0; i < Info.chunkCount; i++)
            //{
            //    string identifier = ReadSignature();
            //    JumpBehind(4);

            //    switch (identifier.Remove(0, 2))
            //    {
            //        case "OB":
            //            Log.PrintInfo("Reading mesh data");
            //            Mesh = new NNTypes.NNZ_OBJECT();
            //            Mesh.Read(this);
            //            break;

            //        case "NN":
            //            Log.PrintInfo("Reading bone names");
            //            NodeNames = new NNTypes.NNZ_NODENAMELIST();
            //            NodeNames.Read(this);
            //            break;

            //        case "TL":
            //            Log.PrintInfo("Reading texture list");
            //            Textures = new NNTypes.NNZ_TEXTURELIST();
            //            Textures.Read(this);
            //            break;

            //        case "EF":
            //            Log.PrintInfo("Reading effect");
            //            Effect = new NNTypes.NNZ_EFFECT();
            //            Effect.Read(this);
            //            break;

            //        case "MO":
            //            Log.PrintInfo("Reading animation");
            //            Motion = new NNTypes.NNZ_MOTION();
            //            Motion.Read(this);
            //            break;

            //        case "MA":
            //            Log.PrintInfo("Reading material anim");
            //            throw new NotImplementedException();
            //            //break;

            //        default:
            //            Log.PrintWarning("Default case hit");
            //            break;
            //    }
            //}

            switch (type)
            {
                case NNTYPE.NNZ:
                    NNTypes.NNZ_FILE.ReadNNZ(this);
                    break;

                case NNTYPE.NNX:
                    throw new NotImplementedException();
                    //break;
            }

            JumpTo(Info.offsetListPointer);




        SkipNNRead:;
        }     

    }


}