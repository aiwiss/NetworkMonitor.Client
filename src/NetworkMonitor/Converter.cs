using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor
{
  public class Converter
  {
    public double BytesToMBytes(long bytes)
    {
      var divider = 1024 * 1024;
      var result = bytes / (double)divider;
      return result;
    }

    public double MilisecondsToSeconds(long ms)
    {
      var divider = 1000;
      var result = ms / (double)divider;
      return result;
    }
  }
}
