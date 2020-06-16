using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkMonitor.Client.Models
{
  public class ConnectionString
  {
    public string Default { get; private set; }
    public ConnectionString(string connectionString)
    {
      Default = connectionString;
    }
  }
}
