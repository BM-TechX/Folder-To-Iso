// Decompiled with JetBrains decompiler
// Type: IsoCreator.IsoWrappers.DirectoryRecordWrapper
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using ISO9660.Enums;
using ISO9660.PrimitiveTypes;
using System;
using System.IO;

namespace IsoCreator.IsoWrappers
{
  internal class DirectoryRecordWrapper
  {
    private DirectoryRecord m_record = new DirectoryRecord();
    private DateWrapper m_date;
    private VolumeType m_volumeDescriptorType = VolumeType.Primary;

    public uint ExtentLocation
    {
      get => IsoAlgorithm.ValueFromBothEndian(this.m_record.ExtentLocation);
      set => this.m_record.ExtentLocation = IsoAlgorithm.BothEndian(value);
    }

    public uint DataLength
    {
      get => IsoAlgorithm.ValueFromBothEndian(this.m_record.DataLength);
      set => this.m_record.DataLength = IsoAlgorithm.BothEndian(value);
    }

    public DateTime Date
    {
      get => this.m_date.Date;
      set
      {
        this.m_date = new DateWrapper(value);
        this.m_record.Date = this.m_date.BinaryDateRecord;
      }
    }

    public DirectoryRecord Record
    {
      get => this.m_record;
      set
      {
        this.m_record = value;
        this.m_date.BinaryDateRecord = this.m_record.Date;
      }
    }

    public VolumeType VolumeDescriptorType
    {
      get => this.m_volumeDescriptorType;
      set
      {
        if (this.m_record.FileIdentifier.Length == 1 && this.m_record.FileIdentifier[0] <= (byte) 1)
        {
          this.m_volumeDescriptorType = value;
        }
        else
        {
          if (this.m_volumeDescriptorType != value && (this.m_volumeDescriptorType == VolumeType.Suplementary || value == VolumeType.Suplementary) && this.m_record != null)
          {
            if (value == VolumeType.Suplementary)
            {
              this.m_record.FileIdentifier = IsoAlgorithm.AsciiToUnicode(this.m_record.FileIdentifier);
              this.m_record.LengthOfFileIdentifier = (byte) this.m_record.FileIdentifier.Length;
              this.m_record.Length = (byte) (33 + (int) this.m_record.LengthOfFileIdentifier + (1 - (int) this.m_record.LengthOfFileIdentifier % 2));
              if (33 + (int) this.m_record.LengthOfFileIdentifier + (1 - (int) this.m_record.LengthOfFileIdentifier % 2) > (int) byte.MaxValue)
                throw new Exception("Depasire!");
            }
            else
            {
              this.m_record.FileIdentifier = IsoAlgorithm.UnicodeToAscii(this.m_record.FileIdentifier);
              this.m_record.LengthOfFileIdentifier = (byte) this.m_record.FileIdentifier.Length;
              this.m_record.Length = (byte) (33 + (int) this.m_record.LengthOfFileIdentifier + (1 - (int) this.m_record.LengthOfFileIdentifier % 2));
              if (this.m_record.FileIdentifier.Length > (int) byte.MaxValue || 33 + (int) this.m_record.LengthOfFileIdentifier + (1 - (int) this.m_record.LengthOfFileIdentifier % 2) > (int) byte.MaxValue)
                throw new Exception("Depasire!");
            }
          }
          this.m_volumeDescriptorType = value;
        }
      }
    }

    public bool IsDirectory => ((int) this.m_record.FileFlags & 2) != 0;

    public string Name => IsoAlgorithm.ByteArrayToString(this.m_record.FileIdentifier);

    public byte Length => this.m_record.Length;

    public DirectoryRecordWrapper(DateTime date, bool isDirectory, string name) => this.SetDirectoryRecord(0U, 0U, date, isDirectory, name);

    public DirectoryRecordWrapper(
      uint extentLocation,
      uint dataLength,
      DateTime date,
      bool isDirectory,
      string name)
    {
      this.SetDirectoryRecord(extentLocation, dataLength, date, isDirectory, name);
    }

