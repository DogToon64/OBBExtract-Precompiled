//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg)
//  Created: 2022-03-07
//  Updated: 2024-03-02
//  Reference: DarioSamo, RadfordHound, Argx2121
//  SegaNN ZN* Format (.ZNO, .ZNM, .ZNV, ect..)
//  /// Other license, not GPL 3.0! ///
//------------------------------------------------
#include "..\TemplateCommon\Utility.h"
#include "..\TemplateCommon\DataTypes.h"

#include "NNCommon.h"


/////  'Z' NN Structs  /////
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

typedef struct ZNO_MATERIALTEX_OLD
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

typedef struct ZNO_MATERIALTEX
{
    uint textureFlag                    <name="Texture Flag", format=hex>;
    uint textureIndex                   <name="Texture Index">;

    uint unknown                        <name="Unknown">;
    float unkPrm1                       <name="Opacity??">;
    float unkPrm2                       <name="Unknown">;
    uint unknown2                       <name="Unknown">;
    VECTOR2 b                           <name="Texture Scale">;

    char unknownD[28]                   <name="Unknown Data">; //56
};

typedef struct ZNO_MATERIAL(int type, int ofst)
{
    local uint matType                  <name="Material Type"> = type;
    local uint matOffset                <name="Material Offset"> = ofst;
    local int j <hidden=true>;

    uint unknown                        <name="Unknown 1">;
    uint unknown2                       <name="Unknown 2">;
    uint colorsPtr                      <name="Colors Pointer">;
    uint unkownPtr                      <name="Unknown Pointer">; // jumps to a section that contains another pointer to the highlights
    uint unknownCount                   <name="Unknown Count">;
    uint textureCount                   <name="Texture Count">;
    uint textureOfst                    <name="Texture Offset">;
    
    FSeek(colorsPtr + g_info);
    
    uint highlight                      <name="Highlights ?">;
    RGBA a                              <name="Base Color">;
    RGBA b                              <name="Shading 1">;
    RGBA c                              <name="Shading 2">;

    FSeek(textureOfst + g_info);

    for (j = 0; j < textureCount; j++)
    {
        ZNO_MATERIALTEX a               <name="Material Texture Data">;
    }

    FSeek(lastPos2);
};

typedef struct ZNO_MATOFFSET
{
    // notable values (S4E2)
    // 0x10000000, 0x40000000 cause material to become a render mask
    uint dataType                       <name="Type">;
    uint dataOffset                     <name="Offset", hidden=true>;

    lastPos2 = FTell();
    FSeek(dataOffset + g_info);
    ZNO_MATERIAL a(dataType, dataOffset)  <name="Params">;
    FSeek(lastPos2);
};

typedef struct ZNO_MATERIAL_LIST
{  
    for (i = 0; i < g_materialCount; i++)
    {
        ZNO_MATOFFSET a                <name="Material", hidden=false>;
    }
};


typedef struct ZNO_NODE
{
    // Bone matrices are inverted
    uint  nodeFlags                     <name="Bone Flags">;
    short nodeUsedIndex                 <name="Used Node Index", comment="FF FF (-1) = Unused">;
    short parentNodeIndex               <name="Parent Node Index", comment="FF FF (-1) = No parent">; //Uses whole bone list
    short childNodeIndex                <name="Child Node Index", comment="Can be wrong">;
    short siblingNodeIndex              <name="Next Sibling Node Index", comment="Can be wrong">; // Next child of the parent
    
    VECTOR3 a                           <name="Parent Relative Position", comment="Can change order">;
    VECTOR3INT a                        <name="BAM Relative To Parent", comment="Can change order">; //Calculate it by: value * (180 / 32767);
    float scaleX                        <name="X Scale">; //Might change order too
    float scaleY                        <name="Y Scale">; //Might change order too
    float scaleZ                        <name="Z Scale">; //Might change order too

    // World Space / Relative to scene origin
    MATRIX a                            <name="Bone Matrix">;
    
    // Render bounds
    VECTOR3 b                           <name="Center">;
    float radius                        <name="Radius">;
    float unknown                       <name="Unknown">;
    VECTOR3 c                           <name="Dimension / 2">;
};

typedef struct ZNO_NODENAME
{
    string nodeName                     <name="Node Name", bgcolor=cGray>;
};

