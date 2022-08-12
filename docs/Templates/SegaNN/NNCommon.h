//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2022-03-07
//  Reference: DarioSamo, RadfordHound, Argx2121
//  SegaNN Formats (.*NO, .*NM, .*NV, ect..)
//  /// Other license, not GPL 3.0! ///
//------------------------------------------------
#include "..\TemplateCommon\Utility.h"
#include "..\TemplateCommon\DataTypes.h"

#ifndef NN_STRUCTS_
#define NN_STRUCTS_

/////  NN Types  /////
typedef struct NNS_TEXCOORD
{
    float U                 <name="U COORD">;
    float V                 <name="V COORD">;
};

typedef struct NNS_ROTATE_A16
{
    short X                 <name="X">;
    short Y                 <name="Y">;
    short Z                 <name="Z">;
};

typedef struct NNS_ROTATE_A32
{
    int X                   <name="X">;
    int Y                   <name="Y">;
    int Z                   <name="Z">;
};

typedef struct VECTOR3INT
{
    // BAM but they're storing short values as int
    // This is probably just NNS_ROTATE_A32
    int X                   <name="X", hidden=true>;
    int Y                   <name="Y", hidden=true>;
    int Z                   <name="Z", hidden=true>;

    local float X_BAM = X * (180 / 32767);
    local float Y_BAM = Y * (180 / 32767);
    local float Z_BAM = Z * (180 / 32767);
};




/////  NN Common  /////
struct NN_CHUNKBASE
{
    char header[4]                      <name="Chunk Identifier", bgcolor=cAqua>;
    uint nextSize                       <name="Next Size", hidden=true, bgcolor=cSilver, comment="Size of THIS chunk from next read position">;
    
    g_nextSize = nextSize;
};

typedef struct NOF0 // Offset Table
{
    local int i                         <hidden=true>;

    NN_CHUNKBASE a                      <hidden=true>;
    lastPos = FTell();

    uint ptrCount                       <name="Pointer Count">; 
    FSkip(4);

    for (i = 0; i < ptrCount; i++)
        uint ptrPointer                 <name="Pointer", bgcolor=cSilver>;

    FSeek((lastPos + g_nextSize));
};

typedef struct NFN0 // Footer (Sometimes not required)
{
    NN_CHUNKBASE a                      <hidden=true>;
    lastPos = FTell();

    uint64 padding                      <hidden=true>;

    string fileName                     <name="Name", bgcolor=cGray>;  

    FSeek((lastPos + g_nextSize));
};

typedef struct NEND // End
{
    NN_CHUNKBASE a                      <hidden=true>;
    char pad[g_nextSize]                <hidden=true>;
};

#endif
