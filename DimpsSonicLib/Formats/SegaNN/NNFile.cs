using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Formats.SegaNN
{
    public class NNFile
    {
        public NNFile(string file)
        {
            Stream stream = File.OpenRead(file);

            ReadNNFile(stream);
        }

        public class SegaNN
        {
            public NN_INFO Info { get; set; }
            public NN_OFFSETLIST OffsetList { get; set; }
            public NN_FILENAME Footer { get; set; }
        }

        public SegaNN Data { get; set; } = new SegaNN();

        public void ReadNNFile(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);

            Data.Info       = new NN_INFO();
            Data.OffsetList = new NN_OFFSETLIST();
            Data.Footer     = new NN_FILENAME();

            Data.Info.chunkID           = reader.ReadSignature();
            Data.Info.chunkSize         = reader.ReadUInt32();
            Data.Info.chunkCount        = reader.ReadUInt32();
            Data.Info.dataPointer       = reader.ReadUInt32();
            Data.Info.dataSize          = reader.ReadUInt32();
            Data.Info.offsetListPointer = reader.ReadUInt32();
            Data.Info.offsetListSize    = reader.ReadUInt32();
            Data.Info.version           = reader.ReadUInt32();

            reader.JumpTo(Data.Info.dataPointer);
            Console.WriteLine(reader.ReadSignature());

            reader.JumpTo(Data.Info.offsetListPointer);
            Console.WriteLine(reader.ReadSignature());


        }


    }

    
}