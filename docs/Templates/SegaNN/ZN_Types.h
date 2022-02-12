//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2022-02-07
//  Reference: DarioSamo, RadfordHound, Argx2121
//  SegaNN ZN* Format (.ZNO, .ZNM, .ZNV, ect..)
//  /// Other license, not GPL 3.0! ///
//------------------------------------------------
//#include "Common/Utility.h"
//#include "Common/DataTypes.h"

//#include "..\TemplateCommon\Utility.h"
//#include "..\TemplateCommon\DataTypes.h"


/////  'Z' NN Other  /////
typedef struct ZNO_DATAOFFSET
{
    uint dataType                       <name="Type">;
    uint dataOffset                     <name="Offset", hidden=false>;   
    lastPos2 = FTell();

    g_dataType   = dataType;
    g_dataOffset = dataOffset;

    FSeek(lastPos2);
};

typedef struct ZNO_TEXTURE
{
    uint  unknown1              <name="Unknown 1", bgcolor=cSilver>;
    uint  unknown2              <name="Unknown 2", bgcolor=cSilver>;
    uint  nameOfst              <name="Name Offset", bgcolor=cSilver>;
    short filter1               <name="NNE_MIN Type", bgcolor=cSilver>;
    short filter2               <name="NNE_MAG Type", bgcolor=cSilver>;
    uint  unknown3              <name="Unknown 3", bgcolor=cSilver>;

    // Get texture file name. Normally we'd just create a string array,
    // but for the purpose of this template I'm doing it directly.
    lastPos2 = FTell();
    FSeek((32 + nameOfst));
    string textureName          <name="Texture Name", bgcolor=cGray>;
    FSeek(lastPos2);
};

typedef struct ZNO_MATERIALTEX
{
    uint textureFlag            <name="Texture Flag">;
    uint textureIndex           <name="Texture Index">;
    uint unknown                <name="Unknown">;
    float texOfst1              <name="Texture Offset X">;
    float texOfst2              <name="Texture Offset Y">;
    uint unknown2               <name="Unknown">;
    float texScal1              <name="Texture Scale X">;
    float texScal2              <name="Texture Scale Y">;
    char unknown[32]            <name="Unknown Data">;
};

typedef struct ZNO_VERTEXBLOCK(uint blockLength)
{
    char data[blockLength]      ;
};

typedef struct ZNO_NODENAME
{
    string nodeName             <name="Node Name", bgcolor=cGray>;
};




/////  'Z' NN Fields  /////
typedef struct NZIF // File Information
{
    NN_CHUNKBASE a                      <hidden=true>;
    uint chunkCount                     <name="Chunk Count", bgcolor=cSilver>;
    uint dataPtr                        <name="Data Pointer", bgcolor=cSilver>;
    uint dataSize                       <name="Data Size", bgcolor=cSilver>;
    uint NOF0Ptr                        <name="Offset Table Pointer", bgcolor=cSilver>;
    uint NOF0Size                       <name="Offset Table Size", bgcolor=cSilver>;
    uint version                        <name="Version", bgcolor=cSilver>;

    g_info = FTell();
};



typedef struct NZTL // Texture Information (Sometimes not required)
{
    local int i                         <hidden=true>;

    NN_CHUNKBASE a                      <hidden=true>;
    lastPos = FTell();

    // Pointer to texture count, ignores NZIF byte count. 
    uint numTexPtr                      <name="Texture Count Pointer", bgcolor=cSilver, hidden=true>;
    
    // int64 padding

    FSeek(numTexPtr + g_info - 4);
    uint unknown5                       <name="Unknown 5", bgcolor=cSilver>;
    uint texCount                       <name="Texture Count", bgcolor=cSilver>;
    uint unknown6                       <name="Unknown 6", bgcolor=cSilver>;
    FSeek(g_info + 12);

    for (i = 0; i < texCount; i++)
        ZNO_TEXTURE a                   <name="Texture">;

    FSeek((lastPos + g_nextSize));
};

typedef struct NZTL_GUESS // Texture Information (Hack version)
{   // This version strangely follows NZMO pretty closely..
    local int guess                     <hidden=true>;
    local int i                         <hidden=true>;

    NN_CHUNKBASE a                      <hidden=true>;
    lastPos = FTell();

    int  nextSize2                      <name="Param Chunk Size", bgcolor=cSilver, comment="Pointer">;
    guess = (nextSize2-12)/20;          // Guess texture count

    for (i = 0; i < guess; i++)
        ZNO_TEXTURE a                   <name="Texture">;
    
    uint unknown5                       <name="Unknown 5", bgcolor=cSilver>;
    uint texCount                       <name="Texture Count", bgcolor=cSilver>;
    uint unknown6                       <name="Unknown 6", bgcolor=cSilver>;

    FSeek((lastPos + g_nextSize));
};



typedef struct NZNN // Node Names (Bones, Sometimes not required)
{
    local int j                         <hidden=true>;
    
    NN_CHUNKBASE a                      <hidden=true>;
    lastPos = FTell();
    
    uint  listPtr   <hidden=true>;    
    FSeek(listPtr + g_info + 4); // Points to a value that's normally 0? skipping.
    
    uint nodeNameCount                  <name="Node Name Count", bgcolor=cSilver>;
    uint nodeIndexPtr                   <hidden=true>;
    
    FSeek(nodeIndexPtr + g_info);
    
    for (j = 0; j < nodeNameCount; j++)
    {
        uint nodeIndex                  <hidden=true>;
        uint nodeNamePtr                <hidden=true>;
        
        FSeek(nodeNamePtr + g_info);
        ZNO_NODENAME b                  <name="Bone Name">;
        FSeek(nodeIndexPtr + g_info + (8 * (j+1)));
    }
    
    FSeek(lastPos + g_nextSize);
};



typedef struct NZEF // Effect Information
{
    NN_CHUNKBASE a                      <hidden=true>;
    lastPos = FTell();


    FSeek((lastPos + g_nextSize));
};



typedef struct NZMO // Motion Information
{
    NN_CHUNKBASE a                      <hidden=true>;   
    lastPos = FTell();


    FSeek((lastPos + g_nextSize));
};