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


        }




        private static void PrintData(NNFile nnFile)
        {
            Console.WriteLine("\nNinja Info           [{0}]\nChunk Size          : {1}\n" +
                "Chunk Count         : {2}\nData Pointer        : {3}\nData Size           : {4}\n" +
                "Offset List Pointer : {5}\nOffset List Size    : {6}\nVersion             : {7}",
                nnFile.Info.chunkID, nnFile.Info.chunkSize, nnFile.Info.chunkCount, nnFile.Info.dataPointer,
                nnFile.Info.dataSize, nnFile.Info.offsetListPointer, nnFile.Info.offsetListSize, nnFile.Info.version);

            if (nnFile.Textures.chunkID != null)
            {
                Console.WriteLine("\nNinja Texture List   [{0}]\nChunk Size          : {1}",
                    nnFile.Textures.chunkID, nnFile.Textures.chunkSize);
            }

            if (nnFile.NodeNames.chunkID != null)
            {
                Console.WriteLine("\nNinja Node Names     [{0}]\nChunk Size          : {1}",
                    nnFile.NodeNames.chunkID, nnFile.NodeNames.chunkSize);
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

            if (nnFile.VertexAnim.chunkID != null)
            {
                Console.WriteLine("\nNinja Vertex Motion  [{0}]\nChunk Size          : {1}",
                    nnFile.VertexAnim.chunkID, nnFile.VertexAnim.chunkSize);
            }

            if (nnFile.OffsetList.chunkID != null)
            {
                Console.WriteLine("\nNinja Offset List    [{0}]\nChunk Size          : {1}" +
                    "\nPointer Count       : {2}",
                    nnFile.OffsetList.chunkID, nnFile.OffsetList.chunkSize, nnFile.OffsetList.pointerCount);
            }

            if (nnFile.Footer.chunkID != null)
            {
                Console.WriteLine("\nNinja File Name      [{0}]\nChunk Size          : {1}" +
                    "\nFile Name           : {2}",
                    nnFile.Footer.chunkID, nnFile.Footer.chunkSize, nnFile.Footer.fileName);
            }
        }
    }
}
