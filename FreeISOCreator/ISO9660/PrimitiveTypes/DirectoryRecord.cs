// Decompiled with JetBrains decompiler
// Type: ISO9660.PrimitiveTypes.DirectoryRecord
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using IsoCreator;

namespace ISO9660.PrimitiveTypes
{
  internal class DirectoryRecord
  {
    public byte Length = IsoAlgorithm.DefaultDirectoryRecordLength;
    public byte ExtendedAttributeLength;
    public ulong ExtentLocation;
    public ulong DataLength;
    public BinaryDateRecord Date = new BinaryDateRecord();
    public sbyte TimeZone;
    public byte FileFlags;
    public byte FileUnitSize;
    public byte InterleaveGapSize;
    public uint VolumeSequnceNumber = 16777217;
    public byte LengthOfFileIdentifier;
    public byte[] FileIdentifier;
  }
}
