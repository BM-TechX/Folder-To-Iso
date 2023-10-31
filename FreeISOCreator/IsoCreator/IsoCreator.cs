// Decompiled with JetBrains decompiler
// Type: IsoCreator.IsoCreator
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using Export;
using ISO9660.Enums;
using IsoCreator.DirectoryTree;
using IsoCreator.IsoWrappers;
using System;
using System.Collections;
using System.IO;
using System.Threading;

namespace IsoCreator
{
  public class IsoCreator
  {
    public void WriteDemoImage(BinaryWriter writer)
    {
      for (int index = 0; index < 16; ++index)
        IsoAlgorithm.WriteEmptySector(writer);
      DirectoryRecordWrapper root = new DirectoryRecordWrapper(19U, IsoAlgorithm.SectorSize, DateTime.Now, true, ".");
      VolumeDescriptorWrapper descriptorWrapper = new VolumeDescriptorWrapper("EPURASU", 28U, 26U, 21U, 22U, root, DateTime.Now, DateTime.Now.Subtract(TimeSpan.FromDays(2.0)), (sbyte) 8);
      descriptorWrapper.VolumeDescriptorType = VolumeType.Primary;
      descriptorWrapper.Write(writer);
      root.SetDirectoryRecord(23U, IsoAlgorithm.SectorSize, DateTime.Now, true, ".");
      descriptorWrapper.VolumeDescriptorType = VolumeType.Suplementary;
      descriptorWrapper.SetVolumeDescriptor("Epurasu", 28U, 38U, 25U, 26U, root, DateTime.Now, DateTime.Now.Subtract(TimeSpan.FromDays(2.0)), (sbyte) 8);
      descriptorWrapper.Write(writer);
      descriptorWrapper.VolumeDescriptorType = VolumeType.SetTerminator;
      descriptorWrapper.Write(writer);
      root.SetDirectoryRecord(19U, IsoAlgorithm.SectorSize, DateTime.Now, true, ".");
      root.Write(writer);
      root.SetDirectoryRecord(19U, IsoAlgorithm.SectorSize, DateTime.Now, true, "..");
      root.Write(writer);
      DirectoryRecordWrapper directoryRecordWrapper1 = new DirectoryRecordWrapper(20U, IsoAlgorithm.SectorSize, DateTime.Now, true, "DIRECTOR");
      int num1 = directoryRecordWrapper1.Write(writer);
      writer.Write(new byte[1980 - num1]);
      directoryRecordWrapper1.SetDirectoryRecord(20U, IsoAlgorithm.SectorSize, DateTime.Now, true, ".");
      directoryRecordWrapper1.Write(writer);
      directoryRecordWrapper1.SetDirectoryRecord(19U, IsoAlgorithm.SectorSize, DateTime.Now, true, "..");
      directoryRecordWrapper1.Write(writer);
      directoryRecordWrapper1.SetDirectoryRecord(27U, 45U, DateTime.Now, false, "NUMELEFI.TXT");
      int num2 = directoryRecordWrapper1.Write(writer);
      writer.Write(new byte[1980 - num2]);
      PathTableRecordWrapper tableRecordWrapper1 = new PathTableRecordWrapper();
      tableRecordWrapper1.Endian = Endian.LittleEndian;
      tableRecordWrapper1.SetPathTableRecord(19U, (ushort) 1, ".");
      int num3 = tableRecordWrapper1.Write(writer);
      tableRecordWrapper1.SetPathTableRecord(20U, (ushort) 1, "DIRECTOR");
      int num4 = num3 + tableRecordWrapper1.Write(writer);
      writer.Write(new byte[2048 - num4]);
      PathTableRecordWrapper tableRecordWrapper2 = new PathTableRecordWrapper();
      tableRecordWrapper2.Endian = Endian.BigEndian;
      tableRecordWrapper2.SetPathTableRecord(19U, (ushort) 1, ".");
      tableRecordWrapper2.Write(writer);
      tableRecordWrapper2.SetPathTableRecord(20U, (ushort) 1, "DIRECTOR");
      tableRecordWrapper2.Write(writer);
      writer.Write(new byte[2048 - num4]);
      root.VolumeDescriptorType = VolumeType.Suplementary;
      root.SetDirectoryRecord(23U, IsoAlgorithm.SectorSize, DateTime.Now, true, ".");
      root.Write(writer);
      root.SetDirectoryRecord(23U, IsoAlgorithm.SectorSize, DateTime.Now, true, "..");
      root.Write(writer);
      DirectoryRecordWrapper directoryRecordWrapper2 = new DirectoryRecordWrapper(24U, IsoAlgorithm.SectorSize, DateTime.Now, true, "Directorul");
      directoryRecordWrapper2.VolumeDescriptorType = VolumeType.Suplementary;
      int num5 = directoryRecordWrapper2.Write(writer);
      writer.Write(new byte[1980 - num5]);
      directoryRecordWrapper2.SetDirectoryRecord(24U, IsoAlgorithm.SectorSize, DateTime.Now, true, ".");
      directoryRecordWrapper2.Write(writer);
      directoryRecordWrapper2.SetDirectoryRecord(23U, IsoAlgorithm.SectorSize, DateTime.Now, true, "..");
      directoryRecordWrapper2.Write(writer);
      directoryRecordWrapper2.SetDirectoryRecord(27U, 45U, DateTime.Now, false, "numeleFisierului.txt");
      int num6 = directoryRecordWrapper2.Write(writer);
      writer.Write(new byte[1980 - num6]);
      PathTableRecordWrapper tableRecordWrapper3 = new PathTableRecordWrapper();
      tableRecordWrapper3.Endian = Endian.LittleEndian;
      tableRecordWrapper3.VolumeDescriptorType = VolumeType.Suplementary;
      tableRecordWrapper3.SetPathTableRecord(23U, (ushort) 1, ".");
      int num7 = tableRecordWrapper3.Write(writer);
      tableRecordWrapper3.SetPathTableRecord(24U, (ushort) 1, "Directorul");
      int num8 = num7 + tableRecordWrapper3.Write(writer);
      writer.Write(new byte[2048 - num8]);
      PathTableRecordWrapper tableRecordWrapper4 = new PathTableRecordWrapper();
      tableRecordWrapper4.Endian = Endian.BigEndian;
      tableRecordWrapper4.VolumeDescriptorType = VolumeType.Suplementary;
      tableRecordWrapper4.SetPathTableRecord(23U, (ushort) 1, ".");
      tableRecordWrapper4.Write(writer);
      tableRecordWrapper4.SetPathTableRecord(24U, (ushort) 1, "Directorul");
      tableRecordWrapper4.Write(writer);
      writer.Write(new byte[2048 - num8]);
      writer.Write(IsoAlgorithm.StringToByteArray("Catelus cu parul cretz fura ratza din cotetz."));
      writer.Write(new byte[2003]);
    }

