// Decompiled with JetBrains decompiler
// Type: IsoCreator.DirectoryTree.FolderElementList
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System.Collections;

namespace IsoCreator.DirectoryTree
{
  internal class FolderElementList : CollectionBase
  {
    public void Add(IsoFolderElement value) => this.InnerList.Add((object) value);

    public void Sort() => this.InnerList.Sort((IComparer) new FolderElementList.DirEntryComparer());

    public IsoFolderElement this[int index]
    {
      get => (IsoFolderElement) this.InnerList[index];
      set => this.InnerList[index] = (object) value;
    }

    public class DirEntryComparer : IComparer
    {
      public int Compare(object x, object y) => string.Compare(((IsoFolderElement) x).LongName, ((IsoFolderElement) y).LongName, false);
    }
  }
}
