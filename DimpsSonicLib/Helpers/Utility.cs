using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib
{
    public static class Utility
    {
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
        /// Returns a new directory at the location of a file (string)
        /// </summary>
        /// <param name="file">The full path of file</param>
        /// <param name="dirName">The name of the folder being created (E.g. "My New Folder")</param>
        /// <returns>The full path of the newly made directory</returns>
        public static string CreateDirectoryAtFileLocation(string file, string dirName) // Change to NewFolderAtFilePath
        {
            var dir = Path.GetFullPath(file).Replace(Path.GetFileName(file), "") + dirName;
            //Console.WriteLine("Dir@FileLocation: " + dir);
            return dir;
        }

        /// <summary>
        /// Returns a new directory at the location of a file (string)
        /// </summary>
        /// <param name="file">The full path of file</param>
        /// <param name="targetDir">Target directory to create the folder in</param>
        /// <param name="append">Text to append to the folder name (E.g. "_extracted")</param>
        /// <returns>The full path of the newly made directory</returns>
        public static string CreateFolderFromFileName(string file, string targetDir = "", string append = "") // Change to NewFolderFromFile
        {
            string dir;
            var fileName = Path.GetFileNameWithoutExtension(file);

            if (targetDir != "")
                dir = targetDir + "\\" + fileName + append;
                //return targetDir + "\\" + fileName + append;
            else
                dir = Path.GetFullPath(file).Replace(Path.GetFileName(file), "") + fileName + append;
                //return Path.GetFullPath(file).Replace(Path.GetFileName(file), "") + fileName + append;

            //Console.WriteLine("FileFolder: " + dir);
            return dir;
        }

        /// <summary>
        /// Creates a directory at the program's root folder (string)
        /// </summary>
        /// <param name="dirName">The name of the folder being created (E.g. "My New Folder")</param>
        /// <returns>The full path of the newly made directory</returns>
        public static string CreateDirectoryAtProgramLocation(string dirName) // Change to NewFolderAtProgramRoot
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + dirName;
            //Console.WriteLine("Dir@EXELocation: " + dir);
            return dir;
        }


    }
}
