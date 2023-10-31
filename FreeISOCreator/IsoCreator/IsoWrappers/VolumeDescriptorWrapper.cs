// Decompiled with JetBrains decompiler
// Type: IsoCreator.IsoWrappers.VolumeDescriptorWrapper
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using ISO9660.Enums;
using ISO9660.PrimitiveTypes;
using System;
using System.IO;

namespace IsoCreator.IsoWrappers
{
  internal class VolumeDescriptorWrapper
  {
    private VolumeDescriptor m_volumeDescriptor = new VolumeDescriptor();
    private VolumeType m_volumeDescriptorType = VolumeType.Primary;
    private DirectoryRecordWrapper m_rootDirRecord;
    private DateWrapper m_creationDate;
    private DateWrapper m_modificationDate;
    private DateWrapper m_expirationDate;
    private DateWrapper m_effectiveDate;

    public VolumeDescriptorWrapper(
      string volumeName,
      DirectoryRecordWrapper root,
      DateTime creationDate,
      DateTime modificationDate)
    {
      this.SetVolumeDescriptor(volumeName, 0U, 0U, 0U, 0U, root, creationDate, modificationDate, (sbyte) 8);
    }

    public VolumeDescriptorWrapper(
      string volumeName,
      uint volumeSpaceSize,
      uint pathTableSize,
      uint typeLPathTable,
      uint typeMPathTable,
      DirectoryRecordWrapper root,
      DateTime creationDate,
      DateTime modificationDate,
      sbyte timeZone)
    {
      this.SetVolumeDescriptor(volumeName, volumeSpaceSize, pathTableSize, typeLPathTable, typeMPathTable, root, creationDate, modificationDate, timeZone);
    }

    public VolumeDescriptor VolumeDescriptor
    {
      get => this.m_volumeDescriptor;
      set
      {
        this.m_volumeDescriptor = value;
        this.m_rootDirRecord = new DirectoryRecordWrapper(this.m_volumeDescriptor.RootDirRecord);
        this.m_creationDate = new DateWrapper(this.m_volumeDescriptor.CreationDate);
        this.m_modificationDate = new DateWrapper(this.m_volumeDescriptor.ModificationDate);
        this.m_expirationDate = new DateWrapper(this.m_volumeDescriptor.ExpirationDate);
        this.m_effectiveDate = new DateWrapper(this.m_volumeDescriptor.EffectiveDate);
      }
    }

