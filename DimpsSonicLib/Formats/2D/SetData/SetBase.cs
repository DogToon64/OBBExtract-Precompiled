using System;
using System.Collections.Generic;
using System.Text;


namespace DimpsSonicLib.Formats._2D.SetData
{
    public class Header
    {
        public byte width { get; set; } = 0;
        public byte height { get; set; } = 0;
    }

    public class CellData
    {
        public ushort count { get; set; } = 0;
    }

    public class Ring
    {
        public byte x { get; set; } = 0; //255 is NoCreate flag
        public byte y { get; set; } = 0;
    }

    public class Decorate
    {
        public byte x { get; set; } = 0;
        public byte y { get; set; } = 0;
        public ushort id { get; set; }
    }

    public class Event
    {
        public byte x { get; set; } = 0;
        public byte y { get; set; } = 0;
        public ushort id;
        public ushort flag;
        public sbyte offset_x;
        public sbyte offset_y;
        public byte wParam;
        public byte hParam;
        public ushort unknown; //always seems to be 0
    }

    // TODO: Add DF, MP, MD

}
