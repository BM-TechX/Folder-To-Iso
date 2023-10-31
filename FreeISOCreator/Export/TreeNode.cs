// Decompiled with JetBrains decompiler
// Type: Export.TreeNode
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System;

namespace Export
{
  public class TreeNode
  {
    private TreeNodeCollection m_dirCol = new TreeNodeCollection();
    private TreeNodeCollection m_fileCol = new TreeNodeCollection();
    private string m_name = "";
    private string m_shortName = "";
    private uint m_length;
    private DateTime m_creationTime;
    private bool m_isDirectory;
    private string m_fullName = "";

    public TreeNodeCollection Files => this.m_fileCol;

    public TreeNodeCollection Directories => this.m_dirCol;

    public string Name
    {
      get => this.m_name;
      set => this.m_name = value;
    }

    public string ShortName
    {
      get => this.m_shortName;
      set => this.m_shortName = value;
    }

    public uint Length
    {
      get => this.m_length;
      set => this.m_length = value;
    }

    public DateTime CreationTime
    {
      get => this.m_creationTime;
      set => this.m_creationTime = value;
    }

    public bool IsDirectory
    {
      get => this.m_isDirectory;
      set => this.m_isDirectory = value;
    }

    public string FullName
    {
      get => this.m_fullName;
      set => this.m_fullName = value;
    }

    public TreeNode[] GetFiles() => this.m_fileCol.ToArray();

    public TreeNode[] GetDirectories() => this.m_dirCol.ToArray();

    public TreeNode[] GetAllChildren()
    {
      TreeNodeCollection treeNodeCollection = new TreeNodeCollection();
      treeNodeCollection.AddRange(this.m_dirCol);
      treeNodeCollection.AddRange(this.m_fileCol);
      return treeNodeCollection.ToArray();
    }

    public override string ToString()
    {
      string str = "<node name=\"" + this.Name + "\" dir=\"true\">";
      foreach (object directory in this.GetDirectories())
        str += directory.ToString();
      foreach (TreeNode file in this.GetFiles())
        str = str + "<node name=\"" + file.Name + "\" dir=\"false\"/>";
      return str + "</node>";
    }
  }
}
