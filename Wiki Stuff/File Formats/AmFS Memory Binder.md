# AMB File Structure

### First, a little bit of background information is necessary:
AMB files are also known as 'Memory Binders'. 

They are completely **hard-coded** file archives, meaning that the game will *only* reference the files inside of these binders by their ID (The ID itself is the order of which the said file appears). 

This means that while you can freely edit whatever file they contain, the order of those files or the file formats themselves must **NOT** be changed. This will confuse the game and cause it to crash or hang.

**For example:** You cannot replace a `.ZNO` file with a `.ZNM` file. 

There are also several bytes within the binder that go unused by the actual game. These bytes are actually meant for an internal tool Dimps used to describe these AMB files. It would spit out a header containing information about the binder and it's contents, which would then be compiled into the final game's code.

Console versions are big endian. PC and Mobile are little endian.

### Memory Binders generally contain these components:
* A Header
* A Sub Header
* A File Index
* File data
* A name table (**Not** required and can be excluded for masking)


# Header
The header of an AMB file is unanimous among almost all versions of the file, with the exception of [Windows Phone AMBs.](https://github.com/OSA413/Sonic4_Tools/blob/master/docs/Specifications/AMB_wp.md "Windows Phone AMB File Structure")

    Offset(h)  0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F  0123456789ABCDEF
    00000000  23 41 4D 42 20 00 00 00  00 00 04 00 00 00 00 00  #AMB ...........

### The information stored in the header is as follows:
  
Address | Description
------- | -----------
  0x00  | `char signature[4]` - The file magic, better known as signature
  0x04  | `uint fileVersion` - The version of the AMB file
  0x08  | `ushort unknown1` - This always seems to be `0`
  0x0A  | `ushort unknown2` - This alwaus seems to be `4`
  0x0C  | `byte endianess` - Endianness flag. `0` is little, `1` is big
  0x0D  | `byte unknown3` - This always seems to be `0`
  0x0E  | `byte unknown4` - This always seems to be `0`
  0x0F  | `byte compressionType` - This flag details the compression method
  
**¹** - Changing this value to a random one doesn't crash game.

### Here are the identifiers for the AMB versions:

fileVersion | Description
------- | -----------
  32  | **AMB v1** (These are the most common types of memory binders you will encounter)
  40  | **AMB v2**
  48  | **AMB v3**

<br></br>
# Sub Header

First things first, We should talk about compressed AMBs. Only certain games feature these, and as such they are the **only games to support them.** 

For right now, we'll just assume that If `compressionType` is not equal to `0`, then the data following that is compressed.

    Offset(h)  0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F  0123456789ABCDEF
    00000000  23 41 4D 42 20 00 00 00  00 00 04 00 00 00 00 02  #AMB ...........
    00000010  00 00 00 00 ?? ?? ?? ??  ?? ?? ?? ?? ?? ?? ?? ??  ....????????????

Address | Description
------- | -----------
  0x10  | `uint fileSize` - This is how long in bytes the **UNCOMPRESSED** AMB is, starting from the header.
  0x14  | from here until the end of the file is the compressed data.
  
<br></br>
## AMB v1

    Offset(h)  0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F  0123456789ABCDEF
    00000010  03 00 00 00 20 00 00 00  60 00 00 00 60 2E 10 00  .... ...`...`...

### The information stored in the sub header is as follows:

Address | Description
------- | -----------
  0x00  | `uint fileCount` - The number of files contained in the AMB
  0x04  | `uint listPointer` - The pointer to the File Index
  0x08  | `uint dataPointer` - The pointer to the file data
  0x0C  | `uint nameTable`? - The pointer to the list of names. This CAN be nulled.

**¹** - Changing this value to a random one doesn't crash game.

<br></br>
## AMB v2

<br></br>
## AMB v3

    Offset(h)  0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F  0123456789ABCDEF
    00000010  15 00 00 00 00 00 00 00  30 00 00 00 00 00 00 00  ........0.......
    00000020  40 02 00 00 00 00 00 00  50 D7 0E 00 00 00 00 00  @.......P?......

### The information stored in the sub header is as follows:

Address | Description
------- | -----------
  0x00  | `uint64 fileCount` - The number of files contained in the AMB
  0x04  | `uint64 listPointer` - The pointer to the File Index
  0x08  | `uint64 dataPointer` - The pointer to the file data
  0x0C  | `uint64 nameTable`? - The pointer to the list of names. This CAN be nulled.

**¹** - Changing this value to a random one doesn't crash game.

<br></br><br></br>
# File Index

This is a complete list of the files contained within the AMB. Each file has it's own sequence of bytes detailing information about the file.

<br></br>
## AMB v1

    Offset(h)  0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F  0123456789ABCDEF
    00000010  60 00 00 00 30 8A 08 00  FF FF FF FF 00 00 00 00  `...0?..????....

### The information stored in the index entry is as follows:

Address | Description
------- | -----------
  0x00  | `uint filePointer` - The pointer to the file in the AMB
  0x04  | `uint fileSize` - The total length of the file in bytes
  0x08  | `uint unkEditorData`? - Unknown Data (Dimps Internal Tool)
  0x0C  | `short USR0`? - User 1 Data (Dimps Internal Tool)
  0x0E  | `short USR1`? - User 2 Data (Dimps Internal Tool)

**¹** - Changing this value to a random one doesn't crash game.

<br></br>
## AMB v2

<br></br>
## AMB v3

    Offset(h)  0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F  0123456789ABCDEF
    00000030  40 02 00 00 00 00 00 00  00 00 00 00 EC 16 01 00  @...........?...
    00000040  FF FF FF FF 00 00 00 00                           ????....

### The information stored in the sub header is as follows:

Address | Description
------- | -----------
  0x00  | `uint filePointer` - The pointer to the file in the AMB
  0x00  | `uint unk1` - Unknown
  0x00  | `uint unk2` - Unknown
  0x04  | `uint fileSize` - The total length of the file in bytes
  0x08  | `uint unkEditorData`? - Unknown Data (Dimps Internal Tool)
  0x0C  | `short USR0`? - User 1 Data (Dimps Internal Tool)
  0x0E  | `short USR1`? - User 2 Data (Dimps Internal Tool)

**¹** - Changing this value to a random one doesn't crash game.
