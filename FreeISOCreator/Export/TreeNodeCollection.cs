// Decompiled with JetBrains decompiler
// Type: Export.TreeNodeCollection
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System.Collections;

namespace Export
{
  public class TreeNodeCollection : CollectionBase
  {
    public int Add(TreeNode node) => this.InnerList.Add((object) node);

    public void AddRange(TreeNodeCollection collection) => this.InnerList.AddRange((ICollection) collection);

    public void Remove(TreeNode node) => this.InnerList.Remove((object) node);

    public TreeNode this[int index]
    {
      get => (TreeNode) this.InnerList[index];
      set => this.InnerList[index] = (object) value;
    }

    public TreeNode[] ToArray() => (TreeNode[]) this.InnerList.ToArray(typeof (TreeNode));
  }
}
