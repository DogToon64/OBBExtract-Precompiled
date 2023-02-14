using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using DimpsSonicLib;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace DimpsSonicLib.IO
{
    public class Compression
    {
        // TODO: Replace SharpZipLib entirely.
        #region zlib
        /// <summary>
        /// Compresses an array of bytes using zlib
        /// </summary>
        /// <param name="input">Byte array to deflate</param>
        /// <param name="type">Compression level 0-9. Default = 6 (DEFAULT_COMPRESSION)</param>
        /// <returns>Compressed byte array</returns>
        public static byte[] CompressZlibChunk(byte[] input, int type = 6)
        {
            MemoryStream result = new MemoryStream();
            DeflaterOutputStream zlib = new DeflaterOutputStream(result, new Deflater(type));

            zlib.Write(input, 0, input.Length);
            zlib.Finish();

            return result.ToArray();
        }

        /// <summary>
        /// Decompresses an array of zlibbed bytes
        /// </summary>
        /// <param name="input">Byte array to inflate</param>
        /// <returns>Decompressed byte array</returns>
        public static byte[] DecompressZlibChunk(byte[] input)
        {
            MemoryStream source = new MemoryStream(input);
            byte[] result = null;

            using (MemoryStream output = new MemoryStream())
            {
                using (InflaterInputStream zlib = new InflaterInputStream(source))
                {
                    zlib.CopyTo(output);
                }
                result = output.ToArray();
            }

            return result;
        }
        #endregion


        #region Dimps RLE 
        public static byte[] RunLengthEncode(byte[] input)
        {
            // ...

            throw new NotImplementedException("Run-length encoding not implemented!");
            // return encodedData;
        }

        public static byte[] RunLengthDecode(byte[] input)
        {
            // ...

            throw new NotImplementedException("Run-length decoding not implemented!");
            // return decodedData; 
        }

        private static void ParseRLHeader(uint header)
        {            
            // ...


        }

        #endregion
    }
}

