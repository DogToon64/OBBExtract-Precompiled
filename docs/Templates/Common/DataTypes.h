//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2021-09-18
//  Common Data Types
//  /// Other license, not GPL 3.0! (Tentative) ///
//------------------------------------------------
// #include "Utility.h"

typedef struct VECTOR3
{
    float X                  <name="X">;
    float Y                  <name="Y">;
    float Z                  <name="Z">;
};

typedef struct RGBA
{
    float Red                <name="Red", bgcolor=cLtRed>;
    float Green              <name="Green", bgcolor=cLtGreen>;
    float Blue               <name="Blue", bgcolor=cLtBlue>;
    float Alpha              <name="Alpha", bgcolor=cWhite>;
};

typedef struct RGB
{
    float Red                <name="Red", bgcolor=cLtRed>;
    float Green              <name="Green", bgcolor=cLtGreen>;
    float Blue               <name="Blue", bgcolor=cLtBlue>;
};