    private void SetDirectoryNumbers(IsoDirectory[] dirArray)
    {
      if (dirArray == null)
        return;
      for (int index = 0; index < dirArray.Length; ++index)
        dirArray[index].Number = (ushort) (index + 1);
    }

    private void WriteFirst16EmptySectors(BinaryWriter writer)
    {
      for (int index = 0; index < 16; ++index)
        writer.Write(new byte[(int) IsoAlgorithm.SectorSize]);
    }

    private void WriteVolumeDescriptors(
      BinaryWriter writer,
      string volumeName,
      IsoDirectory root,
      uint volumeSpaceSize,
      uint pathTableSize1,
      uint pathTableSize2,
      uint typeLPathTable1,
      uint typeMPathTable1,
      uint typeLPathTable2,
      uint typeMPathTable2)
    {
      DirectoryRecordWrapper root1 = new DirectoryRecordWrapper(root.Extent1, root.Size1, root.Date, root.IsDirectory, ".");
      new VolumeDescriptorWrapper(volumeName, volumeSpaceSize, pathTableSize1, typeLPathTable1, typeMPathTable1, root1, DateTime.Now, DateTime.Now, (sbyte) 8)
      {
        VolumeDescriptorType = VolumeType.Primary
      }.Write(writer);
      DirectoryRecordWrapper root2 = new DirectoryRecordWrapper(root.Extent2, root.Size2, root.Date, root.IsDirectory, ".");
      VolumeDescriptorWrapper descriptorWrapper = new VolumeDescriptorWrapper(volumeName, volumeSpaceSize, pathTableSize2, typeLPathTable2, typeMPathTable2, root2, DateTime.Now, DateTime.Now, (sbyte) 8);
      descriptorWrapper.VolumeDescriptorType = VolumeType.Suplementary;
      descriptorWrapper.Write(writer);
      descriptorWrapper.VolumeDescriptorType = VolumeType.SetTerminator;
      descriptorWrapper.Write(writer);
    }

    private void WriteDirectories(BinaryWriter writer, IsoDirectory[] dirArray, VolumeType type)
    {
      if (dirArray == null)
        return;
      for (int index = 0; index < dirArray.Length; ++index)
      {
        dirArray[index].Write(writer, type);
        this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      }
    }

