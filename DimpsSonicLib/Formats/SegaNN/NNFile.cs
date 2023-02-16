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
        public NNFile(Stream input, bool isBigEndian = false) : base(input, isBigEndian) { Initialize(); }
        public NNFile(Stream input, Encoding encoding, bool isBigEndian = false) : base(input, encoding, isBigEndian) { Initialize(); }


        // Variables
        public NN_INFO Info              { get; set; }
        public NN_OFFSETLIST OffsetList  { get; set; }
        public NN_FILENAME Footer        { get; set; }

        public NN_TEXTURELIST Textures   { get; set; }
        public NN_NODENAMELIST NodeNames { get; set; }
        public NN_OBJECT Mesh            { get; set; }

        public NN_MOTION Motion { get; set; }
        public NN_EFFECT Effect { get; set; }
        public NN_VERTEXANIMATION VertexAnim { get; set; }


        // Methods
        public static NNTYPE GetNNTYPE(string input)
        {
            if (input[1] == 'Z')
                return NNTYPE.NNZ;
            else if (input[1] == 'X')
                return NNTYPE.NNX;
            else
                return NNTYPE.Unknown;
        }

        /// <summary>
        /// Call this before creating an object to be written to
        /// </summary>
        public void Initialize()
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
            VertexAnim  = new NN_VERTEXANIMATION();
        }

        /* I tried something lmfao. If using, delete object assignment from ReadNNFile and unknown check.
        public void Initialize_Alt()
        {
            // Base chunks to be populated from reading
            Info = new NN_INFO();
            OffsetList = new NN_OFFSETLIST();
            Footer = new NN_FILENAME();

            Log.PrintInfo("Reading file info");
            Info.Read(this);
            
            NNTYPE type = GetNNTYPE(Info.chunkID);
            if (type == NNTYPE.Unknown)
            {
                Log.PrintWarning("NNTYPE is unknown, this file format may not be supported yet.");
                goto SkipNNRead2;
            }
            
            // Init chunks as types detected
            Mesh = NinjaObject(type);
            Textures = NinjaTextureList(type);
            NodeNames = NinjaNodeNames(type);
            Effect = NinjaEffect(type);
            Motion = NinjaMotion(type);
            VertexAnim = NinjaVertexAnimation(type);

        SkipNNRead2:;
        }
        */

        public void ReadNNFile()
        {
            Initialize();

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

            // Read all chunks in the file
            for (int i = 0; i < Info.chunkCount; i++)
            {
                string identifier = ReadSignature();
                JumpBehind(4);

                switch (identifier.Remove(0, 2))
                {
                    case "OB":
                        Log.PrintInfo("Reading mesh data");
                        Mesh = NinjaObject(type);
                        Mesh.Read(this);
                        break;

                    case "TL":
                        Log.PrintInfo("Reading texture list");
                        Textures = NinjaTextureList(type);
                        Textures.Read(this);
                        break;

                    case "NN":
                        Log.PrintInfo("Reading bone names");
                        NodeNames = NinjaNodeNames(type);
                        NodeNames.Read(this);
                        break;

                    case "EF":
                        Log.PrintInfo("Reading effect");
                        Effect = NinjaEffect(type);
                        Effect.Read(this);
                        break;

                    case "MO":
                        Log.PrintInfo("Reading animation");
                        Motion = NinjaMotion(type);
                        Motion.Read(this);
                        break;

                    case "MA":
                        Log.PrintInfo("Reading vertex animation");
                        VertexAnim = NinjaVertexAnimation(type);
                        VertexAnim.Read(this);
                        break;

                    default:
                        Log.PrintWarning("Default case hit");
                        break;
                }
            }

            JumpTo(Info.offsetListPointer);

            Log.PrintInfo("Reading offset list");
            OffsetList.Read(this);

            Log.PrintInfo("Reading footer");
            Footer.Read(this);


        SkipNNRead:;
        }


        // Type handling
        public dynamic NinjaObject(NNTYPE t)
        {
            switch (t)
            {
                case NNTYPE.NNZ:
                    return new NNTypes.NNZ_OBJECT();

                case NNTYPE.NNX:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }    
        }

        public dynamic NinjaTextureList(NNTYPE t)
        {
            switch (t)
            {
                case NNTYPE.NNZ:
                    return new NNTypes.NNZ_TEXTURELIST();

                case NNTYPE.NNX:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }
        }

        public dynamic NinjaNodeNames(NNTYPE t)
        {
            switch (t)
            {
                case NNTYPE.NNZ:
                    return new NNTypes.NNZ_NODENAMELIST();

                case NNTYPE.NNX:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }
        }

        public dynamic NinjaEffect(NNTYPE t)
        {
            switch (t)
            {
                case NNTYPE.NNZ:
                    return new NNTypes.NNZ_EFFECT();

                case NNTYPE.NNX:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }
        }

        public dynamic NinjaMotion(NNTYPE t)
        {
            switch (t)
            {
                case NNTYPE.NNZ:
                    return new NNTypes.NNZ_MOTION();

                case NNTYPE.NNX:
                    throw new NotImplementedException();

                default:
                    throw new NotImplementedException();
            }
        }

        public dynamic NinjaVertexAnimation(NNTYPE t)
        {
            switch (t)
            {
                case NNTYPE.NNZ:
                    return new NNTypes.NNZ_VERTEXANIMATION();

                case NNTYPE.NNX:
                    throw new NotImplementedException();
                //break;

                default:
                    throw new NotImplementedException();
            }
        }
    }

}