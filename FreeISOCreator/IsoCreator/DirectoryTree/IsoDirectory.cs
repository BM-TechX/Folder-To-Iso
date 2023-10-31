// Decompiled with JetBrains decompiler
// Type: IsoCreator.DirectoryTree.IsoDirectory
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using Export;
using ISO9660.Enums;
using IsoCreator.IsoWrappers;
using System.Collections;
using System.IO;

namespace IsoCreator.DirectoryTree
{
  internal class IsoDirectory : IsoFolderElement
  {
    private IsoDirectory m_parent;
    private ushort m_number;
    private uint m_level;
    private uint m_size1;
    private uint m_size2;
    private uint m_extent1;
    private uint m_extent2;
    private FolderElementList m_children = new FolderElementList();

    private void CalculateSize()
    {
      this.m_size1 = 1U;
      this.m_size2 = 1U;
      uint num1 = 2U * (uint) IsoAlgorithm.DefaultDirectoryRecordLength;
      uint num2 = 2U * (uint) IsoAlgorithm.DefaultDirectoryRecordLength;
      foreach (IsoFolderElement child in (CollectionBase) this.m_children)
      {
        uint num3 = !child.IsDirectory ? (uint) (child.ShortName.Length + 2 + (int) IsoAlgorithm.DefaultDirectoryRecordLength - 1) : (uint) (child.ShortName.Length + (int) IsoAlgorithm.DefaultDirectoryRecordLength - 1);
        if (num3 % 2U == 1U)
          ++num3;
        if (num1 + num3 > IsoAlgorithm.SectorSize)
        {
          ++this.m_size1;
          num1 = num3;
        }
        else
          num1 += num3;
        uint num4 = !child.IsDirectory ? (uint) (2 * (child.LongName.Length + 2) + (int) IsoAlgorithm.DefaultDirectoryRecordLength - 1) : (uint) (2 * child.LongName.Length + (int) IsoAlgorithm.DefaultDirectoryRecordLength - 1);
        if (num4 % 2U == 1U)
          ++num4;
        if (num2 + num4 > IsoAlgorithm.SectorSize)
        {
          ++this.m_size2;
          num2 = num4;
        }
        else
          num2 += num4;
      }
    }

    private void Initialize(DirectoryInfo directory, uint level, ProgressDelegate Progress)
    {
      this.m_level = level;
      FileSystemInfo[] fileSystemInfos = directory.GetFileSystemInfos();
      if (fileSystemInfos != null)
      {
        if (Progress != null)
          Progress((object) this, new ProgressEventArgs(0, fileSystemInfos.Length));
        int length = fileSystemInfos.Length.ToString().Length;
        for (int current = 0; current < fileSystemInfos.Length; ++current)
        {
          string childNumber = string.Format("{0:D" + length.ToString() + "}", (object) current);
          this.m_children.Add(!(fileSystemInfos[current].GetType() == typeof (DirectoryInfo)) ? (IsoFolderElement) new IsoFile((FileInfo) fileSystemInfos[current], childNumber) : (IsoFolderElement) new IsoDirectory(this, (DirectoryInfo) fileSystemInfos[current], level + 1U, childNumber));
          if (Progress != null)
            Progress((object) this, new ProgressEventArgs(current));
        }
      }
      this.m_children.Sort();
      this.CalculateSize();
    }

    public IsoDirectory(
      DirectoryInfo directory,
      uint level,
      string childNumber,
      ProgressDelegate Progress)
      : base((FileSystemInfo) directory, true, childNumber)
    {
      this.m_parent = this;
      this.Initialize(directory, level, Progress);
    }

    public IsoDirectory(
      IsoDirectory parent,
      DirectoryInfo directory,
      uint level,
      string childNumber)
      : base((FileSystemInfo) directory, false, childNumber)
    {
      this.m_parent = parent;
      this.Initialize(directory, level, (ProgressDelegate) null);
    }

