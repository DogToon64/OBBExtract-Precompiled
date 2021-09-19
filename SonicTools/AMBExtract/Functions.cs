using System;
using System.IO;
using DimpsSonicLib;
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
                Stream stream = File.OpenRead(args);
                var reader = new MemoryBinderReader(stream);
                var header = reader.ReadHeader();

                DevFunc(header);
                CheckAMB(header);


                // = = = = = = =


                // Create extraction folder named after current file


                // Load file from stream


                // Create index.bin based on current loaded file


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

        public static void DevFunc(AMBHeader header)
        {

            switch (MemoryBinder.GetAMBVersion(header))
            {
                case MemoryBinder.Version.Rev0:
                    Logger.PrintError("Cast to AMBRev0");
                    break;
                case MemoryBinder.Version.Rev1:
                    Logger.PrintError("Cast to AMBRev1");
                    break;
                case MemoryBinder.Version.Rev2:
                    Logger.PrintError("Cast to AMBRev2");
                    break;
                case MemoryBinder.Version.Unknown:
                    throw new NotImplementedException("Unknown AMB Version");
            }

            // Brain has stopped working. 

        }

        public static void CheckAMB(AMBHeader header)
        {
        #if DEBUG
            Logger.PrintInfo("[DEBUG]");
            MemoryBinder.Version uwu = MemoryBinder.GetAMBVersion(header);

            switch (uwu)
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
