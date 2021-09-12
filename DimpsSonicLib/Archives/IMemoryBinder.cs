using System.IO;

namespace DimpsSonicLib.Archives
{
    public interface IMemoryBinder
    {
        bool isStream { get; set; }

        void ReadSubHeader(Stream input);

        void ReadFileIndex(Stream input);

        void ReadAMB(Stream input);

        void WriteAMB(bool isBigEndian, byte compressionType);

    }
}
