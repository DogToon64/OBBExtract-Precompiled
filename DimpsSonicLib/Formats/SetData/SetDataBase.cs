using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.SetData
{
    public class Header
    {
        public ubyte X { get; set; } = 0; //255 is NoCreate flag
        public ubyte Y { get; set; } = 0;
    }

    public class CellData
    {
        public ushort count { get; set; };
    }
}