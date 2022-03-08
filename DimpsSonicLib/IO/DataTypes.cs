using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace DimpsSonicLib
{
    // Various data type implementations used within DimpsSonicLib. 

    public struct VECTOR3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public VECTOR3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    public struct VECTOR4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public VECTOR4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W})";
        }
    }

    public struct RGB
    {
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }

        public RGB(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        // If the values passed in are 0-255
        public RGB(int red, int green, int blue, int alpha)
        {
            Red = (red / 255f);
            Green = (green / 255f);
            Blue = (blue / 255f);
        }

        public override string ToString()
        {
            return $"({Red}, {Green}, {Blue})";
        }

    }

    public struct RGBA
    {
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
        public float Alpha { get; set; }

        public RGBA(float red, float green, float blue, float alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        // If the values passed in are 0-255
        public RGBA(int red, int green, int blue, int alpha)
        {
            Red = (red / 255f);
            Green = (green / 255f);
            Blue = (blue / 255f);
            Alpha = (alpha / 255f);
        }

        public override string ToString()
        {
            return $"({Red}, {Green}, {Blue}, {Alpha})";
        }

    }



}
