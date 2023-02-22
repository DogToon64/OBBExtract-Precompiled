using System;
using System.IO;
using DimpsSonicLib.IO;
using System.Collections.Generic;
using System.Text;


namespace DimpsSonicLib.Formats._2D.MapData
{
    public struct MainLight
    {
        public int Enable; //Always 1
        public RGB Color; //sRGB
    }

    public struct Light
    {
        public int Enable; //Always 1
        // 4 bytes always 0
        public RGBA Color; //sRGB
        public float Intensity;

        // Light Direction stored in a vector
        // X: 1.0 = Left, -1.0 = Right
        // Y: 1.0 = Bottom, -1.0 = Top
        // Z: 1.0 = Rear, -1.0 = Front
        public VECTOR3 Direction;
        // int64 padding
    }

    public class LightSetting
    {
        // Main directional light color
        public MainLight Main;
        // Object1, Object2, Character1, Character2, Terrain1 , Terrain2, Unknown1, Unknown2
        public Light[] Lights;

        public LightSetting() { Main = new MainLight(); Lights = new Light[8]; }
        public LightSetting(Stream input) { Main = new MainLight(); Lights = new Light[8]; Read(input); }

        public void Read(Stream input)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(input, true);

            Main.Enable = reader.ReadInt32();
            Main.Color = reader.ReadRGB();

            for (int i = 0; i < 8; i++)
            {
                Lights[i].Enable = reader.ReadInt32();
                reader.JumpAhead(4);
                Lights[i].Color = reader.ReadRGBA();
                Lights[i].Intensity = reader.ReadSingle();
                Lights[i].Direction = reader.ReadVector3();
                reader.JumpAhead(32);
            }
        }

        public void Write(Stream input)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(input, true);

            writer.Write(Main.Enable);
            writer.Write(Main.Color.Red);
            writer.Write(Main.Color.Green);
            writer.Write(Main.Color.Blue);

            for (int i = 0; i < 8; i++)
            {
                writer.Write(Lights[i].Enable);
                writer.WriteNulls(4);
                writer.Write(Lights[i].Color.Red);
                writer.Write(Lights[i].Color.Green);
                writer.Write(Lights[i].Color.Blue);
                writer.Write(Lights[i].Color.Alpha);
                writer.Write(Lights[i].Intensity);
                writer.Write(Lights[i].Direction.X);
                writer.Write(Lights[i].Direction.Y);
                writer.Write(Lights[i].Direction.Z);
                writer.WriteNulls(32);
            }
        }
    }

    // = = = = = = //

    public struct Paralax
    {
        public int Unknown; //Always 1
        public int MDLOffset;
        public int ScrollAMT; //Negative numbers cause side effects
    }

    public struct FogParam
    {
        public int Unknown; //Always 1
        public int Toggle; //Treated as a boolean; 1=Enabled, 0=Disabled.
        public RGB Color; //sRGB
        public float Start;
        public float End;
    }

    public class MapFarSetting
    {
        public Paralax ParalaxH;
        public Paralax ParalaxV;
        public FogParam Fog;

        public MapFarSetting() { ParalaxH = new Paralax(); ParalaxV = new Paralax(); Fog = new FogParam(); }
        public MapFarSetting(Stream input) { ParalaxH = new Paralax(); ParalaxV = new Paralax(); Fog = new FogParam(); Read(input); }

        public void Read(Stream input)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(input, true);

            ParalaxH.Unknown = reader.ReadInt32();
            ParalaxH.MDLOffset = reader.ReadInt32();
            ParalaxH.ScrollAMT = reader.ReadInt32();

            ParalaxV.Unknown = reader.ReadInt32();
            ParalaxV.MDLOffset = reader.ReadInt32();
            ParalaxV.ScrollAMT = reader.ReadInt32();

            Fog.Unknown = reader.ReadInt32();
            Fog.Toggle = reader.ReadInt32();
            Fog.Color = reader.ReadRGB();
            Fog.Start = reader.ReadSingle();
            Fog.End = reader.ReadSingle();
        }

        public void Write(Stream input)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(input, true);

            writer.Write(ParalaxH.Unknown);
            writer.Write(ParalaxH.MDLOffset);
            writer.Write(ParalaxH.ScrollAMT);

            writer.Write(ParalaxV.Unknown);
            writer.Write(ParalaxV.MDLOffset);
            writer.Write(ParalaxV.ScrollAMT);

            writer.Write(Fog.Unknown);
            writer.Write(Fog.Toggle);
            writer.Write(Fog.Color.Red);
            writer.Write(Fog.Color.Green);
            writer.Write(Fog.Color.Blue);
            writer.Write(Fog.Start);
            writer.Write(Fog.End);
        }
    }
}