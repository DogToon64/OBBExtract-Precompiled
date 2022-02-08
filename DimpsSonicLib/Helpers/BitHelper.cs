// Source: https://github.com/blueskythlikesclouds/MikuMikuLibrary/blob/master/MikuMikuLibrary/IO/Common/BitHelper.cs
/* MIT License
 * 
 * Copyright (c) 2021 Skyth (https://github.com/blueskythlikesclouds)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

//*****************************************//
// Sources:                                //
// https://github.com/TGEnigma/Amicitia.IO //
// https://github.com/zeux/meshoptimizer   //
//*****************************************//

using System.Runtime.CompilerServices;

namespace DimpsSonicLib
{
    public static class BitHelper
    {
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static byte Unpack( byte value, int from, int to )
        {
            return ( byte ) ( ( value >> from ) & ( byte.MaxValue >> ( sizeof( byte ) * 8 - ( to - from ) ) ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ushort Unpack( ushort value, int from, int to )
        {
            return ( ushort ) ( ( value >> from ) & ( ushort.MaxValue >> ( sizeof( ushort ) * 8 - ( to - from ) ) ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static uint Unpack( uint value, int from, int to )
        {
            return ( value >> from ) & ( uint.MaxValue >> ( sizeof( uint ) * 8 - ( to - from ) ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ulong Unpack( ulong value, int from, int to )
        {
            return ( value >> from ) & ( ulong.MaxValue >> ( sizeof( ulong ) * 8 - ( to - from ) ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Pack( ref byte destination, byte value, int from, int to )
        {
            int mask = byte.MaxValue >> ( sizeof( byte ) * 8 - ( to - from ) );
            destination = ( byte ) ( ( destination & ~( mask << from ) ) | ( ( value & mask ) << from ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Pack( ref ushort destination, ushort value, int from, int to )
        {
            int mask = ushort.MaxValue >> ( sizeof( ushort ) * 8 - ( to - from ) );
            destination = ( ushort ) ( ( destination & ~( mask << from ) ) | ( ( value & mask ) << from ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Pack( ref uint destination, uint value, int from, int to )
        {
            uint mask = uint.MaxValue >> ( sizeof( uint ) * 8 - ( to - from ) );
            destination = ( destination & ~( mask << from ) ) | ( ( value & mask ) << from );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static void Pack( ref ulong destination, ulong value, int from, int to )
        {
            ulong mask = ulong.MaxValue >> ( sizeof( ulong ) * 8 - ( to - from ) );
            destination = ( destination & ~( mask << from ) ) | ( ( value & mask ) << from );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static byte Pack( byte destination, byte value, int from, int to )
        {
            int mask = byte.MaxValue >> ( sizeof( byte ) * 8 - ( to - from ) );
            return ( byte ) ( ( destination & ~( mask << from ) ) | ( ( value & mask ) << from ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ushort Pack( ushort destination, ushort value, int from, int to )
        {
            int mask = ushort.MaxValue >> ( sizeof( ushort ) * 8 - ( to - from ) );
            return ( ushort ) ( ( destination & ~( mask << from ) ) | ( ( value & mask ) << from ) );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static uint Pack( uint destination, uint value, int from, int to )
        {
            uint mask = uint.MaxValue >> ( sizeof( uint ) * 8 - ( to - from ) );
            return ( destination & ~( mask << @from ) ) | ( ( value & mask ) << @from );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static ulong Pack( ulong destination, ulong value, int from, int to )
        {
            ulong mask = ulong.MaxValue >> ( sizeof( ulong ) * 8 - ( to - from ) );
            return ( destination & ~( mask << from ) ) | ( ( value & mask ) << from );
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static int QuantizeUnorm( float v, int n )
        {
            int scale = (1 << n) - 1;

            v = (v >= 0) ? v : 0;
            v = (v <= 1) ? v : 1;

            return (int)(v * scale + 0.5f);
        }      
        
        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public static int QuantizeSnorm( float v, int n )
        {
            int scale = (1 << (n - 1)) - 1;

            float round = (v >= 0 ? 0.5f : -0.5f);

            v = (v >= -1) ? v : -1;
            v = (v <= +1) ? v : +1;

            return (int)(v * scale + round);
        }
    }
}