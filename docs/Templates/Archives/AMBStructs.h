//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2021-09-18
//  Updated: 2023-12-16
//  Dimps AmFS: Memory Binder (.AMB)
//  /// Other license, not GPL 3.0! (Tentative) ///
//------------------------------------------------
#include "../TemplateCommon/Utility.h"

// Template Read Helpers
string EndianRead(byte  in)
{
	if (in == 1)
		return "Big Endian";
	else
		return "Little Endian";
}

typedef struct Header
{
    char   signature[4]                         <name="Signature", bgcolor=cWhite>;
    uint   version                              <name="File Version", bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
    ushort unkEditorVar1                        <name="?", hidden=true, bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
	// Might be byte alignment? only used by their internal tool.
    ushort unkEditorVar2                        <name="?", hidden=true, bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
    byte   isBigEndian                          <name="Endianness", read=EndianRead, bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
    byte   unkEditorVar3                        <name="?", hidden=true, bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
    byte   unkEditorVar4                        <name="?", hidden=true, bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
    byte   compressionType                      <name="Compression Type(?)", bgcolor=cSilver, comment="DIMPS EDITOR ONLY">;
	
    g_version = version;
    g_compressionType = compressionType;
};

typedef struct SubHeader // v20
{
    uint fileCount                              <name="File Count">;
    uint listPointer                            <name="List Pointer">;
    uint dataPointer                            <name="Data Pointer">;
    uint nameTable                              <name="Name Table Pointer">; //Ignored by AMBs that have had their contents hard-coded

    g_fileCount = fileCount;
    g_listPointer = listPointer;
    g_dataPointer = dataPointer;
    g_nameTable = nameTable;
};

typedef struct SubHeaderRev1 // v28
{
    uint fileCount                              <name="File Count">;
    uint listPointer                            <name="List Pointer">;
    uint unk1                                   <name="Unknown">;
    uint dataPointer                            <name="Data Pointer">;
    uint nameTable                              <name="Name Table Pointer">; //Ignored by AMBs that have had their contents hard-coded
    uint unk2                                   <name="Unknown">;
    
    g_fileCount = fileCount;
    g_listPointer = listPointer;
    g_dataPointer = dataPointer;
    g_nameTable = nameTable;
};

typedef struct SubHeaderRev2 // v30
{
    uint64 fileCount                             <name="File Count">;
    uint64 listPointer                           <name="List Pointer">;
    uint64 dataPointer                           <name="Data Pointer">;
    uint64 nameTable                             <name="Name Table Pointer">; //Ignored by AMBs that have had their contents hard-coded

    g_fileCount = fileCount;
    g_listPointer = listPointer;
    g_dataPointer = dataPointer;
    g_nameTable = nameTable;
};

typedef struct FileIndexData // v20
{
    uint  filePointer                           <name="File Pointer", bgcolor=cAqua>;
    uint  fileSize                              <name="File Length", bgcolor=cAqua>;
    uint  unkEditorVar5                         <name="0x00 or 0xFFFFFFFF", bgcolor=cAqua, format=hex, comment="DIMPS EDITOR ONLY">;
    short USR0                                  <name="USR0 Data", bgcolor=cAqua, comment="DIMPS EDITOR ONLY">;
    short USR1                                  <name="USR1 Data", bgcolor=cAqua, comment="DIMPS EDITOR ONLY">;

    g_filePointer = filePointer;
    g_fileSize = fileSize;
};

typedef struct FileIndexDataRev1 // v28
{
    uint  filePointer                           <name="File Pointer", bgcolor=cAqua>;
    uint  unk1                                  <name="Unknown">;
    uint  fileSize                              <name="File Length", bgcolor=cAqua>;
    uint  unkEditorVar5                         <name="0x00 or 0xFFFFFFFF", bgcolor=cAqua, format=hex, comment="DIMPS EDITOR ONLY">;
    short USR0                                  <name="USR0 Data", bgcolor=cAqua, comment="DIMPS EDITOR ONLY">;
    short USR1                                  <name="USR1 Data", bgcolor=cAqua, comment="DIMPS EDITOR ONLY">;

    g_filePointer = filePointer;
    g_fileSize = fileSize;
};

typedef struct FileIndexDataRev2 // v30
{
    uint  filePointer                           <name="File Pointer">;
    uint  unk1[2]                               <name="Unknown">;
    uint  fileSize                              <name="File Length">;
    uint  unkEditorVar5                         <name="0x00 or 0xFFFFFFFF", format=hex, comment="DIMPS EDITOR ONLY">;
    short USR0                                  <name="USR0 Data", comment="DIMPS EDITOR ONLY">;
    short USR1                                  <name="USR1 Data", comment="DIMPS EDITOR ONLY">;

    g_filePointer = filePointer;
    g_fileSize = fileSize;
};

typedef struct CompressedBinder
{
    uint trueFileLen                            <name="File Length (Decompressed)">;
    local int64 fileLength                      <hidden=true> = FileSize();
    FileData a(fileLength-20)                   <name="Data", bgcolor=cDkGreen>;
};