typedef struct ZNO_NODEUSEDINDEX
{
    uint16 node                         ;
};

typedef struct ZNO_NODELIST
{
    for (i = 0; i < g_nodeCount; i++)
    {
        ZNO_NODE a                      <name="Bone">;
    }
};


typedef struct ZNO_OBJECTINFO
{
    VECTOR3 a                           <name="Center">;
    float radius                        <name="Radius">;

    uint materialCount                  <name="Material Count">;
    uint materialPtr                    <name="Material Pointer", hidden=false>;

    uint vertexCount                    <name="Vertex Count">;
    uint vertexPtr                      <name="Vertex Pointer", hidden=true>;

    uint faceCount                      <name="Face Count">;
    uint facePtr                        <name="Face Pointer", hidden=true>;

    uint nodeCount                      <name="Total Bone Count">;
    uint unusedNodeCount                <name="Unused Bone Count">;
    uint nodePtr                        <name="Bone Pointer", hidden=true>;
    uint usedNodeCount                  <name="Used Bone Count">;

    uint meshSetCount                   <name="Mesh Set Count">;
    uint meshSetPtr                     <name="Mesh Set Pointer", hidden=true>;
    uint textureCount                   <name="Texture Count">;

    g_materialPtr = materialPtr;
    g_vertexPtr   = vertexPtr;
    g_facePtr     = facePtr;
    g_nodePtr     = nodePtr;
    g_meshSetPtr  = meshSetPtr;

    g_materialCount   = materialCount;
    g_vertexCount     = vertexCount;
    g_faceCount       = faceCount;
    g_nodeCount       = nodeCount;
    g_unusedNodeCount = unusedNodeCount;
    g_usedNodeCount   = usedNodeCount;
    g_meshSetCount    = meshSetCount;
    g_textureCount    = textureCount;
};

typedef struct ZNO_VERTEXBLOCK(uint blockLength)
{
    char data[blockLength];
};

typedef struct ZNO_VERTEX(int type, int ofst)
{
    local int x                         <hidden=true>;
    int64 vertexFlags                   <name="Vertex Flags">;
    int   blockSize                     <name="Block Size">;
    int   vertCount                     <name="Vertex Count">;
    int   vertOffset                    <name="Vertex Offset">;
    int   boneCount                     <name="Bone Count">;
    int   boneOffset                    <name="Bone Offset">;

    if (boneOffset)
    {
        FSeek(boneOffset + g_info);
        
        for (x = 0; x < boneCount; x++)
        {
            ZNO_NODEUSEDINDEX a         <name="Used Node Index">;
        }
    }

    FSeek(vertOffset + g_info);
    
    for (i = 0; i < vertCount; i++)
    {
        ZNO_VERTEXBLOCK a(blockSize)    <name="Vertex Block", bgcolor=cBlack>;
    };

    FSeek(lastPos2);
};

typedef struct ZNO_VERTEXLIST
{
    local int z                         <hidden=true>;
    
    for (z = 0; z < g_vertexCount; z++)
    {
        ZNO_DATAOFFSET vert             <name="Vertex Info">;

        lastPos2 = FTell();
        FSeek(g_dataOffset + g_info);
        ZNO_VERTEX a(vert.dataType, vert.dataOffset) <name="Vertex">;
        FSeek(lastPos2);
    }
};

typedef struct ZNO_STRIP(uint len)
{
    short strip[len]                     <name="Face Index">;
};

typedef struct ZNO_STRIPLIST(uint num)
{
    for (w = 0; w < num; ++w)
    {
        ZNO_STRIP a(stripLen[w])        <name="Strip">;       
    }
};

typedef struct ZNO_FACE(int type, int ofst)
{
    local int w                         <hidden=true>;
    local int v                         <hidden=true>;
    uint unknown                        <name="Unknown">;
    uint faceCount                      <name="Face Count">;
    uint faceStripCount                 <name="Face Strip Count">;
    uint faceStripPtr                   <name="Face Strip Pointer">;
    uint facePtr                        <name="Face Pointer">;

    FSeek(faceStripPtr + g_info);
    
    ushort stripLen[faceStripCount]     <name="Strip Lengths">;

    FSeek(facePtr + g_info);
    
    ZNO_STRIPLIST a(faceStripCount)        <name="Strips">; // strip[stripLength[i]]

    FSeek(lastPos2);
};

