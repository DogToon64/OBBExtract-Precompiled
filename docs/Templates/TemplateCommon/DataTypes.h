//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2022-03-07
//  Updated: 2024-03-02
//  Common Data Types
//  /// Other license, not GPL 3.0! (Tentative) ///
//------------------------------------------------
#ifndef DATATYPES_
#define DATATYPES_

// Generic 
typedef struct
{
    float X                 <name="X">;
    float Y                 <name="Y">;
} VECTOR2 <read=Str("[%.2f, %.2f]", X, Y)>;

typedef struct
{
    float X                 <name="X">;
    float Y                 <name="Y">;
    float Z                 <name="Z">;
} VECTOR3 <read=Str("[%.2f, %.2f, %.2f]", X, Y, Z)>;

typedef struct
{
    float X                 <name="X">;
    float Y                 <name="Y">;
    float Z                 <name="Z">;
    float W                 <name="W">;
} VECTOR4 <read=Str("[%.2f, %.2f, %.2f, %.2f]", X, Y, Z, W)>;

typedef struct MATRIX //MATRIX44
{
    VECTOR4 a 				<name="Row 1">;
    VECTOR4 b 				<name="Row 2">;
    VECTOR4 c 				<name="Row 3">;
    VECTOR4 d 				<name="Row 4">;
};


// Color
typedef struct RGB
{
    float Red               <name="Red", bgcolor=cLtRed>;
    float Green             <name="Green", bgcolor=cLtGreen>;
    float Blue              <name="Blue", bgcolor=cLtBlue>;
};

typedef struct RGBA
{
    float Red               <name="Red", bgcolor=cLtRed>;
    float Green             <name="Green", bgcolor=cLtGreen>;
    float Blue              <name="Blue", bgcolor=cLtBlue>;
    float Alpha             <name="Alpha", bgcolor=cWhite>;
};

typedef struct RGBA8
{
#ifdef DATATYPES_USE_ARGB8
	ubyte Alpha             <name="Alpha", bgcolor=cWhite>;
	ubyte Red               <name="Red", bgcolor=cLtRed>;
	ubyte Green             <name="Green", bgcolor=cLtGreen>;
	ubyte Blue              <name="Blue", bgcolor=cLtBlue>;
#else
	ubyte Red               <name="Red", bgcolor=cLtRed, comment="If Alpha, use #define DATATYPES_USE_ARGB8">;
    ubyte Green             <name="Green", bgcolor=cLtGreen>;
    ubyte Blue              <name="Blue", bgcolor=cLtBlue>;
    ubyte Alpha             <name="Alpha", bgcolor=cWhite>;
#endif
};


// Template Results Helpers
typedef enum <ubyte> // Bool dropdown
{
    False,
    True
} bool;


typedef struct // Binary Angular Measurement (uhh fuck idk how to handle this LOL)
{
    ushort _bam16           <name="BAM16 Angle">;
    local float angle = (float)360.0 / 65535 * _bam16;
} BAM16 <read=Str("[%.3f]", angle)>;

#endif