    public DirectoryRecordWrapper(DirectoryRecord directoryRecord)
    {
      this.m_record = directoryRecord;
      this.m_date = new DateWrapper(directoryRecord.Date);
    }

    private void SetDirectoryRecord(
      ulong extentLocation,
      ulong dataLength,
      BinaryDateRecord date,
      sbyte timeZone,
      byte fileFlags,
      byte[] fileIdentifier)
    {
      if (this.m_record == null)
        this.m_record = new DirectoryRecord();
      this.m_record.ExtentLocation = extentLocation;
      this.m_record.DataLength = dataLength;
      this.m_record.Date = date;
      this.m_record.TimeZone = timeZone;
      this.m_record.FileFlags = fileFlags;
      this.m_record.LengthOfFileIdentifier = (byte) fileIdentifier.Length;
      this.m_record.FileIdentifier = fileIdentifier;
      this.m_record.Length = (byte) ((uint) this.m_record.LengthOfFileIdentifier + 33U);
      if ((int) this.m_record.Length % 2 == 1)
        ++this.m_record.Length;
      if (fileIdentifier.Length > (int) byte.MaxValue || (int) this.m_record.LengthOfFileIdentifier + 33 > (int) byte.MaxValue)
        throw new Exception("Depasire!");
      if (this.m_volumeDescriptorType != VolumeType.Suplementary || ((int) fileFlags & 2) != 0 && fileIdentifier.Length == 1 && fileIdentifier[0] <= (byte) 1)
        return;
      this.m_volumeDescriptorType = VolumeType.Primary;
      this.VolumeDescriptorType = VolumeType.Suplementary;
    }

    private void SetDirectoryRecord(
      uint extentLocation,
      uint dataLength,
      DateTime date,
      sbyte timeZone,
      bool isDirectory,
      string name)
    {
      this.m_date = new DateWrapper(date);
      byte fileFlags = isDirectory ? (byte) 2 : (byte) 0;
      byte[] fileIdentifier;
      switch (name)
      {
        case ".":
          fileIdentifier = new byte[1];
          break;
        case "..":
          fileIdentifier = new byte[1]{ (byte) 1 };
          break;
        default:
          fileIdentifier = !isDirectory ? IsoAlgorithm.StringToByteArray(name + ";1") : IsoAlgorithm.StringToByteArray(name);
          break;
      }
      this.SetDirectoryRecord(IsoAlgorithm.BothEndian(extentLocation), IsoAlgorithm.BothEndian(dataLength), this.m_date.BinaryDateRecord, timeZone, fileFlags, fileIdentifier);
    }

    public void SetDirectoryRecord(
      uint extentLocation,
      uint dataLength,
      DateTime date,
      bool isDirectory,
      string name)
    {
      this.SetDirectoryRecord(extentLocation, dataLength, date, (sbyte) 8, isDirectory, name);
    }

    public int Write(BinaryWriter writer)
    {
      if (this.m_record == null)
        return 0;
      writer.Write(this.m_record.Length);
      writer.Write(this.m_record.ExtendedAttributeLength);
      writer.Write(this.m_record.ExtentLocation);
      writer.Write(this.m_record.DataLength);
      this.m_date.WriteBinaryDateRecord(writer);
      writer.Write(this.m_record.TimeZone);
      writer.Write(this.m_record.FileFlags);
      writer.Write(this.m_record.FileUnitSize);
      writer.Write(this.m_record.InterleaveGapSize);
      writer.Write(this.m_record.VolumeSequnceNumber);
      writer.Write(this.m_record.LengthOfFileIdentifier);
      writer.Write(this.m_record.FileIdentifier);
      if ((int) this.m_record.LengthOfFileIdentifier % 2 == 0)
        writer.Write((byte) 0);
      return (int) this.m_record.Length;
    }
  }
}