typedef struct ZNO_FACELIST
{
    for (i = 0; i < g_faceCount; i++)
    {
        ZNO_DATAOFFSET face             <name="Face Info">;

        lastPos2 = FTell();
        FSeek(g_dataOffset + g_info);
        ZNO_FACE a(face.dataType, face.dataOffset) <name="Face">;
        FSeek(lastPos2);
    }
};

typedef struct ZNO_MESHTEXLIST(int texCount)
{
    local int jj                        <hidden=true>;

    for (jj = 0; jj < texCount; ++jj)
    {
        uint texID                      <name="Texture Index">;
    }
    FSeek(lastPos2);
};

typedef struct ZNO_MESH
{  
    VECTOR3 a                           <name="Center">;
    float radius                        <name="Radius">;

    uint nodeVis                        <name="Bone Visibility">; // Uses 'UsedNode' set
    uint nodeWeight                     <name="Weighted Bone">; // Uses overall node set
    uint material                       <name="Material Index">; 
    uint vertex                         <name="Vertex Index">;
    uint face                           <name="Face Index">;
    uint mesh                           <name="Mesh Index", comment="Optional, depends on platform">;
};

typedef struct ZNO_MESHSET
{
    local int ii                        <hidden=true>;
    
	// Static meshes contain only one Node, Weighted meshes can have multiple

    // Properties are literally irrelevant to sonic 4 wtf ????????????
	// Type     : Static = 256, Weighted = 512
	// Property : Opaque = 1, Transparent = 2, Punchthrough = 4

    // 01 01 = static meshes with textures (Opaque) 			00000000 00000001	00000000 00000001
    // 01 02 = weighted meshes with textures (Opaque) 			00000000 00000001	00000000 00000010
    // 01 04 = static meshes with textures (Punchthrough) 		00000000 00000001	00000000 00000100
    // 02 01 = static meshes with no textures (Transparent?) 	00000000 00000010	00000000 00000001
	// 04 01 = unknown
    uint meshSetFlags                   <name="Mesh Flags">;

    uint meshCount                      <name="Mesh Count">;
    uint meshPtr                        <name="Mesh Pointer", hidden=true>;
    uint textureCount                   <name="Mesh Texture Count">;
    uint texIndicesPtr                  <name="Texture Indices Pointer", hidden=true>; // Doesn't matter?

    lastPos2 = FTell();
    FSeek(texIndicesPtr + g_info);
    ZNO_MESHTEXLIST tex(textureCount)   <name="Mesh Texture List">;
    FSeek(lastPos2);
    
    FSeek(meshPtr + g_info);
    for (ii = 0; ii < meshCount; ++ii)
        ZNO_MESH mesh                   <name="Mesh">;
    FSeek(lastPos2);
};

typedef struct ZNO_MESHSETLIST
{   
    for (i = 0; i < g_meshSetCount; i++)
    {
        ZNO_MESHSET a <name="Mesh Set">;
    }
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



typedef struct NZOB // Object Information
{
    local int i                         <hidden=true>;
    NN_CHUNKBASE a                      <hidden=true>;
    
    lastPos = FTell();

    uint  modelInfoPtr                  <hidden=true>;
    uint  unknown                       <name="Unknown">;

    uint  vertBufferLen                 <name="Vertex Buffer Length">;
    uint  vertBufferPtr                 <name="Vertex Buffer Pointer", hidden=true>;
    int64 pad                           <hidden=true>;
    
    FSeek(modelInfoPtr + g_info);
    ZNO_OBJECTINFO a                    <name="Model Info", bgcolor=cGray>;    
    FSeek(g_nodePtr + g_info);
    ZNO_NODELIST a                      <name="Bone List", bgcolor=cSilver>;
    FSeek(g_materialPtr + g_info);
    ZNO_MATERIAL_LIST a                 <name="Material List", bgcolor=cGray>;
    FSeek(g_vertexPtr + g_info);
    ZNO_VERTEXLIST a                    <name="Vertex List", bgcolor=cSilver>;
    FSeek(g_facePtr + g_info);
    ZNO_FACELIST a                      <name="Faces List", bgcolor=cGray>;
    FSeek(g_meshSetPtr + g_info);
    ZNO_MESHSETLIST a                   <name="Mesh Set List", bgcolor=cSilver>;        
    
    FSeek((lastPos + g_nextSize)); 
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