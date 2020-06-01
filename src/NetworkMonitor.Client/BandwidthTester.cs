using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    private readonly string _fileUploadUrl;
    private readonly string _filePath;
    private readonly Converter _converter;

    public BandwidthTester(HttpClient client, string fileDownloadUrl, string fileUploadUrl, string filePath)
    {
      _httpClient = client;
      _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "other");
      _fileDownloadUrl = fileDownloadUrl;
      _fileUploadUrl = fileUploadUrl;
      _filePath = filePath;
      _converter = new Converter();
    }

    public async Task RunTestsAndStoreResults()
    {
      var downloadSpeed = await GetDownloadSpeedMbSec();
    }

    public async Task<double> GetDownloadSpeedMbSec()
    {
      var sw = new Stopwatch();

      sw.Start();
      var res = await _httpClient.GetAsync(_fileDownloadUrl);
      sw.Stop();

      var contentLength = res.Content.Headers.ContentLength;

      var fileSizeMb = contentLength.HasValue ? _converter.BytesToMbits(contentLength.Value) : 0;
      var elapsedTimeSeconds = _converter.MilisecondsToSeconds(sw.ElapsedMilliseconds);
      var downloadSpeedMbSec = GetSpeedMbSec(fileSizeMb, elapsedTimeSeconds);

      return downloadSpeedMbSec;
    }

    public async Task<double> GetUploadSpeedMbSec()
    {
      var sw = new Stopwatch();

      var file = File.ReadAllBytes(_filePath);
      var fileContent = new ByteArrayContent(file);
      var multiPartContent = new MultipartFormDataContent();
      multiPartContent.Add(fileContent, "50MBTest", "50MBTest.txt");

      sw.Start();
      var res = await _httpClient.PostAsync(_fileUploadUrl, multiPartContent);
      sw.Stop();

      var fileSizeMb = _converter.BytesToMbits(file.Length);
      var elapsedTimeSeconds = _converter.MilisecondsToSeconds(sw.ElapsedMilliseconds);
      var uploadSpeedMbSec = GetSpeedMbSec(fileSizeMb, elapsedTimeSeconds);

      return uploadSpeedMbSec;
    }

    private double GetSpeedMbSec(double contentSize, double elapsedTime)
    {
      return Math.Round(contentSize / elapsedTime, 2);
    }
  }
}
