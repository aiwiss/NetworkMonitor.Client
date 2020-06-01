using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMonitor
{
  public class BandwidthTester
  {
    private readonly HttpClient _httpClient;
    private readonly string _fileDownloadUrl;

    public BandwidthTester(HttpClient client, string fileDownloadUrl)
    {
      _httpClient = client;
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "other");
      _fileDownloadUrl = fileDownloadUrl;
    }

    public async Task RunTestsAndStoreResults()
    {
      var downloadSpeed = await GetDownloadSpeed();
    }

    public async Task<double> GetDownloadSpeed()
    {
      var sw = new Stopwatch();
      var converter = new Converter();

      sw.Start();
      var res = await _httpClient.GetAsync(_fileDownloadUrl);
      sw.Stop();

      var contentLength = res.Content.Headers.ContentLength;

      var fileSizeMB = contentLength.HasValue ? converter.BytesToMBytes(contentLength.Value) : 0;

      var elapsedTimeSeconds = converter.MilisecondsToSeconds(sw.ElapsedMilliseconds);
      var downloadSpeedMBSec = Math.Round(fileSizeMB / elapsedTimeSeconds, 2);

      return downloadSpeedMBSec;
    }
  }
}
