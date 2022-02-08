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
        public static void UnpackAMBFile(string arg)
        {
            try
            {
                // Create extraction folder named after current file


                // Load AMB file from argument
                Stream stream = File.OpenRead(arg);
                AMB amb = Binder.ReadAMB(stream);

                



                //Binder.WriteAMB(amb);



                // Create index based on current file



                // Extract data



            }
            catch (Exception ex)
            {
                Log.PrintError(ex.ToString());
            }
        }

        public static void PackAMBFile(string args)
        {
            try
            {
                Log.PrintError("PackAMBFile Not Implemented\n");

                // Try get either index binary or read from original.

                // If above fails, generate a brand new file after user prompt (can be silenced)

                // Pack data from current list 



            }
            catch (Exception ex)
            {
                Log.PrintError(ex.Message.ToString());
            }
        }

        public static void ChangeEndian()
        {
            
        }

        //public static void CheckAMB(Stream stream)
        //{
        //#if DEBUG
        //    var reader = new MemoryBinderReader(stream);
        //    var header = reader.ReadHeader();
            
        //    Logger.PrintInfo("[DEBUG]");
        //    MemoryBinder.Version ver = MemoryBinder.GetAMBVersion(header);
        //    var sheader = reader.ReadSubHeader(ver);

        //    switch (ver)
        //    {
        //        case MemoryBinder.Version.Rev0:
        //            Logger.PrintWarning("AMB version:        Base Version");
        //            break;
        //        case MemoryBinder.Version.Rev1:
        //            Logger.PrintWarning("AMB version:        Revision 1");
        //            break;
        //        case MemoryBinder.Version.Rev2:
        //            Logger.PrintWarning("AMB version:        Revision 2");
        //            break;
        //        case MemoryBinder.Version.Unknown:
        //            Logger.PrintWarning("AMB version:        Unknown Type");
        //            break;
        //    }

        //    Logger.PrintWarning("Is big endian?:     " + header.isBigEndian.ToString());
        //    Logger.PrintWarning("Compression type:   " + header.compressionType.ToString());
        //    Logger.PrintInfo   ("Files in AMB:       " + "\n");  // Add after implementing covariance/contravariance
        //#endif
        //}
    }
}
