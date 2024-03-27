// Decompiled with JetBrains decompiler
// Type: FreeISOCreator.Program
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using System;
using System.Windows.Forms;


namespace FreeISOCreator
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
        if (args.Length == 3)
        {
                cmdverstion cmdverstion = new cmdverstion();
                cmdverstion.CmdVersion(args[0], args[1], args[2]);
                //test
           

            }
        else
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run((Form)new MainForm());

         }
    }
  }
}
