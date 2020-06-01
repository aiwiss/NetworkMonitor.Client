using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetworkMonitor
{
  public class Worker : BackgroundService
  {
    private const string _fileDownloadUrl = "http://localhost:4000/download";
    private readonly ILogger<Worker> _logger;
    private readonly BandwidthTester _bandwidthTester;

    public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
    {
      _logger = logger;
      _bandwidthTester = new BandwidthTester(httpClientFactory.CreateClient(), _fileDownloadUrl, _fileDownloadUrl, _fileDownloadUrl);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        var downloadSpeed = _bandwidthTester.GetDownloadSpeedMbSec();

        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}
