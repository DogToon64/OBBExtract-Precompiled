using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.Archives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBExtract
{
    class Functions
    {
        public static void UnpackAMBFile(string args)
        {
            Stream stream = File.OpenRead(args);

            var header = MemoryBinder.ReadHeader(stream);

            Logger.Print("AMB version:        " + header.version.ToString());
            Logger.Print("Is big endian?:     " + header.isBigEndian.ToString());
            Logger.Print("Compression type:   " + header.compressionType.ToString() + "\n");

            // Below is not legal code, just shittily brainstorming.

            // var IndexList = IMemoryBinder.ReadFileIndex( stream, type, (endian)false );

            // foreach Item in <IndexList> { IMemoryBinder.ExtractFile( IndexList.Pointer ) }; 
            // Alternatively... for (int i = 0; i < AMBSubHeader.FileCount, i++) { Archive.Extract(stream, IndexList[i]) };

            // IMemoryBinder.ReadAMB(stream, (isStream)false, (endian)false, ); 

        }

        public static void PackAMBFile(string args)
        {
            Logger.PrintError("PackAMBFile Not Implemented\n");

            //Try get either index binary or read from original.

            //

        }
    }
}
