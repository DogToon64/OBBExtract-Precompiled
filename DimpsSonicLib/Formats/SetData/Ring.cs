using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.SetData
{
    public class Header
    {
        public ushort width { get; set; } = 0;
        public ushort height { get; set; } = 0;
    }

    public class Ring
    {
        public ubyte X { get; set; } = 0; //255 is NoCreate flag
        public ubyte Y { get; set; } = 0;
    }

    public class RingCellData
    {
        public ushort count { get; set; } = 0; //Count of rings in cell
        public list<Ring>(); //coordinate list
    }



}
