using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.IO
{
    // File utilities and operations
    public class FileInterface
    {
        // Completely unused, but a list of supported files I suppose. Maybe I'll find a use for this someday.
        public enum FileType
        {
            Unknown, OBB, AMB, RG, EV, DC, MD, MP, GPB, LTS, MFS
        }

        /// <summary>
        /// Returns the full path to a given file relative to the program's directory
        /// </summary>
        /// <param name="file">File name (including extension)</param>
        /// <returns>The full path to the file</returns>
        public static string GetLocalFile(string file, bool printResult = false)
        {
            string fileDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + file;

            if (File.Exists(fileDir))
            {
                if (printResult)
                    Console.WriteLine("GetFile Returned: {0}\n", fileDir);
                return fileDir;
            }
            else throw new Exception("Local file \"" + file + "\" doesn't seem to exist.");
        }

        /// <summary>
        /// Creates a directory at the location of a file
        /// </summary>
        /// <param name="file">The file's full path</param>
        /// <param name="dirName">The name of the directory being created (E.g. "MyNewDirectoryName")</param>
        /// <returns>The full path of the newly made directory</returns>
        public static string CreateDirectoryAtFileLocation(string file, string dirName)
        {
            var dir = Path.GetFullPath(file).Replace(Path.GetFileName(file), "") + dirName;
            Console.WriteLine("Dir@FileLocation: " + dir);
            return dir;
        }

        /// <summary>
        /// Creates a directory at the program's root folder
        /// </summary>
        /// <param name="dirName">The name of the directory being created (E.g. "MyNewDirectoryName")</param>
        /// <returns>The full path of the newly made directory</returns>
        public static string CreateDirectoryAtProgramLocation(string dirName)
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + dirName;
            Console.WriteLine("Dir@EXELocation: " + dir);
            return dir;
        }


    }
}
