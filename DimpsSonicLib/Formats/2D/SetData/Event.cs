using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.2D.SetData
{
    public class Event
    {
        public ubyte X { get; set; } = 0; //255 is NoCreate flag
        public ubyte Y { get; set; } = 0;
        // Other params
    }

    public class EventData : CellData
    {
        public ushort count { get; set; } = 0;
        public list<Event>(); //Property list
    }


}