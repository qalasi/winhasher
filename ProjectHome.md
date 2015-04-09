# Project Status #

| **Current Release** | 1.6.2.67 |
|:--------------------|:---------|
| **Date of Release** | August 6, 2013 |
| **Next Release Milestone** | 1.7.0 |
| **Date of Next Release** | Fall/Winter 2013 |
| **Status of Development** | Sporadic |

# Download WinHasher #

Google Code has disabled the ability to host downloadable files for projects hosted here. The following links point to the latest version of the WinHasher download files hosted on Google Drive. The GnuPG signature and SHA-1 hash for each file can also be found below. Jeff's current GnuPG signature can be found [here](https://www.gpf-comics.com/gnupg.php).

| **Download Type** | **GnuPG Signature** | **SHA-1 Hash** | **Size**|
|:------------------|:--------------------|:---------------|:|
| [Windows Installer](https://drive.google.com/uc?export=download&id=0B55ltyq5FildTlVHWjBRSnAxaUE) | [Signature](https://drive.google.com/uc?export=download&id=0B55ltyq5FildRUVKNVRiRWtNcTQ) | 6620fae809b7fecb6dc18335065199d4a479cd88 | 389kb |
| [Binaries w/o Installer](https://drive.google.com/uc?export=download&id=0B55ltyq5FilddFFOaEktLVpFbWc) | [Signature](https://drive.google.com/uc?export=download&id=0B55ltyq5Fildcm9vX3BCT25NTHc) | f5bc900380e3fb68f16f73e1dad4b867e3a34086 | 91kb |
| [Source Archive](https://drive.google.com/uc?export=download&id=0B55ltyq5FildR2FrVHBqOW82bkk) | [Signature](https://drive.google.com/uc?export=download&id=0B55ltyq5FildVXhvOVM1c2lwU2c) | a8d7ff8f65415635900c418252f38191ae5da8b4 | 142kb |

<a href='Hidden comment: 
How to make download links (from http://productforums.google.com/forum/#!topic/drive/5p_KUmaTfJE):

1) Go to Jeff"s personal Google Drive folders and go to Project Downloads, then WinHasher.
2) Right-click the file to download and choose Share, then Share again.
3) Get the Link to Share URL and copy it. Click Done to close the dialog.
4) Isolate the ID part of the URL and paste it onto the end of this URL: https://drive.google.com/uc?export=download&id=
5) Use this link for the download links on the page.
'></a>

# About WinHasher #

WinHasher is a free, Open Source cryptographic hash or digest generator written in C# using Microsoft's .NET 2.0 Framework. It can be used to verify file download integrity, compare two or more files for modifications, and to some degree generate strong, unique passwords. (For password generation, we recommend our derivative project, [Cryptnos for Windows](https://code.google.com/p/cryptnos-for-windows/) and [Android](https://code.google.com/p/cryptnos-for-android/).)

[Cryptographic hashing](http://en.wikipedia.org/wiki/Cryptographic_hash_function) is readily available on many computer operating systems. It often comes built-in to the OS or as a (relatively) standard optional package. Mac OS, Linux, Free/OpenBSD, and many other OSes include [OpenSSL](http://www.openssl.org/) as either a pre-installed or easily installable optional component. OpenSSL includes several command-line components for generating cryptographic hashes and there are number of graphical user interface (GUI) applications that allow point-and-click access to its capabilities.

Not so with Microsoft Windows. Windows does not include any built-in utilities for cryptographic hashes, and installing and using OpenSSL on Windows is not a trivial matter. The typical Windows user of today is much less familiar with the Windows Console (i.e. command line) let alone compiling software from source. And while cryptographic hashes are pretty much standard in programming libraries such as the Microsoft .NET Framework, the user is required to write and compile their own applications to use them.

This "hashing divide" has annoyed me for some time. While I consider myself to be an operating system agnostic and find myself equally home on both Windows and Linux, there are many times I've downloaded Windows-only software but didn't have the capability to verify the file's hash. Either I've been unable to install and run OpenSSL on a given machine, or I haven't had the time or access to a Linux box to copy the file over, generate the hash, and verify it before install. So I wanted to create a quick, simple, easy-to-use Windows app so I could get the hash of a file without waiting or moving it around. I also thought it would be a nice idea to be able to quickly compare the hashes of multiple files without having to generate each one and manually check every hexadecimal digit, so I added that functionality too. After writing the program, I thought it might be useful to others, so I decided to share.