    private void Initialize(TreeNode directory, uint level, ProgressDelegate Progress)
    {
      this.m_level = level;
      TreeNode[] allChildren = directory.GetAllChildren();
      if (allChildren != null)
      {
        if (Progress != null)
          Progress((object) this, new ProgressEventArgs(0, allChildren.Length));
        int length = allChildren.Length.ToString().Length;
        for (int current = 0; current < allChildren.Length; ++current)
        {
          string childNumber = string.Format("{0:D" + length.ToString() + "}", (object) current);
          this.m_children.Add(!allChildren[current].IsDirectory ? (IsoFolderElement) new IsoFile(allChildren[current], childNumber) : (IsoFolderElement) new IsoDirectory(this, allChildren[current], level + 1U, childNumber));
          if (Progress != null)
            Progress((object) this, new ProgressEventArgs(current));
        }
        this.m_children.Sort();
      }
      this.CalculateSize();
    }

    public IsoDirectory(
      TreeNode directory,
      uint level,
      string childNumber,
      ProgressDelegate Progress)
      : base(directory, true, childNumber)
    {
      this.m_parent = this;
      this.Initialize(directory, level, Progress);
    }

    public IsoDirectory(IsoDirectory parent, TreeNode directory, uint level, string childNumber)
      : base(directory, false, childNumber)
    {
      this.m_parent = parent;
      this.Initialize(directory, level, (ProgressDelegate) null);
    }

    public uint TotalDirSize
    {
      get
      {
        uint totalDirSize = (this.Size1 + this.Size2) / IsoAlgorithm.SectorSize;
        foreach (IsoFolderElement child in (CollectionBase) this.Children)
        {
          if (child.IsDirectory)
            totalDirSize += ((IsoDirectory) child).TotalDirSize;
        }
        return totalDirSize;
      }
    }

    public uint TotalSize
    {
      get
      {
        uint totalSize = (this.Size1 + this.Size2) / IsoAlgorithm.SectorSize;
        foreach (IsoFolderElement child in (CollectionBase) this.Children)
        {
          if (!child.IsDirectory)
          {
            totalSize += child.Size1 / IsoAlgorithm.SectorSize;
            if (child.Size1 % IsoAlgorithm.SectorSize != 0U)
              ++totalSize;
          }
          else
            totalSize += ((IsoDirectory) child).TotalSize;
        }
        return totalSize;
      }
    }

    public FolderElementList Children => this.m_children;

    public IsoDirectory Parent => this.m_parent;

    public uint Level => this.m_level;

    public ushort Number
    {
      get => this.m_number;
      set => this.m_number = value;
    }

    public override uint Extent1
    {
      get => this.m_extent1;
      set => this.m_extent1 = value;
    }

    public override uint Extent2
    {
      get => this.m_extent2;
      set => this.m_extent2 = value;
    }

    public override uint Size1 => this.m_size1 * IsoAlgorithm.SectorSize;

    public override uint Size2 => this.m_size2 * IsoAlgorithm.SectorSize;

    public override bool IsDirectory => true;

    public void WriteFiles(BinaryWriter writer, ProgressDelegate Progress)
    {
      foreach (IsoFolderElement child in (CollectionBase) this.Children)
      {
        if (!child.IsDirectory)
        {
          ((IsoFile) child).Write(writer, Progress);
          Progress((object) this, new ProgressEventArgs((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize)));
        }
      }
      foreach (IsoFolderElement child in (CollectionBase) this.Children)
      {
        if (child.IsDirectory)
        {
          ((IsoDirectory) child).WriteFiles(writer, Progress);
          Progress((object) this, new ProgressEventArgs((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize)));
        }
      }
    }

