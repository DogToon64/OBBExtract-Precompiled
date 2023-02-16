using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.Formats.SegaNN;
using DimpsSonicLib.Formats.SegaNN.NNTypes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CmdTest
{
    public class ZNOReader
    {
        // ZNO Reading tests
        public static void ReadZNO(string file)
        {
            // Load the file into a stream
            Console.WriteLine($"Opening {Path.GetFileName(file)}");
            Stream stream = File.OpenRead(file);

            // Set up the object, passing in the file's stream
            NNFile nnFile = new NNFile(stream);

            // Parse the given file's stream
            nnFile.ReadNNFile();

            Console.WriteLine("Finished.");

            // Print various data to console to check if parsing correctly
            PrintData(nnFile);

            Console.ReadLine();

            // Typecast to access format-specific data not exposed by NNBase
            //var nnzTextures = (NNZ_TEXTURELIST)nnFile.Textures;
        }



        private static void PrintData(NNFile nnFile)
        {
            //  NN_INFO  //
            Console.WriteLine("\nNinja Info           [{0}]\nChunk Size          : {1}\n" +
                "Chunk Count         : {2}\nData Pointer        : {3}\nData Size           : {4}\n" +
                "Offset List Pointer : {5}\nOffset List Size    : {6}\nVersion             : {7}",
                nnFile.Info.ChunkID, nnFile.Info.ChunkSize, nnFile.Info.ChunkCount, nnFile.Info.DataPointer,
                nnFile.Info.DataSize, nnFile.Info.OffsetListPointer, nnFile.Info.OffsetListSize, nnFile.Info.Version);

            //  NN_TEXTURELIST  //
            if (nnFile.Textures.ChunkID != null)
            {
                Console.WriteLine("\nNinja Texture List   [{0}]\nChunk Size          : {1}",
                    nnFile.Textures.ChunkID, nnFile.Textures.ChunkSize);

                // Typecast to NNZ
                var nnzTextures = (NNZ_TEXTURELIST)nnFile.Textures;

                Console.WriteLine("TexCount Pointer    : {0}\nTexture Count       : {1}\nFirst Texture Name  : {2}",
                    nnzTextures.texCountPtr, nnzTextures.texCount, nnzTextures.TexList[0].TexName);
            }

            //  NN_NODENAMELIST  //
            if (nnFile.NodeNames.ChunkID != null)
            {
                Console.WriteLine("\nNinja Node Names     [{0}]\nChunk Size          : {1}",
                    nnFile.NodeNames.ChunkID, nnFile.NodeNames.ChunkSize);
            }

            //  NN_EFFECT  //
            if (nnFile.Effect.ChunkID != null)
            {
                Console.WriteLine("\nNinja Effect         [{0}]\nChunk Size          : {1}",
                    nnFile.Effect.ChunkID, nnFile.Effect.ChunkSize);
            }

            //  NN_OBJECT  //
            if (nnFile.Mesh.ChunkID != null)
            {
                Console.WriteLine("\nNinja Object         [{0}]\nChunk Size          : {1}",
                    nnFile.Mesh.ChunkID, nnFile.Mesh.ChunkSize);
            }

            //  NN_MOTION  //
            if (nnFile.Motion.ChunkID != null)
            {
                Console.WriteLine("\nNinja Motion         [{0}]\nChunk Size          : {1}",
                    nnFile.Motion.ChunkID, nnFile.Motion.ChunkSize);
            }

            //  NN_VERTEXANIMATION  //
            if (nnFile.VertexAnim.ChunkID != null)
            {
                Console.WriteLine("\nNinja Vertex Motion  [{0}]\nChunk Size          : {1}",
                    nnFile.VertexAnim.ChunkID, nnFile.VertexAnim.ChunkSize);
            }

            //  NN_OFFSETLIST  //
            if (nnFile.OffsetList.ChunkID != null)
            {
                Console.WriteLine("\nNinja Offset List    [{0}]\nChunk Size          : {1}" +
                    "\nPointer Count       : {2}",
                    nnFile.OffsetList.ChunkID, nnFile.OffsetList.ChunkSize, nnFile.OffsetList.PointerCount);
            }

            //  NN_FILENAME  //
            if (nnFile.Footer.ChunkID != null)
            {
                Console.WriteLine("\nNinja File Name      [{0}]\nChunk Size          : {1}" +
                    "\nFile Name           : {2}",
                    nnFile.Footer.ChunkID, nnFile.Footer.ChunkSize, nnFile.Footer.FileName);
            }
        }
    }
}
