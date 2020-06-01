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
    private const string _fileUploadUrl = "http://localhost:4000/upload";
    private const string _filePath = @"C:\Users\aivarasjonikas\Documents\Development\NetworkMonitor\d\50MBTest.txt";

    public SpeedTesterTests()
    {
      _speedTester = new BandwidthTester(new HttpClient(), _fileDownloadUrl, _fileUploadUrl, _filePath);
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
