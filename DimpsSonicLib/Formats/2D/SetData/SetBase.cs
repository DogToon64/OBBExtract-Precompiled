using System;
using System.Collections.Generic;
using System.Text;


namespace DimpsSonicLib.Formats._2D.SetData
{
    public class Header
    {
        public byte Width { get; set; } = 0;
        public byte Height { get; set; } = 0;
    }

    public class CellData
    {
        public ushort Count { get; set; } = 0;
    }

    public class Ring
    {
        public byte X { get; set; } = 0; //255 is NoCreate flag
        public byte Y { get; set; } = 0;
    }

    public class Decorate
    {
        public byte X { get; set; } = 0;
        public byte Y { get; set; } = 0;
        public ushort ID { get; set; } // Game specific
    }

    public class Event
    {
        public byte X { get; set; } = 0;
        public byte Y { get; set; } = 0;
        public ushort ID; // Game specific
        public ushort Flag; // Game specific
        public sbyte OffsetX;
        public sbyte OffsetY;
        public byte wParam;
        public byte hParam;
        public ushort Unknown; //always seems to be 0
    }

    // TODO: Add DF, MP, MD

}
