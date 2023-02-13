using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.SetData
{
    public class Decorate
    {
        public ubyte X { get; set; } = 0; //255 is NoCreate flag
        public ubyte Y { get; set; } = 0;
        // Other params
    }

    public class DecorateData : CellData
    {
        public ushort count { get; set; } = 0;
        public list<Decorate>(); //Property list
    }


}