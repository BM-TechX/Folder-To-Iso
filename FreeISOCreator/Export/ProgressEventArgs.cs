// Decompiled with JetBrains decompiler
// Type: Export.ProgressEventArgs
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System;

namespace Export
{
  public class ProgressEventArgs : EventArgs
  {
    private int m_current = -1;
    private int m_maximum = -1;
    private string m_action;

    public ProgressEventArgs(int current) => this.m_current = current;

    public ProgressEventArgs(int current, int maximum)
    {
      this.m_current = current;
      this.m_maximum = maximum;
    }

    public ProgressEventArgs(string action, int current, int maximum)
    {
      this.m_action = action;
      this.m_current = current;
      this.m_maximum = maximum;
    }

    public int Current
    {
      get => this.m_current;
      set => this.m_current = value;
    }

    public int Maximum
    {
      get => this.m_maximum;
      set => this.m_maximum = value;
    }

    public string Action
    {
      get => this.m_action;
      set => this.m_action = value;
    }
  }
}
