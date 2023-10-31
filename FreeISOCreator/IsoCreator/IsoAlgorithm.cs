// Decompiled with JetBrains decompiler
// Type: IsoCreator.IsoAlgorithm
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System;
using System.IO;
using System.Text;

namespace IsoCreator
{
  internal static class IsoAlgorithm
  {
    public static uint SectorSize => 2048;

    public static byte DefaultDirectoryRecordLength => 34;

    public static byte DefaultPathTableRecordLength => 10;

    public static byte AsciiBlank => 32;

    public static byte[] UnicodeBlank => new byte[2]
    {
      (byte) 0,
      IsoAlgorithm.AsciiBlank
    };

    public static int SystemIdLength => 32;

    public static int VolumeIdLength => 32;

    public static int VolumeSetIdLength => 128;

    public static int PublisherIdLength => 128;

    public static int PreparerIdLength => 128;

    public static int ApplicationIdLength => 128;

    public static int CopyrightFileIdLength => 37;

    public static int AbstractFileIdLength => 37;

    public static int BibliographicFileIdLength => 37;

    public static DateTime NoDate => new DateTime(1900, 1, 1, 0, 0, 0, 0);

    public static int FileNameMaxLength => 101;

    public static bool IsSubDir(string fileName) => !fileName.StartsWith(".");

    public static ulong BothEndian(uint value)
    {
      ulong num1 = 4278190080;
      ulong num2 = 16711680;
      ulong num3 = 65280;
      ulong maxValue = (ulong) byte.MaxValue;
      return (ulong) ((long) value | ((long) value & (long) num1) << 8 | ((long) value & (long) num2) << 24 | ((long) value & (long) num3) << 40 | ((long) value & (long) maxValue) << 56);
    }

    public static uint BothEndian(ushort value)
    {
      uint num = 65280;
      uint maxValue = (uint) byte.MaxValue;
      return (uint) ((int) value | ((int) value & (int) num) << 8 | ((int) value & (int) maxValue) << 24);
    }

    public static uint ValueFromBothEndian(ulong value) => (uint) (value & (ulong) uint.MaxValue);

    public static ushort ValueFromBothEndian(uint value) => (ushort) (value & (uint) ushort.MaxValue);

    public static byte[] MemSet(int count, byte value)
    {
      byte[] numArray = new byte[count];
      for (int index = 0; index < count; ++index)
        numArray[index] = value;
      return numArray;
    }

    public static byte[] MemSet(int count, byte[] value)
    {
      byte[] numArray = new byte[count * value.Length];
      for (int index = 0; index < count; ++index)
        value.CopyTo((Array) numArray, index * value.Length);
      return numArray;
    }

    public static byte[] AsciiToUnicode(string asciiText)
    {
      MemoryStream output = new MemoryStream();
      BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.BigEndianUnicode);
      binaryWriter.Write(asciiText);
      binaryWriter.Close();
      byte[] buffer = output.GetBuffer();
      byte[] unicode = new byte[asciiText.Length * 2];
      for (int index = 0; index < unicode.Length && index + 1 < buffer.Length; ++index)
        unicode[index] = buffer[index + 1];
      return unicode;
    }

    public static byte[] AsciiToUnicode(string asciiText, int size)
    {
      byte[] unicode = IsoAlgorithm.AsciiToUnicode(asciiText);
      byte[] array = IsoAlgorithm.MemSet(size / 2, IsoAlgorithm.UnicodeBlank);
      if (size % 2 == 1)
        Array.Resize<byte>(ref array, array.Length + 1);
      Array.Copy((Array) unicode, (Array) array, Math.Min(size, unicode.Length));
      if (unicode.Length < size - 2)
        array[unicode.Length] = array[unicode.Length + 1] = (byte) 0;
      return array;
    }

    public static byte[] AsciiToUnicode(byte[] asciiText)
    {
      MemoryStream output = new MemoryStream();
      BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.BigEndianUnicode);
      for (int index = 0; index < asciiText.Length; ++index)
        binaryWriter.Write((char) asciiText[index]);
      binaryWriter.Close();
      byte[] buffer = output.GetBuffer();
      byte[] destinationArray = new byte[asciiText.Length * 2];
      Array.Copy((Array) buffer, (Array) destinationArray, Math.Min(destinationArray.Length, buffer.Length));
      return destinationArray;
    }

    public static byte[] AsciiToUnicode(byte[] asciiText, int size)
    {
      byte[] unicode = IsoAlgorithm.AsciiToUnicode(asciiText);
      byte[] array = IsoAlgorithm.MemSet(size / 2, IsoAlgorithm.UnicodeBlank);
      if (size % 2 == 1)
        Array.Resize<byte>(ref array, array.Length + 1);
      Array.Copy((Array) unicode, (Array) array, Math.Min(array.Length, unicode.Length));
      return array;
    }

    public static byte[] UnicodeToAscii(byte[] unicodeText)
    {
      byte[] ascii = new byte[unicodeText.Length / 2];
      for (int index = 0; index < ascii.Length; ++index)
        ascii[index] = unicodeText[index * 2];
      return ascii;
    }

    public static byte[] UnicodeToAscii(byte[] unicodeText, int size)
    {
      byte[] ascii = IsoAlgorithm.MemSet(size, IsoAlgorithm.AsciiBlank);
      for (int index = 0; index < ascii.Length && index < unicodeText.Length / 2; ++index)
        ascii[index] = unicodeText[index * 2];
      return ascii;
    }

    public static byte[] StringToByteArray(string text)
    {
      byte[] byteArray = new byte[text.Length];
      for (int index = 0; index < byteArray.Length; ++index)
        byteArray[index] = (byte) text[index];
      return byteArray;
    }

    public static byte[] StringToByteArray(string text, int size)
    {
      byte[] byteArray = IsoAlgorithm.StringToByteArray(text);
      byte[] destinationArray = IsoAlgorithm.MemSet(size, IsoAlgorithm.AsciiBlank);
      Array.Copy((Array) byteArray, (Array) destinationArray, Math.Min(destinationArray.Length, byteArray.Length));
      return destinationArray;
    }

    public static string ByteArrayToString(byte[] array)
    {
      char[] chArray = new char[array.Length];
      for (int index = 0; index < chArray.Length; ++index)
        chArray[index] = (char) array[index];
      return new string(chArray);
    }

    public static void WriteEmptySector(BinaryWriter writer)
    {
      byte[] buffer = new byte[(int) IsoAlgorithm.SectorSize];
      writer.Write(buffer);
    }

    public static uint ChangeEndian(uint value)
    {
      uint num1 = 4278190080;
      uint num2 = 16711680;
      uint num3 = 65280;
      uint maxValue = (uint) byte.MaxValue;
      return (uint) ((int) ((value & num1) >> 24) | (int) ((value & num2) >> 8) | ((int) value & (int) num3) << 8 | ((int) value & (int) maxValue) << 24);
    }

    public static ushort ChangeEndian(ushort value) => (ushort) ((uint) value >> 8 | (uint) (ushort) (((int) value & (int) byte.MaxValue) << 8));
  }
}
