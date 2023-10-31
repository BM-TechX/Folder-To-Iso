// Decompiled with JetBrains decompiler
// Type: IsoCreator.DirectoryTree.IsoFolderElement
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using Export;
using System;
using System.IO;

namespace IsoCreator.DirectoryTree
{
  internal abstract class IsoFolderElement
  {
    private DateTime m_date;
    private string m_shortIdent;
    private string m_identifier;

    public IsoFolderElement(FileSystemInfo folderElement, bool isRoot, string childNumber)
    {
      this.m_date = folderElement.CreationTime;
      this.m_identifier = folderElement.Name;
      if (isRoot)
      {
        this.m_shortIdent = ".";
        this.m_identifier = ".";
      }
      else if (this.m_identifier.Length > 8)
      {
        this.m_shortIdent = this.m_identifier.Substring(0, 8 - childNumber.Length).ToUpper().Replace(' ', '_').Replace('.', '_');
        this.m_shortIdent += childNumber;
      }
      else
        this.m_shortIdent = this.m_identifier.ToUpper().Replace(' ', '_').Replace('.', '_');
      if (this.m_identifier.Length <= IsoAlgorithm.FileNameMaxLength)
        return;
      this.m_identifier = this.m_identifier.Substring(0, IsoAlgorithm.FileNameMaxLength - childNumber.Length) + childNumber;
    }

    public IsoFolderElement(TreeNode folderElement, bool isRoot, string childNumber)
    {
      this.m_date = folderElement.CreationTime;
      this.m_identifier = folderElement.Name;
      if (isRoot)
      {
        this.m_shortIdent = ".";
        this.m_identifier = ".";
      }
      else if (this.m_identifier.Length > 8)
      {
        this.m_shortIdent = this.m_identifier.Substring(0, 8 - childNumber.Length).ToUpper().Replace(' ', '_').Replace('.', '_');
        this.m_shortIdent += childNumber;
      }
      else
        this.m_shortIdent = this.m_identifier.ToUpper().Replace(' ', '_').Replace('.', '_');
      if (this.m_identifier.Length <= IsoAlgorithm.FileNameMaxLength)
        return;
      this.m_identifier = this.m_identifier.Substring(0, IsoAlgorithm.FileNameMaxLength - childNumber.Length) + childNumber;
    }

    public abstract uint Extent1 { get; set; }

    public abstract uint Extent2 { get; set; }

    public abstract uint Size1 { get; }

    public abstract uint Size2 { get; }

    public abstract bool IsDirectory { get; }

    public DateTime Date => this.m_date;

    public string ShortName
    {
      get => this.m_shortIdent;
      set => this.m_shortIdent = value;
    }

    public string LongName => this.m_identifier;
  }
}
