// Decompiled with JetBrains decompiler
// Type: IsoCreator.IsoWrappers.PathTableRecordWrapper
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using ISO9660.Enums;
using ISO9660.PrimitiveTypes;
using System;
using System.IO;

namespace IsoCreator.IsoWrappers
{
  internal class PathTableRecordWrapper
  {
    private PathTableRecord m_record = new PathTableRecord();
    private Endian m_endian;
    private VolumeType m_volumeDescriptorType = VolumeType.Primary;

    public PathTableRecordWrapper()
    {
    }

    public PathTableRecordWrapper(uint extentLocation, ushort parentNumber, string name) => this.SetPathTableRecord(extentLocation, parentNumber, name);

    public PathTableRecord Record
    {
      get => this.m_record;
      set => this.m_record = value;
    }

    public Endian Endian
    {
      get => this.m_endian;
      set
      {
        if (value != this.m_endian)
        {
          this.m_record.ExtentLocation = IsoAlgorithm.ChangeEndian(this.m_record.ExtentLocation);
          this.m_record.ParentNumber = IsoAlgorithm.ChangeEndian(this.m_record.ParentNumber);
        }
        this.m_endian = value;
      }
    }

    public VolumeType VolumeDescriptorType
    {
      get => this.m_volumeDescriptorType;
      set
      {
        if (this.m_record == null || this.m_record.Identifier.Length == 1 && this.m_record.Identifier[0] == (byte) 0)
        {
          this.m_volumeDescriptorType = value;
        }
        else
        {
          if (this.m_volumeDescriptorType != value && (this.m_volumeDescriptorType == VolumeType.Suplementary || value == VolumeType.Suplementary))
          {
            if (value == VolumeType.Suplementary)
            {
              this.m_record.Identifier = IsoAlgorithm.AsciiToUnicode(this.m_record.Identifier);
              this.m_record.Length = (byte) this.m_record.Identifier.Length;
              if (this.m_record.Identifier.Length > (int) byte.MaxValue)
                throw new Exception("Depasire!");
            }
            else
            {
              this.m_record.Identifier = IsoAlgorithm.UnicodeToAscii(this.m_record.Identifier);
              this.m_record.Length = (byte) this.m_record.Identifier.Length;
              if (this.m_record.Identifier.Length > (int) byte.MaxValue)
                throw new Exception("Depasire!");
            }
          }
          this.m_volumeDescriptorType = value;
        }
      }
    }

    public uint ExtentLocation
    {
      get => this.m_endian == Endian.BigEndian ? IsoAlgorithm.ChangeEndian(this.m_record.ExtentLocation) : this.m_record.ExtentLocation;
      set
      {
        if (this.m_endian == Endian.BigEndian)
          this.m_record.ExtentLocation = IsoAlgorithm.ChangeEndian(value);
        else
          this.m_record.ExtentLocation = value;
      }
    }

    public ushort ParentNumber
    {
      get => this.m_endian == Endian.BigEndian ? IsoAlgorithm.ChangeEndian(this.m_record.ParentNumber) : this.m_record.ParentNumber;
      set
      {
        if (this.m_endian == Endian.BigEndian)
          this.m_record.ParentNumber = IsoAlgorithm.ChangeEndian(value);
        else
          this.m_record.ParentNumber = value;
      }
    }

    public string Name
    {
      get => this.m_record.Identifier.Length == 1 && this.m_record.Identifier[0] == (byte) 0 ? "." : IsoAlgorithm.ByteArrayToString(this.m_record.Identifier);
      set
      {
        if (value == ".")
        {
          this.m_record.Identifier = new byte[1];
          this.m_record.Length = (byte) 1;
        }
        else
        {
          this.m_record.Identifier = IsoAlgorithm.StringToByteArray(value);
          this.m_record.Length = (byte) this.m_record.Identifier.Length;
          if (this.m_record.Identifier.Length > (int) byte.MaxValue)
            throw new Exception("Depasire!");
          if (this.m_volumeDescriptorType != VolumeType.Suplementary)
            return;
          this.m_volumeDescriptorType = VolumeType.Primary;
          this.VolumeDescriptorType = VolumeType.Suplementary;
        }
      }
    }

    public byte Length => this.m_record.Length;

    private void SetPathTableRecord(uint extentLocation, ushort parentNumber, byte[] identifier)
    {
      if (this.m_record == null)
        this.m_record = new PathTableRecord();
      this.m_record.Length = (byte) identifier.Length;
      this.m_record.Identifier = identifier.Length <= (int) byte.MaxValue ? identifier : throw new Exception("Depasire!");
      this.m_record.ExtentLocation = extentLocation;
      this.m_record.ParentNumber = parentNumber;
      if (this.m_volumeDescriptorType != VolumeType.Suplementary || identifier.Length <= 1 && identifier[0] == (byte) 0)
        return;
      this.m_volumeDescriptorType = VolumeType.Primary;
      this.VolumeDescriptorType = VolumeType.Suplementary;
    }

    public void SetPathTableRecord(uint extentLocation, ushort parentNumber, string name)
    {
      if (this.m_endian == Endian.BigEndian)
      {
        extentLocation = IsoAlgorithm.ChangeEndian(extentLocation);
        parentNumber = IsoAlgorithm.ChangeEndian(parentNumber);
      }
      byte[] identifier = !(name != ".") ? new byte[1] : IsoAlgorithm.StringToByteArray(name);
      this.SetPathTableRecord(extentLocation, parentNumber, identifier);
    }

    public int Write(BinaryWriter writer)
    {
      if (this.m_record == null)
      {
        this.m_record = new PathTableRecord();
        this.m_record.Length = (byte) 1;
        this.m_record.Identifier = new byte[1]{ (byte) 65 };
      }
      writer.Write(this.m_record.Length);
      writer.Write(this.m_record.ExtendedLength);
      writer.Write(this.m_record.ExtentLocation);
      writer.Write(this.m_record.ParentNumber);
      writer.Write(this.m_record.Identifier);
      if ((int) this.m_record.Length % 2 == 1)
        writer.Write((byte) 0);
      return 8 + (int) this.m_record.Length + (int) this.m_record.Length % 2;
    }
  }
}
