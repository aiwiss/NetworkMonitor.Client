using NetworkMonitor.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMonitor.Client.Repositories
{
  public interface INetMonitorRepo
  {
    Task<bool> WriteBandwidthTestResults(BandwidthTestResult testResults);
  }
}
