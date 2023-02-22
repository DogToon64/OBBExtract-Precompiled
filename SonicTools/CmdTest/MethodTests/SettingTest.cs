using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimpsSonicLib.Formats._2D.MapData;

namespace CmdTest
{
    public class SettingTest
    {
        public static void ReadLTS(string file)
        {
            // Read File
            Console.WriteLine($"Opening {Path.GetFileName(file)}");

            LightSetting LTS = new LightSetting(File.OpenRead(file));
            Console.WriteLine("Terrain1 Color: {0}", LTS.Lights[4].Color.ToString());

            // Write File
            FileStream newLTS = File.OpenWrite((Path.GetFileNameWithoutExtension(file)) + "_new.LTS");
            LTS.Write(newLTS);
            newLTS.Close();

            Console.WriteLine("Done on LTS");
        }

        public static void ReadMFS(string file)
        {
            // Read File
            Console.WriteLine($"Opening {Path.GetFileName(file)}");

            MapFarSetting MFS = new MapFarSetting(File.OpenRead(file));
            Console.WriteLine("Fog Color: {0}", MFS.Fog.Color.ToString());

            // Write File
            FileStream newMFS = File.OpenWrite((Path.GetFileNameWithoutExtension(file)) + "_new.MFS");
            MFS.Write(newMFS);
            newMFS.Close();

            Console.WriteLine("Done on MFS");
        }
    }
}
