using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.IO;
using DimpsSonicLib.Archives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AMBExtract
{
    class Functions
    {
        public static void UnpackAMBFile(string args)
        {
            try
            {
                // Testing stuff 

                Stream stream2 = File.OpenRead(args);
                var reader = new MemoryBinderReader(stream2);
                var header = reader.ReadHeader();
                CheckAMB(header);

                // = = = = = = =


                // Create extraction folder named after current file


                // Load file from stream
                Stream stream = File.OpenRead(args);
                var amb = MemoryBinder.ReadAMB(stream);
                AMBHeader amH = (AMBHeader)amb.Header;


                // Create index.bin based on current loaded file
                if (amb.Version == MemoryBinder.Version.Rev0)
                {
                    AMBSubHeader amSH = (AMBSubHeader)amb.SubHeader;
                    List<AMBFileIndex> amFI = (List<AMBFileIndex>)amb.FileIndex;

                    ExtendedBinaryReader a = new ExtendedBinaryReader(stream);
                    a.JumpTo(amSH.nameTable);


                }
                else if (amb.Version == MemoryBinder.Version.Rev1)
                {
                    AMBSubHeader1 amSH1 = (AMBSubHeader1)amb.SubHeader;
                    List<AMBFileIndex1> amFI1 = (List<AMBFileIndex1>)amb.FileIndex;

                    ExtendedBinaryReader a = new ExtendedBinaryReader(stream);
                    a.JumpTo(amSH1.nameTable);


                }
                else if (amb.Version == MemoryBinder.Version.Rev2)
                {
                    AMBSubHeader2 amSH2 = (AMBSubHeader2)amb.SubHeader;
                    List<AMBFileIndex2> amFI2 = (List<AMBFileIndex2>)amb.FileIndex;

                    ExtendedBinaryReader a = new ExtendedBinaryReader(stream);
                    a.JumpTo((long)amSH2.nameTable);


                }
                else throw new Exception("Something happened.");


                // Extract data



            }
            catch (Exception ex)
            {
                Logger.PrintError(ex.Message.ToString());
            }
        }

        public static void PackAMBFile(string args)
        {
            try
            {
                Logger.PrintError("PackAMBFile Not Implemented\n");

                // Try get either index binary or read from original.


                // Pack data from current list 



            }
            catch (Exception ex)
            {
                Logger.PrintError(ex.Message.ToString());
            }
        }

        public static void CheckAMB(AMBHeader header)
        {
        #if DEBUG
            Logger.PrintInfo("[DEBUG]");
            MemoryBinder.Version ver = MemoryBinder.GetAMBVersion(header);

            switch (ver)
            {
                case MemoryBinder.Version.Rev0:
                    Logger.PrintWarning("AMB version:        Base Version");
                    break;
                case MemoryBinder.Version.Rev1:
                    Logger.PrintWarning("AMB version:        Revision 1");
                    break;
                case MemoryBinder.Version.Rev2:
                    Logger.PrintWarning("AMB version:        Revision 2");
                    break;
                case MemoryBinder.Version.Unknown:
                    Logger.PrintWarning("AMB version:        Unknown Type");
                    break;
            }

            Logger.PrintWarning("Is big endian?:     " + header.isBigEndian.ToString());
            Logger.PrintWarning("Compression type:   " + header.compressionType.ToString() + "\n");
        #endif
        }
    }
}