    public VolumeType VolumeDescriptorType
    {
      get => this.m_volumeDescriptorType;
      set
      {
        if (this.m_volumeDescriptorType != value && (this.m_volumeDescriptorType == VolumeType.Suplementary || value == VolumeType.Suplementary) && this.m_volumeDescriptor != null)
        {
          if (value == VolumeType.Suplementary)
          {
            this.m_volumeDescriptor.SystemId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.SystemId, IsoAlgorithm.SystemIdLength);
            this.m_volumeDescriptor.VolumeId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.VolumeId, IsoAlgorithm.VolumeIdLength);
            this.m_volumeDescriptor.VolumeSetId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.VolumeSetId, IsoAlgorithm.VolumeSetIdLength);
            this.m_volumeDescriptor.PublisherId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.PublisherId, IsoAlgorithm.PublisherIdLength);
            this.m_volumeDescriptor.PreparerId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.PreparerId, IsoAlgorithm.PreparerIdLength);
            this.m_volumeDescriptor.ApplicationId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.ApplicationId, IsoAlgorithm.ApplicationIdLength);
            this.m_volumeDescriptor.CopyrightFileId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.CopyrightFileId, IsoAlgorithm.CopyrightFileIdLength);
            this.m_volumeDescriptor.AbstractFileId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.AbstractFileId, IsoAlgorithm.AbstractFileIdLength);
            this.m_volumeDescriptor.BibliographicFileId = IsoAlgorithm.AsciiToUnicode(this.m_volumeDescriptor.BibliographicFileId, IsoAlgorithm.BibliographicFileIdLength);
          }
          else
          {
            this.m_volumeDescriptor.SystemId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.SystemId, IsoAlgorithm.SystemIdLength);
            this.m_volumeDescriptor.VolumeId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.VolumeId, IsoAlgorithm.VolumeIdLength);
            this.m_volumeDescriptor.VolumeSetId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.VolumeSetId, IsoAlgorithm.VolumeSetIdLength);
            this.m_volumeDescriptor.PublisherId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.PublisherId, IsoAlgorithm.PublisherIdLength);
            this.m_volumeDescriptor.PreparerId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.PreparerId, IsoAlgorithm.PreparerIdLength);
            this.m_volumeDescriptor.ApplicationId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.ApplicationId, IsoAlgorithm.ApplicationIdLength);
            this.m_volumeDescriptor.CopyrightFileId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.CopyrightFileId, IsoAlgorithm.CopyrightFileIdLength);
            this.m_volumeDescriptor.AbstractFileId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.AbstractFileId, IsoAlgorithm.AbstractFileIdLength);
            this.m_volumeDescriptor.BibliographicFileId = IsoAlgorithm.UnicodeToAscii(this.m_volumeDescriptor.BibliographicFileId, IsoAlgorithm.BibliographicFileIdLength);
          }
        }
        this.m_volumeDescriptorType = value;
        if (this.m_volumeDescriptor == null)
          return;
        this.m_volumeDescriptor.VolumeDescType = (byte) value;
      }
    }

    public uint VolumeSpaceSize
    {
      get => IsoAlgorithm.ValueFromBothEndian(this.m_volumeDescriptor.VolumeSpaceSize);
      set => this.m_volumeDescriptor.VolumeSpaceSize = IsoAlgorithm.BothEndian(value);
    }

    public uint PathTableSize
    {
      get => IsoAlgorithm.ValueFromBothEndian(this.m_volumeDescriptor.PathTableSize);
      set => this.m_volumeDescriptor.PathTableSize = IsoAlgorithm.BothEndian(value);
    }

    public uint TypeLPathTable
    {
      get => this.m_volumeDescriptor.TypeLPathTable;
      set => this.m_volumeDescriptor.TypeLPathTable = value;
    }

    public uint TypeMPathTable
    {
      get => IsoAlgorithm.ChangeEndian(this.m_volumeDescriptor.TypeMPathTable);
      set => this.m_volumeDescriptor.TypeMPathTable = IsoAlgorithm.ChangeEndian(value);
    }

    private void SetVolumeDescriptor(
      byte[] systemId,
      byte[] volumeId,
      ulong volumeSpaceSize,
      ulong pathTableSize,
      uint typeLPathTable,
      uint typeMPathTable,
      DirectoryRecord rootDirRecord,
      AsciiDateRecord creationDate,
      AsciiDateRecord modificationDate,
      AsciiDateRecord expirationDate,
      AsciiDateRecord effectiveDate)
    {
      if (this.m_volumeDescriptor == null)
        this.m_volumeDescriptor = new VolumeDescriptor();
      this.m_volumeDescriptor.VolumeDescType = (byte) this.m_volumeDescriptorType;
      systemId.CopyTo((Array) this.m_volumeDescriptor.SystemId, 0);
      volumeId.CopyTo((Array) this.m_volumeDescriptor.VolumeId, 0);
      this.m_volumeDescriptor.VolumeSpaceSize = volumeSpaceSize;
      this.m_volumeDescriptor.PathTableSize = pathTableSize;
      this.m_volumeDescriptor.TypeLPathTable = typeLPathTable;
      this.m_volumeDescriptor.TypeMPathTable = typeMPathTable;
      this.m_volumeDescriptor.RootDirRecord = rootDirRecord;
      this.m_rootDirRecord = new DirectoryRecordWrapper(rootDirRecord);
      this.m_volumeDescriptor.CreationDate = creationDate;
      this.m_creationDate = new DateWrapper(creationDate);
      this.m_volumeDescriptor.ModificationDate = modificationDate;
      this.m_modificationDate = new DateWrapper(modificationDate);
      this.m_volumeDescriptor.ExpirationDate = expirationDate;
      this.m_expirationDate = new DateWrapper(expirationDate);
      this.m_volumeDescriptor.EffectiveDate = effectiveDate;
      this.m_effectiveDate = new DateWrapper(effectiveDate);
    }

    private void SetVolumeDescriptor(
      byte[] systemId,
      byte[] volumeId,
      ulong volumeSpaceSize,
      ulong pathTableSize,
      uint typeLPathTable,
      uint typeMPathTable,
      DirectoryRecord rootDirRecord,
      byte[] volumeSetId,
      byte[] publisherId,
      byte[] preparerId,
      byte[] applicationId,
      byte[] copyrightFileId,
      byte[] abstractFileId,
      byte[] bibliographicFieldId,
      AsciiDateRecord creationDate,
      AsciiDateRecord modificationDate,
      AsciiDateRecord expirationDate,
      AsciiDateRecord effectiveDate)
    {
      if (this.m_volumeDescriptor == null)
        this.m_volumeDescriptor = new VolumeDescriptor();
      volumeSetId.CopyTo((Array) this.m_volumeDescriptor.VolumeSetId, 0);
      publisherId.CopyTo((Array) this.m_volumeDescriptor.PublisherId, 0);
      preparerId.CopyTo((Array) this.m_volumeDescriptor.PreparerId, 0);
      applicationId.CopyTo((Array) this.m_volumeDescriptor.ApplicationId, 0);
      copyrightFileId.CopyTo((Array) this.m_volumeDescriptor.CopyrightFileId, 0);
      abstractFileId.CopyTo((Array) this.m_volumeDescriptor.AbstractFileId, 0);
      bibliographicFieldId.CopyTo((Array) this.m_volumeDescriptor.BibliographicFileId, 0);
      this.SetVolumeDescriptor(systemId, volumeId, volumeSpaceSize, pathTableSize, typeLPathTable, typeMPathTable, rootDirRecord, creationDate, modificationDate, expirationDate, effectiveDate);
    }

    private void SetVolumeDescriptor(
      string systemId,
      string volumeId,
      uint volumeSpaceSize,
      uint pathTableSize,
      uint typeLPathTable,
      uint typeMPathTable,
      DirectoryRecordWrapper rootDir,
      DateTime creationDate,
      DateTime modificationDate,
      sbyte timeZone)
    {
      byte[] systemId1;
      byte[] volumeId1;
      if (this.VolumeDescriptorType == VolumeType.Primary)
      {
        systemId1 = IsoAlgorithm.StringToByteArray(systemId, IsoAlgorithm.SystemIdLength);
        volumeId1 = IsoAlgorithm.StringToByteArray(volumeId, IsoAlgorithm.VolumeIdLength);
      }
      else if (this.VolumeDescriptorType == VolumeType.Suplementary)
      {
        systemId1 = IsoAlgorithm.AsciiToUnicode(systemId, IsoAlgorithm.SystemIdLength);
        volumeId1 = IsoAlgorithm.AsciiToUnicode(volumeId, IsoAlgorithm.VolumeIdLength);
      }
      else
      {
        if (this.m_volumeDescriptor == null)
          this.m_volumeDescriptor = new VolumeDescriptor();
        this.m_volumeDescriptor.VolumeDescType = (byte) this.m_volumeDescriptorType;
        return;
      }
      ulong volumeSpaceSize1 = IsoAlgorithm.BothEndian(volumeSpaceSize);
      ulong pathTableSize1 = IsoAlgorithm.BothEndian(pathTableSize);
      uint typeMPathTable1 = IsoAlgorithm.ChangeEndian(typeMPathTable);
      DateWrapper dateWrapper1 = new DateWrapper(creationDate, timeZone);
      DateWrapper dateWrapper2 = new DateWrapper(modificationDate, timeZone);
      DateWrapper dateWrapper3 = new DateWrapper(IsoAlgorithm.NoDate);
      this.SetVolumeDescriptor(systemId1, volumeId1, volumeSpaceSize1, pathTableSize1, typeLPathTable, typeMPathTable1, rootDir.Record, dateWrapper1.AsciiDateRecord, dateWrapper2.AsciiDateRecord, dateWrapper3.AsciiDateRecord, dateWrapper3.AsciiDateRecord);
    }

    public void SetVolumeDescriptor(
      string volumeName,
      uint volumeSpaceSize,
      uint pathTableSize,
      uint typeLPathTable,
      uint typeMPathTable,
      DirectoryRecordWrapper root,
      DateTime creationDate,
      DateTime modificationDate,
      sbyte timeZone)
    {
      this.SetVolumeDescriptor(" ", volumeName, volumeSpaceSize, pathTableSize, typeLPathTable, typeMPathTable, root, creationDate, modificationDate, timeZone);
    }

    public int Write(BinaryWriter writer)
    {
      if (this.m_volumeDescriptor == null)
        return 0;
      writer.Write(this.m_volumeDescriptor.VolumeDescType);
      writer.Write(this.m_volumeDescriptor.StandardIdentifier);
      if (this.VolumeDescriptorType == VolumeType.SetTerminator)
      {
        writer.Write(new byte[(int) (IsoAlgorithm.SectorSize - 7U)]);
        return (int) IsoAlgorithm.SectorSize;
      }
      writer.Write(this.m_volumeDescriptor.Reserved1);
      writer.Write(this.m_volumeDescriptor.SystemId);
      writer.Write(this.m_volumeDescriptor.VolumeId);
      writer.Write(this.m_volumeDescriptor.Reserved2);
      writer.Write(this.m_volumeDescriptor.VolumeSpaceSize);
      if (this.m_volumeDescriptorType == VolumeType.Suplementary)
        writer.Write(this.m_volumeDescriptor.Reserved3_1);
      else
        writer.Write(new byte[3]);
      writer.Write(this.m_volumeDescriptor.Reserved3_2);
      writer.Write(this.m_volumeDescriptor.VolumeSetSize);
      writer.Write(this.m_volumeDescriptor.VolumeSequenceNumber);
      writer.Write(this.m_volumeDescriptor.SectorkSize);
      writer.Write(this.m_volumeDescriptor.PathTableSize);
      writer.Write(this.m_volumeDescriptor.TypeLPathTable);
      writer.Write(this.m_volumeDescriptor.OptionalTypeLPathTable);
      writer.Write(this.m_volumeDescriptor.TypeMPathTable);
      writer.Write(this.m_volumeDescriptor.OptionalTypeMPathTable);
      this.m_rootDirRecord.VolumeDescriptorType = this.VolumeDescriptorType;
      this.m_rootDirRecord.Write(writer);
      writer.Write(this.m_volumeDescriptor.VolumeSetId);
      writer.Write(this.m_volumeDescriptor.PublisherId);
      writer.Write(this.m_volumeDescriptor.PreparerId);
      writer.Write(this.m_volumeDescriptor.ApplicationId);
      writer.Write(this.m_volumeDescriptor.CopyrightFileId);
      writer.Write(this.m_volumeDescriptor.AbstractFileId);
      writer.Write(this.m_volumeDescriptor.BibliographicFileId);
      this.m_creationDate.WriteAsciiDateRecord(writer);
      this.m_modificationDate.WriteAsciiDateRecord(writer);
      this.m_expirationDate.WriteAsciiDateRecord(writer);
      this.m_effectiveDate.WriteAsciiDateRecord(writer);
      writer.Write(this.m_volumeDescriptor.FileStructureVersion);
      writer.Write(this.m_volumeDescriptor.Reserved4);
      writer.Write(this.m_volumeDescriptor.ApplicationData);
      writer.Write(this.m_volumeDescriptor.Reserved5);
      return (int) IsoAlgorithm.SectorSize;
    }
  }
}