    private int WritePathTable(
      BinaryWriter writer,
      IsoDirectory[] dirArray,
      Endian endian,
      VolumeType type)
    {
      if (dirArray == null)
        return 0;
      int num = 0;
      for (int index = 0; index < dirArray.Length; ++index)
        num += dirArray[index].WritePathTable(writer, index == 0, endian, type);
      writer.Write(new byte[(long) IsoAlgorithm.SectorSize - (long) num % (long) IsoAlgorithm.SectorSize]);
      return num;
    }

    private void Folder2Iso(
      DirectoryInfo rootDirectoryInfo,
      BinaryWriter writer,
      string volumeName)
    {
      this.OnProgress("Initializing ISO root directory...", 0, 1);
      IsoDirectory root = new IsoDirectory(rootDirectoryInfo, 1U, "0", this.Progress);
      this.OnProgress("Preparing first set of directory extents...", 0, 1);
      ArrayList stack1 = new ArrayList();
      stack1.Add((object) root);
      IsoDirectory.SetExtent1(stack1, 0, 19U);
      this.OnProgress(1);
      this.OnProgress("Calculating directory numbers...", 0, 1);
      IsoDirectory[] dirArray1 = new IsoDirectory[stack1.Count];
      stack1.ToArray().CopyTo((Array) dirArray1, 0);
      this.SetDirectoryNumbers(dirArray1);
      this.OnProgress(1);
      this.OnProgress("Preparing first set of path tables...", 0, 2);
      MemoryStream output1 = new MemoryStream();
      BinaryWriter writer1 = new BinaryWriter((Stream) output1);
      IsoDirectory isoDirectory1 = dirArray1[dirArray1.Length - 1];
      uint typeLPathTable1 = isoDirectory1.Extent1 + isoDirectory1.Size1 / IsoAlgorithm.SectorSize;
      this.WritePathTable(writer1, dirArray1, Endian.LittleEndian, VolumeType.Primary);
      this.OnProgress(1);
      uint typeMPathTable1 = typeLPathTable1 + (uint) output1.Length / IsoAlgorithm.SectorSize;
      uint pathTableSize1 = (uint) this.WritePathTable(writer1, dirArray1, Endian.BigEndian, VolumeType.Primary);
      this.OnProgress(2);
      this.OnProgress("Preparing second set of directory extents...", 0, 1);
      ArrayList stack2 = new ArrayList();
      stack2.Add((object) root);
      uint currentExtent1 = typeLPathTable1 + (uint) output1.Length / IsoAlgorithm.SectorSize;
      IsoDirectory.SetExtent2(stack2, 0, currentExtent1);
      IsoDirectory[] dirArray2 = new IsoDirectory[stack2.Count];
      stack2.ToArray().CopyTo((Array) dirArray2, 0);
      this.OnProgress(1);
      this.OnProgress("Preparing second set of path tables...", 0, 2);
      MemoryStream output2 = new MemoryStream();
      BinaryWriter writer2 = new BinaryWriter((Stream) output2);
      IsoDirectory isoDirectory2 = dirArray2[dirArray2.Length - 1];
      uint typeLPathTable2 = isoDirectory2.Extent2 + isoDirectory2.Size2 / IsoAlgorithm.SectorSize;
      this.WritePathTable(writer2, dirArray2, Endian.LittleEndian, VolumeType.Suplementary);
      this.OnProgress(1);
      uint typeMPathTable2 = typeLPathTable2 + (uint) output2.Length / IsoAlgorithm.SectorSize;
      uint pathTableSize2 = (uint) this.WritePathTable(writer2, dirArray2, Endian.BigEndian, VolumeType.Suplementary);
      this.OnProgress(2);
      this.OnProgress("Initializing...", 0, 1);
      uint currentExtent2 = typeLPathTable2 + (uint) output2.Length / IsoAlgorithm.SectorSize;
      root.SetFilesExtent(ref currentExtent2);
      uint num = 19U + root.TotalSize + (uint) output1.Length / IsoAlgorithm.SectorSize + (uint) output2.Length / IsoAlgorithm.SectorSize;
      byte[] buffer1 = output1.GetBuffer();
      Array.Resize<byte>(ref buffer1, (int) output1.Length);
      byte[] buffer2 = output2.GetBuffer();
      Array.Resize<byte>(ref buffer2, (int) output2.Length);
      output1.Close();
      output2.Close();
      writer1.Close();
      writer2.Close();
      this.OnProgress(1);
      this.OnProgress("Writing data to file...", 0, (int) num);
      this.WriteFirst16EmptySectors(writer);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.WriteVolumeDescriptors(writer, volumeName, root, num, pathTableSize1, pathTableSize2, typeLPathTable1, typeMPathTable1, typeLPathTable2, typeMPathTable2);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.WriteDirectories(writer, dirArray2, VolumeType.Primary);
      writer.Write(buffer1);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.WriteDirectories(writer, dirArray2, VolumeType.Suplementary);
      writer.Write(buffer2);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      root.WriteFiles(writer, this.Progress);
      this.OnProgress("Finished.", 1, 1);
    }

