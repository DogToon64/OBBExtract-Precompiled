//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2021-09-18
//  Common Utilities 
//  /// Other license, not GPL 3.0! (Tentative) ///
//------------------------------------------------

typedef struct FileData (uint dataLength)
{
    char fileDataStart                          <bgcolor=cYellow>;
    char fileData[dataLength-2];
    char fileDataEnd                            <bgcolor=cRed>;
};

typedef struct CompressedData
{
    local int64 fileLength                      <hidden=true> = FileSize();
    FileData a(fileLength-20)                   <name="Data", bgcolor=cDkGreen>;
};