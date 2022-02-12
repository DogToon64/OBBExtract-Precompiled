//------------------------------------------------
//  Header file for 010 Editor Binary Template
//  Author: Kass(RadiantDerg) 2022-02-11
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

// Compare a char array at the current read position; Returns '1' on match.
// The length of the signature to be compared can be up to 32 chars MAX.
local char sigValue[32]                         <hidden=true>;
local int CheckSignature(char cmp[])
{
    local int size = (sizeof(cmp) - 1);
    ReadBytes(sigValue, FTell(), size);
    
    if (sigValue == cmp)
    {
        Printf("Signature check of '%s' OK\n", cmp);
        return 1;
    }
    else
    {
        Printf("Signature check of '%s' FAILED. Input was '%s'.\n", cmp, sigValue);
        return 0;
    }
}