    public void Folder2Iso(string folderPath, string isoPath, string volumeName)
    {
      try
      {
        FileStream output = new FileStream(isoPath, FileMode.Create);
        BinaryWriter writer = new BinaryWriter((Stream) output);
        DirectoryInfo rootDirectoryInfo = new DirectoryInfo(folderPath);
        try
        {
          this.Folder2Iso(rootDirectoryInfo, writer, volumeName);
          writer.Close();
          output.Close();
          this.OnFinished("ISO writing process finished succesfully");
        }
        catch (Exception ex)
        {
          writer.Close();
          output.Close();
          throw ex;
        }
      }
      catch (ThreadAbortException ex)
      {
        this.OnAbort("Aborted by user");
      }
      catch (Exception ex)
      {
        this.OnAbort(ex.Message);
      }
    }

    public void Folder2Iso(object data)
    {
      if (data.GetType() != typeof (IsoCreator.IsoCreatorFolderArgs))
        return;
      IsoCreator.IsoCreatorFolderArgs creatorFolderArgs = (IsoCreator.IsoCreatorFolderArgs) data;
      this.Folder2Iso(creatorFolderArgs.FolderPath, creatorFolderArgs.IsoPath, creatorFolderArgs.VolumeName);
    }

    private void Tree2Iso(TreeNode volume, BinaryWriter writer)
    {
      this.OnProgress("Initializing ISO root directory...", 0, 1);
      IsoDirectory root = new IsoDirectory(volume, 1U, "0", this.Progress);
      this.OnProgress("Preparing first set of directory extents...", 0, 1);
      ArrayList stack1 = new ArrayList();
      stack1.Add((object) root);
      IsoDirectory.SetExtent1(stack1, 0, 19U);
      this.OnProgress(1);
      this.OnProgress("Calculating directory numbers...", 0, 1);
      IsoDirectory[] dirArray1 = new IsoDirectory[stack1.Count];
      stack1.ToArray().CopyTo((Array) dirArray1, 0);
      this.SetDirectoryNumbers(dirArray1);
      this.OnProgress(1);
      this.OnProgress("Preparing first set of path tables...", 0, 2);
      MemoryStream output1 = new MemoryStream();
      BinaryWriter writer1 = new BinaryWriter((Stream) output1);
      IsoDirectory isoDirectory1 = dirArray1[dirArray1.Length - 1];
      uint typeLPathTable1 = isoDirectory1.Extent1 + isoDirectory1.Size1 / IsoAlgorithm.SectorSize;
      this.WritePathTable(writer1, dirArray1, Endian.LittleEndian, VolumeType.Primary);
      this.OnProgress(1);
      uint typeMPathTable1 = typeLPathTable1 + (uint) output1.Length / IsoAlgorithm.SectorSize;
      uint pathTableSize1 = (uint) this.WritePathTable(writer1, dirArray1, Endian.BigEndian, VolumeType.Primary);
      this.OnProgress(2);
      this.OnProgress("Preparing second set of directory extents...", 0, 1);
      ArrayList stack2 = new ArrayList();
      stack2.Add((object) root);
      uint currentExtent = typeLPathTable1 + (uint) output1.Length / IsoAlgorithm.SectorSize;
      IsoDirectory.SetExtent2(stack2, 0, currentExtent);
      IsoDirectory[] dirArray2 = new IsoDirectory[stack2.Count];
      stack2.ToArray().CopyTo((Array) dirArray2, 0);
      this.OnProgress(1);
      this.OnProgress("Preparing second set of path tables...", 0, 2);
      MemoryStream output2 = new MemoryStream();
      BinaryWriter writer2 = new BinaryWriter((Stream) output2);
      IsoDirectory isoDirectory2 = dirArray2[dirArray2.Length - 1];
      uint typeLPathTable2 = isoDirectory2.Extent2 + isoDirectory2.Size2 / IsoAlgorithm.SectorSize;
      this.WritePathTable(writer2, dirArray2, Endian.LittleEndian, VolumeType.Suplementary);
      this.OnProgress(1);
      uint typeMPathTable2 = typeLPathTable2 + (uint) output2.Length / IsoAlgorithm.SectorSize;
      uint pathTableSize2 = (uint) this.WritePathTable(writer2, dirArray2, Endian.BigEndian, VolumeType.Suplementary);
      this.OnProgress(2);
      this.OnProgress("Initializing...", 0, 1);
      uint num = 19U + root.TotalDirSize + (uint) output1.Length / IsoAlgorithm.SectorSize + (uint) output2.Length / IsoAlgorithm.SectorSize;
      byte[] buffer1 = output1.GetBuffer();
      Array.Resize<byte>(ref buffer1, (int) output1.Length);
      byte[] buffer2 = output2.GetBuffer();
      Array.Resize<byte>(ref buffer2, (int) output2.Length);
      output1.Close();
      output2.Close();
      writer1.Close();
      writer2.Close();
      this.OnProgress(1);
      this.OnProgress("Writing data to file...", 0, (int) num);
      this.WriteFirst16EmptySectors(writer);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.WriteVolumeDescriptors(writer, volume.Name, root, num, pathTableSize1, pathTableSize2, typeLPathTable1, typeMPathTable1, typeLPathTable2, typeMPathTable2);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.WriteDirectories(writer, dirArray2, VolumeType.Primary);
      writer.Write(buffer1);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.WriteDirectories(writer, dirArray2, VolumeType.Suplementary);
      writer.Write(buffer2);
      this.OnProgress((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize));
      this.OnProgress("Finished.", 1, 1);
    }

