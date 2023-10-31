// Decompiled with JetBrains decompiler
// Type: Export.FinishEventArgs
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System;

namespace Export
{
  public class FinishEventArgs : EventArgs
  {
    private string m_message;

    public FinishEventArgs(string message) => this.m_message = message;

    public string Message
    {
      get => this.m_message;
      set => this.m_message = value;
    }
  }
}
