using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DimpsSonicLib.IO;

namespace DimpsSonicLib.Archives
{
    public class MemoryBinder : IMemoryBinder
    {
        public bool isStream { get; set; }

        enum AMBVer
        {
            //Common, Type 1, Type 2
            Rev0, Rev1, Rev2, Unknown
        }

        /// <summary>
        /// Checks the file magic within the AMB file header, returns false if invalid.
        /// </summary>
        /// <param name="input">File Stream to be passed in</param>
        /// <returns></returns>
        public static bool CheckSignature(Stream input)
        {
            var reader = new ExtendedBinaryReader(input);

            if (reader.ReadSignature(4) == "#AMB")
                return true;
            else
                return false;
        }

        public void ReadAMB(Stream input)
        {
            throw new NotImplementedException();
        }

        public static AMBHeader ReadHeader(Stream input)
        {
            var reader = new ExtendedBinaryReader(input);
            AMBHeader header = new AMBHeader { };

            if (reader.ReadSignature(4) != AMBHeader.signature)
                throw new Exception("Signature does not match, aborting.");

            reader.JumpAhead(8);
            header.isBigEndian = reader.ReadByte() == 1 ? true : false;

            reader.JumpAhead(2);
            header.compressionType = reader.ReadByte();

            reader.JumpTo(4);
            if (header.isBigEndian)
            {
                reader.IsBigEndian = true; header.version = reader.ReadUInt32();
            }
            else
                header.version = reader.ReadUInt32();
            
            return header;
        }

        public void ReadSubHeader(Stream input)
        {
            throw new NotImplementedException();
        }

        public void ReadFileIndex(Stream input)
        {
            throw new NotImplementedException();
        }

        public void WriteAMB(bool isBigEndian, byte compressionType)
        {
            throw new NotImplementedException();
        }

    }

}
