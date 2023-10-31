// Decompiled with JetBrains decompiler
// Type: Export.IExportPlugin
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

namespace Export
{
  public interface IExportPlugin
  {
    event ProgressDelegate Progress;

    event FinishDelegate Finished;

    event AbortDelegate Abort;

    string ID { get; }

    string Description { get; }

    string Extension { get; }

    TreeNode Volume { get; set; }

    string FileName { get; set; }

    void DoExport(TreeNode volume, string volumeDescription);

    void DoExport();
  }
}
