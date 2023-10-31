// Decompiled with JetBrains decompiler
// Type: IsoCreator.DirectoryTree.IsoFile
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using Export;
using System.IO;

namespace IsoCreator.DirectoryTree
{
  internal class IsoFile : IsoFolderElement
  {
    private string m_fullPath;
    private uint m_size;
    private uint m_extent;

    public IsoFile(FileInfo file, string childNumber)
      : base((FileSystemInfo) file, false, childNumber)
    {
      this.m_fullPath = file.FullName;
      this.m_size = (uint) file.Length;
    }

    public IsoFile(TreeNode file, string childNumber)
      : base(file, false, childNumber)
    {
      this.m_fullPath = file.FullName;
      this.m_size = file.Length;
    }

    public override uint Extent1
    {
      get => this.m_extent;
      set => this.m_extent = value;
    }

    public override uint Extent2
    {
      get => this.m_extent;
      set => this.m_extent = value;
    }

    public override uint Size1 => this.m_size;

    public override uint Size2 => this.m_size;

    public override bool IsDirectory => false;

    public void Write(BinaryWriter writer, ProgressDelegate Progress)
    {
      if (this.m_extent <= 0U || this.m_size <= 0U)
        return;
      FileStream input = new FileStream(this.m_fullPath, FileMode.Open, FileAccess.Read);
      BinaryReader binaryReader = new BinaryReader((Stream) input);
      int count1 = (int) IsoAlgorithm.SectorSize * 512;
      byte[] buffer = new byte[count1];
      int count2;
      while (true)
      {
        count2 = binaryReader.Read(buffer, 0, count1);
        if (count2 != 0)
        {
          if (count2 == count1)
          {
            writer.Write(buffer);
            Progress((object) this, new ProgressEventArgs((int) (writer.BaseStream.Length / (long) IsoAlgorithm.SectorSize)));
          }
          else
            break;
        }
        else
          goto label_7;
      }
      writer.Write(buffer, 0, count2);
      if ((long) count2 % (long) IsoAlgorithm.SectorSize != 0L)
        writer.Write(new byte[(long) IsoAlgorithm.SectorSize - (long) count2 % (long) IsoAlgorithm.SectorSize]);
label_7:
      binaryReader.Close();
      input.Close();
    }
  }
}