    public void Write(BinaryWriter writer, VolumeType type)
    {
      uint extentLocation1 = type == VolumeType.Primary ? this.Extent1 : this.Extent2;
      uint dataLength1 = type == VolumeType.Primary ? this.Size1 : this.Size2;
      uint extentLocation2 = type == VolumeType.Primary ? this.Parent.Extent1 : this.Parent.Extent2;
      uint dataLength2 = type == VolumeType.Primary ? this.Parent.Size1 : this.Parent.Size2;
      new DirectoryRecordWrapper(extentLocation1, dataLength1, this.Date, this.IsDirectory, ".").Write(writer);
      new DirectoryRecordWrapper(extentLocation2, dataLength2, this.Parent.Date, this.Parent.IsDirectory, "..").Write(writer);
      int num = 2 * (int) IsoAlgorithm.DefaultDirectoryRecordLength;
      foreach (IsoFolderElement child in (CollectionBase) this.m_children)
      {
        uint extentLocation3 = type == VolumeType.Primary ? child.Extent1 : child.Extent2;
        uint dataLength3 = type == VolumeType.Primary ? child.Size1 : child.Size2;
        string name = type == VolumeType.Primary ? child.ShortName : child.LongName;
        DirectoryRecordWrapper directoryRecordWrapper = new DirectoryRecordWrapper(extentLocation3, dataLength3, child.Date, child.IsDirectory, name);
        directoryRecordWrapper.VolumeDescriptorType = type;
        if ((long) ((int) directoryRecordWrapper.Length + num) > (long) IsoAlgorithm.SectorSize)
        {
          writer.Write(new byte[(long) IsoAlgorithm.SectorSize - (long) num]);
          num = 0;
        }
        num += directoryRecordWrapper.Write(writer);
      }
      writer.Write(new byte[(long) IsoAlgorithm.SectorSize - (long) num]);
    }

    public int WritePathTable(BinaryWriter writer, bool isRoot, Endian endian, VolumeType type)
    {
      uint extentLocation = type == VolumeType.Primary ? this.Extent1 : this.Extent2;
      string name = type == VolumeType.Primary ? this.ShortName : this.LongName;
      PathTableRecordWrapper tableRecordWrapper = !isRoot ? new PathTableRecordWrapper(extentLocation, this.Parent.Number, name) : new PathTableRecordWrapper(extentLocation, this.Parent.Number, ".");
      tableRecordWrapper.VolumeDescriptorType = type;
      tableRecordWrapper.Endian = endian;
      return tableRecordWrapper.Write(writer);
    }

    public void SetFilesExtent(ref uint currentExtent)
    {
      foreach (IsoFolderElement child in (CollectionBase) this.Children)
      {
        if (!child.IsDirectory)
        {
          if (child.Size1 == 0U)
          {
            child.Extent1 = 0U;
          }
          else
          {
            child.Extent1 = currentExtent;
            currentExtent += child.Size1 / IsoAlgorithm.SectorSize;
            if (child.Size1 % IsoAlgorithm.SectorSize != 0U)
              ++currentExtent;
          }
        }
      }
      foreach (IsoFolderElement child in (CollectionBase) this.Children)
      {
        if (child.IsDirectory)
          ((IsoDirectory) child).SetFilesExtent(ref currentExtent);
      }
    }

    public static void SetExtent1(ArrayList stack, int index, uint currentExtent)
    {
      if (index >= stack.Count)
        return;
      IsoDirectory isoDirectory = (IsoDirectory) stack[index];
      isoDirectory.Extent1 = currentExtent;
      uint currentExtent1 = currentExtent + isoDirectory.Size1 / IsoAlgorithm.SectorSize;
      foreach (IsoFolderElement child in (CollectionBase) isoDirectory.Children)
      {
        if (child.IsDirectory)
          stack.Add((object) child);
      }
      IsoDirectory.SetExtent1(stack, index + 1, currentExtent1);
    }

    public static void SetExtent2(ArrayList stack, int index, uint currentExtent)
    {
      if (index >= stack.Count)
        return;
      IsoDirectory isoDirectory = (IsoDirectory) stack[index];
      isoDirectory.Extent2 = currentExtent;
      uint currentExtent1 = currentExtent + isoDirectory.Size2 / IsoAlgorithm.SectorSize;
      foreach (IsoFolderElement child in (CollectionBase) isoDirectory.Children)
      {
        if (child.IsDirectory)
          stack.Add((object) child);
      }
      IsoDirectory.SetExtent2(stack, index + 1, currentExtent1);
    }
  }
}
