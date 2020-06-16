using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Client.Models
{
  public class BandwidthTestResult
  {
    public BandwidthTestResult(double downloadSpeed, double uploadSpeed)
    {
      DownloadSpeed = downloadSpeed;
      UploadSpeed = uploadSpeed;
    }

    public double DownloadSpeed { get; set; }
    public double UploadSpeed { get; set; }
  }
}
