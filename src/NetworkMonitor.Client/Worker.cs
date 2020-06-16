using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetworkMonitor.Client.Models;
using NetworkMonitor.Client.Repositories;

namespace NetworkMonitor
{
  public class Worker : BackgroundService
  {
    private readonly string _apiUrl;
    private readonly int _interval;
    private readonly string _filePath;
    private readonly string _apiSecret;
    private readonly INetMonitorRepo _repo;
    private readonly ILogger<Worker> _logger;
    private readonly HttpClient _client;

    public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, INetMonitorRepo repo)
    {
      _logger = logger;
      _client = httpClientFactory.CreateClient();
      _apiUrl = configuration.GetValue<string>("API_URL") ?? throw new ArgumentNullException("No api url");
      _interval = configuration.GetValue<int>("NETTEST_INTERVAL");
      _filePath = configuration.GetValue<string>("FILEPATH");
      _apiSecret = configuration.GetValue<string>("API_SECRET");
      _repo = repo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var bandwidthTester = new BandwidthTester(_client, _apiUrl, _apiSecret, _filePath);

      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        var downloadSpeed = await bandwidthTester.GetDownloadSpeedMbSec();

        var uploadSpeed = await bandwidthTester.GetUploadSpeedMbSec();

        var resultObject = new BandwidthTestResult(downloadSpeed, uploadSpeed);

        var result = await _repo.WriteBandwidthTestResults(resultObject);

        await Task.Delay(_interval, stoppingToken);
      }
    }
  }
}
