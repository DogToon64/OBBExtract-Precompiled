using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.Formats.SegaNN;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CmdTest
{
    public class ZNOReader
    {
        public static void ReadZNO(string file)
        {
            // ZNO reading tests
            Stream stream = File.OpenRead(file);

            NNFile nnFile = new NNFile(stream);
            nnFile.ReadNNFile();


            Console.WriteLine("\nNinja Info           [{0}]\n" +
                "Chunk Size          : {1}\n" +
                "Chunk Count         : {2}\n" +
                "Data Pointer        : {3}\n" +
                "Data Size           : {4}\n" +
                "Offset List Pointer : {5}\n" +
                "Offset List Size    : {6}\n" +
                "Version             : {7}",
                nnFile.Info.chunkID, nnFile.Info.chunkSize, nnFile.Info.chunkCount, nnFile.Info.dataPointer,
                nnFile.Info.dataSize, nnFile.Info.offsetListPointer, nnFile.Info.offsetListSize, nnFile.Info.version);

            if (nnFile.NodeNames.chunkID != null)
            {
                Console.WriteLine("\nNinja Node Names     [{0}]\nChunk Size          : {1}",
                    nnFile.NodeNames.chunkID, nnFile.NodeNames.chunkSize);
            }

            if (nnFile.Textures.chunkID != null)
            {
                Console.WriteLine("\nNinja Texture List   [{0}]\nChunk Size          : {1}",
                    nnFile.Textures.chunkID, nnFile.Textures.chunkSize);
            }

            if (nnFile.Effect.chunkID != null)
            {
                Console.WriteLine("\nNinja Effect         [{0}]\nChunk Size          : {1}",
                    nnFile.Effect.chunkID, nnFile.Effect.chunkSize);
            }

            if (nnFile.Mesh.chunkID != null)
            {
                Console.WriteLine("\nNinja Object         [{0}]\nChunk Size          : {1}",
                    nnFile.Mesh.chunkID, nnFile.Mesh.chunkSize);
            }
            
            if (nnFile.Motion.chunkID != null)
            {
                Console.WriteLine("\nNinja Motion         [{0}]\nChunk Size          : {1}",
                    nnFile.Motion.chunkID, nnFile.Motion.chunkSize);
            }


        }
    }
}
