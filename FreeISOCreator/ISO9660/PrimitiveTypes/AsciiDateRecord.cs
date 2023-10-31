// Decompiled with JetBrains decompiler
// Type: ISO9660.PrimitiveTypes.AsciiDateRecord
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

namespace ISO9660.PrimitiveTypes
{
  internal class AsciiDateRecord
  {
    public byte[] Year = new byte[4]
    {
      (byte) 48,
      (byte) 48,
      (byte) 48,
      (byte) 48
    };
    public byte[] Month = new byte[2]
    {
      (byte) 48,
      (byte) 48
    };
    public byte[] DayOfMonth = new byte[2]
    {
      (byte) 48,
      (byte) 48
    };
    public byte[] Hour = new byte[2]{ (byte) 48, (byte) 48 };
    public byte[] Minute = new byte[2]
    {
      (byte) 48,
      (byte) 48
    };
    public byte[] Second = new byte[2]
    {
      (byte) 48,
      (byte) 48
    };
    public byte[] HundredthsOfSecond = new byte[2]
    {
      (byte) 48,
      (byte) 48
    };
    public sbyte TimeZone;
  }
}
