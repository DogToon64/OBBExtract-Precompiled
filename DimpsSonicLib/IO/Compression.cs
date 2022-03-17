using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using DimpsSonicLib;

namespace DimpsSonicLib.IO
{
    public class Compression
    {
        #region zlib
        public static Stream CompressStream(Stream input, int type)
        {
            // ...

            throw new NotImplementedException("zlib not implemented!");
            // return packedStream;
        }

        public static byte[] CompressZLibChunk(byte[] input)
        {
            // ...

            throw new NotImplementedException("zlib not implemented!");
            // return packedStream;
        }

        public static Stream DecompressStream(Stream input)
        {
            // ...

            throw new NotImplementedException("zlib not implemented!");
            // return unpackedStream; 
        }
        #endregion

        #region Dimps RLE 
        public static Stream NitroRLEncode(Stream input)
        {
            // ...

            throw new NotImplementedException("Run-length encoding not implemented!");
            // return encodedStream;
        }

        public static Stream NitroRLDecode(Stream input)
        {
            // ...

            throw new NotImplementedException("Run-length decoding not implemented!");
            // return decodedStream; 
        }
        #endregion
    }
}
