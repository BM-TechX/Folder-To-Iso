// Decompiled with JetBrains decompiler
// Type: Export.ExportIso
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

namespace Export
{
  public class ExportIso : IExportPlugin
  {
    private IsoCreator.IsoCreator m_creator = new IsoCreator.IsoCreator();
    private TreeNode m_volume;
    private string m_fileName;

    public ExportIso()
    {
      this.m_creator.Progress += new ProgressDelegate(this.creator_Progress);
      this.m_creator.Abort += new AbortDelegate(this.creator_Abort);
      this.m_creator.Finish += new FinishDelegate(this.creator_Finished);
    }

    private void creator_Finished(object sender, FinishEventArgs e)
    {
      if (this.Finished == null)
        return;
      this.Finished(sender, e);
    }

    private void creator_Abort(object sender, AbortEventArgs e)
    {
      if (this.Abort == null)
        return;
      this.Abort(sender, e);
    }

    private void creator_Progress(object sender, ProgressEventArgs e)
    {
      if (this.Progress == null)
        return;
      this.Progress(sender, e);
    }

    public string ID => "ExportISO";

    public string Name => "ISO";

    public string Extension => "iso";

    public string Description => "CD image with virtual files";

    public TreeNode Volume
    {
      get => this.m_volume;
      set => this.m_volume = value;
    }

    public string FileName
    {
      get => this.m_fileName;
      set => this.m_fileName = value;
    }

    public void DoExport()
    {
      if (this.m_volume == null || this.m_fileName == null)
        return;
      this.m_creator.Tree2Iso(this.m_volume, this.m_fileName);
    }

    public void DoExport(TreeNode volume, string fileName)
    {
      this.m_volume = volume;
      this.m_fileName = fileName;
      this.m_creator.Tree2Iso(this.m_volume, this.m_fileName);
    }

    public event ProgressDelegate Progress;

    public event FinishDelegate Finished;

    public event AbortDelegate Abort;

    public override string ToString() => this.Name;
  }
}
