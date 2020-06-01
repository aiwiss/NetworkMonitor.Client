using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor
{
  public class Converter
  {
    public double BytesToMbits(long bytes)
    {
      var divider = 125000;
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
