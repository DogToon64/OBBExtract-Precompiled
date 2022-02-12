//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2021-09-18
//  Common Data Types
//  /// Other license, not GPL 3.0! (Tentative) ///
//------------------------------------------------


// Generic 
typedef struct VECTOR2
{
    float X                 <name="X">;
    float Y                 <name="Y">;
};

typedef struct VECTOR3
{
    float X                 <name="X">;
    float Y                 <name="Y">;
    float Z                 <name="Z">;
};

typedef struct VECTOR4
{
    float X                 <name="X">;
    float Y                 <name="Y">;
    float Z                 <name="Z">;
    float W                 <name="W">;
};

typedef struct MATRIX
{
    VECTOR4 a <name="Row 1">;
    VECTOR4 b <name="Row 2">;
    VECTOR4 c <name="Row 3">;
    VECTOR4 d <name="Row 4">;
};

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