    public void Tree2Iso(TreeNode volume, string isoPath)
    {
      try
      {
        FileStream output = new FileStream(isoPath, FileMode.Create);
        BinaryWriter writer = new BinaryWriter((Stream) output);
        try
        {
          this.Tree2Iso(volume, writer);
          writer.Close();
          output.Close();
          this.OnFinished("ISO writing process finished succesfully");
        }
        catch (Exception ex)
        {
          writer.Close();
          output.Close();
          throw ex;
        }
      }
      catch (ThreadAbortException ex)
      {
        this.OnAbort("Aborted by user");
      }
      catch (Exception ex)
      {
        this.OnAbort(ex.Message);
      }
    }

    public void Tree2Iso(object data)
    {
      if (data.GetType() != typeof (IsoCreator.IsoCreatorTreeArgs))
        return;
      IsoCreator.IsoCreatorTreeArgs isoCreatorTreeArgs = (IsoCreator.IsoCreatorTreeArgs) data;
      this.Tree2Iso(isoCreatorTreeArgs.Volume, isoCreatorTreeArgs.IsoPath);
    }

    public event ProgressDelegate Progress;

    public event FinishDelegate Finish;

    public event AbortDelegate Abort;

    private void OnFinished(string message)
    {
      if (this.Finish == null)
        return;
      this.Finish((object) this, new FinishEventArgs(message));
    }

    private void OnProgress(int current)
    {
      if (this.Progress == null)
        return;
      this.Progress((object) this, new ProgressEventArgs(current));
    }

    private void OnProgress(int current, int maximum)
    {
      if (this.Progress == null)
        return;
      this.Progress((object) this, new ProgressEventArgs(current, maximum));
    }

    private void OnProgress(string action, int current, int maximum)
    {
      if (this.Progress == null)
        return;
      this.Progress((object) this, new ProgressEventArgs(action, current, maximum));
    }

    private void OnAbort(string message)
    {
      if (this.Abort == null)
        return;
      this.Abort((object) this, new AbortEventArgs(message));
    }

    public class IsoCreatorFolderArgs
    {
      private string m_folderPath;
      private string m_isoPath;
      private string m_volumeName;

      public string FolderPath => this.m_folderPath;

      public string IsoPath => this.m_isoPath;

      public string VolumeName => this.m_volumeName;

      public IsoCreatorFolderArgs(string folderPath, string isoPath, string volumeName)
      {
        this.m_folderPath = folderPath;
        this.m_isoPath = isoPath;
        this.m_volumeName = volumeName;
      }
    }

    public class IsoCreatorTreeArgs
    {
      private TreeNode m_volume;
      private string m_isoPath;

      public TreeNode Volume => this.m_volume;

      public string IsoPath => this.m_isoPath;

      public IsoCreatorTreeArgs(TreeNode volume, string isoPath)
      {
        this.m_volume = volume;
        this.m_isoPath = isoPath;
      }
    }
  }
}
