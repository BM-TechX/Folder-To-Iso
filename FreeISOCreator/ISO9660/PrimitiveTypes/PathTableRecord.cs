// Decompiled with JetBrains decompiler
// Type: ISO9660.PrimitiveTypes.PathTableRecord
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

namespace ISO9660.PrimitiveTypes
{
  internal class PathTableRecord
  {
    public byte Length;
    public byte ExtendedLength;
    public uint ExtentLocation;
    public ushort ParentNumber;
    public byte[] Identifier;
  }
}
