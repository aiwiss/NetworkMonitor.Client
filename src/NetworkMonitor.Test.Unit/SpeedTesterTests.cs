using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetworkMonitor.Test.Unit
{
  public class SpeedTesterTests
  {
    private BandwidthTester _speedTester;
    private const string _fileDownloadUrl = "http://localhost:4000/download";

    public SpeedTesterTests()
    {
      _speedTester = new BandwidthTester(new HttpClient(), _fileDownloadUrl);
    }


    [Fact]
    public async Task GetDownloadSpeed_ValidUrl()
    {
      var result = await _speedTester.GetDownloadSpeed();
    }

    
  }
}
