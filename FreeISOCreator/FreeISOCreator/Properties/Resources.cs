// Decompiled with JetBrains decompiler
// Type: FreeISOCreator.Properties.Resources
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FreeISOCreator.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) FreeISOCreator.Properties.Resources.resourceMan, (object) null))
          FreeISOCreator.Properties.Resources.resourceMan = new ResourceManager("FreeISOCreator.Properties.Resources", typeof (FreeISOCreator.Properties.Resources).Assembly);
        return FreeISOCreator.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => FreeISOCreator.Properties.Resources.resourceCulture;
      set => FreeISOCreator.Properties.Resources.resourceCulture = value;
    }
  }
}
