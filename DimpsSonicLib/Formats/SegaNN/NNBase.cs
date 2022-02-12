using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.SegaNN
{
    // Placeholder
    public enum NNTYPE
    {
        NNZ, NNX, Unknown
    }

    public interface NN_CHUNKBASE
    {
        string chunkID { get; set; }
        dynamic chunkSize { get; set; }
    }

    public interface NN_CHUNKDATA_OFFSET
    {
        uint chunkDataType { get; set; }
        uint chunkDataOffset { get; set; }
    }

    public class NN_INFO : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public uint chunkCount { get; set; }
        public uint dataPointer { get; set; }
        public uint dataSize { get; set; }
        public uint offsetListPointer { get; set; }
        public uint offsetListSize { get; set; }
        public uint version { get; set; }
        
    }

    public class NN_FILENAME : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        // long padding; (8 bytes of padding)
        public string fileName { get; set; }
    }

    public class NN_OFFSETLIST : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }

        public uint pointerCount { get; set; }
    }

    // Literally just the end. Will have padding. 
    public class NN_END : NN_CHUNKBASE
    {
        public string chunkID { get; set; }
        public dynamic chunkSize { get; set; }
    }


}
