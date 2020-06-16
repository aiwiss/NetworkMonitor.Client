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
    private const string _fileDownloadUrl = "";
    private const string _fileUploadUrl = "";
    private const string _pfileDownloadUrl = "";
    private const string _pfileUploadUrl = "";
    private const string _filePath = "";

    public SpeedTesterTests()
    {
      _speedTester = new BandwidthTester(new HttpClient(),"", _pfileDownloadUrl, _filePath);
    }


    [Fact]
    public async Task GetDownloadSpeed_ValidUrl()
    {
      var result = await _speedTester.GetDownloadSpeedMbSec();
    }

    [Fact]
    public async Task GetUploadSpeed_ValidUrl()
    {
      var result = await _speedTester.GetUploadSpeedMbSec();
    }

  }
}
