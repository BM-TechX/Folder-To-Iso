// Decompiled with JetBrains decompiler
// Type: ISO9660.PrimitiveTypes.VolumeDescriptor
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using IsoCreator;

namespace ISO9660.PrimitiveTypes
{
  internal class VolumeDescriptor
  {
    public byte VolumeDescType;
    public byte[] StandardIdentifier = new byte[6]
    {
      (byte) 67,
      (byte) 68,
      (byte) 48,
      (byte) 48,
      (byte) 49,
      (byte) 1
    };
    public byte Reserved1;
    public byte[] SystemId = IsoAlgorithm.MemSet(IsoAlgorithm.SystemIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] VolumeId = IsoAlgorithm.MemSet(IsoAlgorithm.VolumeIdLength, IsoAlgorithm.AsciiBlank);
    public ulong Reserved2;
    public ulong VolumeSpaceSize;
    public byte[] Reserved3_1 = new byte[3]
    {
      (byte) 37,
      (byte) 47,
      (byte) 69
    };
    public byte[] Reserved3_2 = new byte[29];
    public uint VolumeSetSize = 16777217;
    public uint VolumeSequenceNumber = 16777217;
    public uint SectorkSize = 526336;
    public ulong PathTableSize;
    public uint TypeLPathTable;
    public uint OptionalTypeLPathTable;
    public uint TypeMPathTable;
    public uint OptionalTypeMPathTable;
    public DirectoryRecord RootDirRecord = new DirectoryRecord();
    public byte[] VolumeSetId = IsoAlgorithm.MemSet(IsoAlgorithm.VolumeSetIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] PublisherId = IsoAlgorithm.MemSet(IsoAlgorithm.PublisherIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] PreparerId = IsoAlgorithm.MemSet(IsoAlgorithm.PreparerIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] ApplicationId = IsoAlgorithm.MemSet(IsoAlgorithm.ApplicationIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] CopyrightFileId = IsoAlgorithm.MemSet(IsoAlgorithm.CopyrightFileIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] AbstractFileId = IsoAlgorithm.MemSet(IsoAlgorithm.AbstractFileIdLength, IsoAlgorithm.AsciiBlank);
    public byte[] BibliographicFileId = IsoAlgorithm.MemSet(IsoAlgorithm.BibliographicFileIdLength, IsoAlgorithm.AsciiBlank);
    public AsciiDateRecord CreationDate = new AsciiDateRecord();
    public AsciiDateRecord ModificationDate = new AsciiDateRecord();
    public AsciiDateRecord ExpirationDate = new AsciiDateRecord();
    public AsciiDateRecord EffectiveDate = new AsciiDateRecord();
    public byte FileStructureVersion = 1;
    public byte Reserved4;
    public byte[] ApplicationData = new byte[512];
    public byte[] Reserved5 = new byte[653];
  }
}
