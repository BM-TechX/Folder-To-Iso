// Decompiled with JetBrains decompiler
// Type: IsoCreator.IsoWrappers.DateWrapper
// Assembly: FreeISOCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A96E54CF-A063-4A01-B798-5B4D5A02139B
// Assembly location: C:\Program Files (x86)\Free ISO Creator\FreeISOCreator.exe

using ISO9660.PrimitiveTypes;
using System;
using System.IO;

namespace IsoCreator.IsoWrappers
{
  internal class DateWrapper
  {
    private BinaryDateRecord m_binaryDateRecord;
    private AsciiDateRecord m_asciiDateRecord;
    private DateTime m_date;

    public BinaryDateRecord BinaryDateRecord
    {
      get => this.m_binaryDateRecord;
      set => this.m_binaryDateRecord = value;
    }

    public AsciiDateRecord AsciiDateRecord
    {
      get => this.m_asciiDateRecord;
      set => this.m_asciiDateRecord = value;
    }

    public DateTime Date
    {
      get => this.m_date;
      set
      {
        this.m_date = value;
        this.SetAsciiDateRecord(value);
        this.SetBinaryDateRecord(value);
      }
    }

    public sbyte TimeZone
    {
      get => this.m_asciiDateRecord.TimeZone;
      set => this.m_asciiDateRecord.TimeZone = value;
    }

    public DateWrapper(DateTime date) => this.Date = date;

    public DateWrapper(DateTime date, sbyte timeZone)
    {
      this.m_date = date;
      this.SetAsciiDateRecord(date, timeZone);
      this.SetBinaryDateRecord(date);
    }

    public DateWrapper(BinaryDateRecord dateRecord)
    {
      this.m_binaryDateRecord = dateRecord;
      this.SetAsciiDateRecord(1900 + (int) dateRecord.Year, (int) dateRecord.Month, (int) dateRecord.DayOfMonth, (int) dateRecord.Hour, (int) dateRecord.Minute, (int) dateRecord.Second, 0, (sbyte) 8);
      this.m_date = new DateTime(1900 + (int) dateRecord.Year, (int) dateRecord.Month, (int) dateRecord.DayOfMonth, (int) dateRecord.Hour, (int) dateRecord.Minute, (int) dateRecord.Second);
    }

    public DateWrapper(AsciiDateRecord dateRecord)
    {
      this.m_asciiDateRecord = dateRecord;
      byte year = (byte) (Convert.ToInt32(IsoAlgorithm.ByteArrayToString(dateRecord.Year)) - 1900);
      byte month = Convert.ToByte(IsoAlgorithm.ByteArrayToString(dateRecord.Month));
      byte num = Convert.ToByte(IsoAlgorithm.ByteArrayToString(dateRecord.DayOfMonth));
      byte hour = Convert.ToByte(IsoAlgorithm.ByteArrayToString(dateRecord.Hour));
      byte minute = Convert.ToByte(IsoAlgorithm.ByteArrayToString(dateRecord.Minute));
      byte second = Convert.ToByte(IsoAlgorithm.ByteArrayToString(dateRecord.Second));
      int millisecond = Convert.ToInt32(IsoAlgorithm.ByteArrayToString(dateRecord.HundredthsOfSecond)) * 10;
      this.SetBinaryDateRecord(year, month, num, hour, minute, second);
      this.m_date = new DateTime(1900 + (int) year, (int) month, (int) num, (int) hour, (int) minute, (int) second, millisecond);
    }

    private void SetBinaryDateRecord(
      byte year,
      byte month,
      byte dayOfMonth,
      byte hour,
      byte minute,
      byte second)
    {
      if (this.m_binaryDateRecord == null)
        this.m_binaryDateRecord = new BinaryDateRecord();
      this.m_binaryDateRecord.Year = year;
      this.m_binaryDateRecord.Month = month;
      this.m_binaryDateRecord.DayOfMonth = dayOfMonth;
      this.m_binaryDateRecord.Hour = hour;
      this.m_binaryDateRecord.Minute = minute;
      this.m_binaryDateRecord.Second = second;
    }

    private void SetBinaryDateRecord(DateTime date) => this.SetBinaryDateRecord((byte) (date.Year - 1900), (byte) date.Month, (byte) date.Day, (byte) date.Hour, (byte) date.Minute, (byte) date.Second);

    private void SetAsciiDateRecord(
      int year,
      int month,
      int dayOfMonth,
      int hour,
      int minute,
      int second,
      int hundredthsOfSecond,
      sbyte timeZone)
    {
      if (this.m_asciiDateRecord == null)
        this.m_asciiDateRecord = new AsciiDateRecord();
      string text1 = string.Format("{0:D4}", (object) (year % 10000));
      string text2 = string.Format("{0:D2}", (object) month);
      string text3 = string.Format("{0:D2}", (object) dayOfMonth);
      string text4 = string.Format("{0:D2}", (object) hour);
      string text5 = string.Format("{0:D2}", (object) minute);
      string text6 = string.Format("{0:D2}", (object) second);
      string text7 = string.Format("{0:D2}", (object) hundredthsOfSecond);
      this.m_asciiDateRecord.Year = IsoAlgorithm.StringToByteArray(text1);
      this.m_asciiDateRecord.Month = IsoAlgorithm.StringToByteArray(text2);
      this.m_asciiDateRecord.DayOfMonth = IsoAlgorithm.StringToByteArray(text3);
      this.m_asciiDateRecord.Hour = IsoAlgorithm.StringToByteArray(text4);
      this.m_asciiDateRecord.Minute = IsoAlgorithm.StringToByteArray(text5);
      this.m_asciiDateRecord.Second = IsoAlgorithm.StringToByteArray(text6);
      this.m_asciiDateRecord.HundredthsOfSecond = IsoAlgorithm.StringToByteArray(text7);
      this.m_asciiDateRecord.TimeZone = timeZone;
    }

    private void SetAsciiDateRecord(DateTime date, sbyte timeZone) => this.SetAsciiDateRecord(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond / 10, timeZone);

    private void SetAsciiDateRecord(DateTime date) => this.SetAsciiDateRecord(date, (sbyte) 8);

    public void ResetAsciiDateRecord()
    {
      this.m_date = new DateTime(0, 0, 0, 0, 0, 0, 0);
      this.SetAsciiDateRecord(this.m_date);
      this.SetBinaryDateRecord(this.m_date);
    }

    public int WriteBinaryDateRecord(BinaryWriter writer)
    {
      if (this.m_binaryDateRecord == null)
        return 0;
      writer.Write(new byte[6]
      {
        this.m_binaryDateRecord.Year,
        this.m_binaryDateRecord.Month,
        this.m_binaryDateRecord.DayOfMonth,
        this.m_binaryDateRecord.Hour,
        this.m_binaryDateRecord.Minute,
        this.m_binaryDateRecord.Second
      });
      return 6;
    }

    public int WriteAsciiDateRecord(BinaryWriter writer)
    {
      if (this.m_asciiDateRecord == null)
        return 0;
      writer.Write(this.m_asciiDateRecord.Year);
      writer.Write(this.m_asciiDateRecord.Month);
      writer.Write(this.m_asciiDateRecord.DayOfMonth);
      writer.Write(this.m_asciiDateRecord.Hour);
      writer.Write(this.m_asciiDateRecord.Minute);
      writer.Write(this.m_asciiDateRecord.Second);
      writer.Write(this.m_asciiDateRecord.HundredthsOfSecond);
      writer.Write(this.m_asciiDateRecord.TimeZone);
      return 17;
    }
  }
}
